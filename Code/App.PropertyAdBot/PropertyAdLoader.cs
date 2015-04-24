using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.PropertyAdBot.Model;
using TK1.Data.Bizz.Broker.Controller;
using TK1.Data.Bizz.Mapper.Controller;
using TK1.Data.Bizz.Model;
using TK1.Utility;

namespace App.PropertyAdBot
{

    public class PropertyAdLoader
    {
        public bool IsRunning { get; set; }
        public bool IsCancelling { get; set; }
        public bool IsFinished { get; set; }
        public List<PropertyAdLoadItem> Queue { get; set; }

        public PropertyAdLoader()
        {
            Queue = new List<PropertyAdLoadItem>();
        }
        internal void StartAsyncLoad()
        {
            var thread = new Thread(new ThreadStart(asyncLoader));
            thread.Start();
            this.IsRunning = true;
        }

        private void asyncLoader()
        {
            int count = 0, countSuccess = 0, countError = 0;
            var keepLoading = true;
            PropertyAdController adController = null;
            BizzMapperController mapController = null;

            try
            {
                while (keepLoading)
                {
                    PropertyAdLoadItem adItem = null;
                    try
                    {
                        if (Queue.Count > 0)
                        {
                            count++;
                            adItem = Queue.FirstOrDefault();
                            var ad = adItem.Ad;

                            if (adController == null)
                                adController = new PropertyAdController(ad.CustomerCode);
                            if (mapController == null)
                                mapController = new BizzMapperController();

                            var location = ad.Location;
                            mapController.UpdateLocation(ref location);
                            mapController.UpdateLocationGroup(ref location);
                            ad.Location = location;
                            adController.SetPropertyAd(ad);
                            adController.SetPropertyAdDetails(ad.AdType, ad.AdCode, ad.Details);
                            countSuccess++;
                        }
                    }
                    catch (Exception exception)
                    {
                        countError++;
                        mapController.RollBack();
                        adController.RollBack();
                        //AppOutput.WriteToFile("runAdBot.UpdateDB ERROR: " + ad.Details.Where(o => o.Code == PropertyAdDetailCodes.PROPERTY_AD_URL).FirstOrDefault().Value, true);
                        AppOutput.WriteToFile(exception);
                    }
                    finally
                    {
                        if (adItem != null)
                        {
                            lock (Queue)
                            {
                                Queue.Remove(adItem);
                            }
                        }
                    }

                    keepLoading = !(IsCancelling & Queue.Count == 0);
                    Thread.Sleep(100);
                }
            }
            finally
            {
                this.IsRunning = false;
                this.IsCancelling = false;
                this.IsFinished = true;
            }
        }

        internal void StopAsyncLoad()
        {
            AppOutput.Write("PropertyAdLoader.StopAsyncLoad", true);
            this.IsCancelling = true;
        }

        internal void WaitForAsyncOperations()
        {
            AppOutput.Write("PropertyAdLoader.WaitForAsyncOperations START", true);
            while (Queue.Count > 0)
            {
                Thread.Sleep(1000);
            }
            AppOutput.Write("PropertyAdLoader.WaitForAsyncOperations END", true);
        }

        internal void LoadAsync(Model.PropertyAdLoadItem item)
        {
            if (item != null)
            {
                lock (Queue)
                {
                    Queue.Add(item);
                }
            }
        }
    }
}
