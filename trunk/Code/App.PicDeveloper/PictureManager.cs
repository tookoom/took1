using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Media.Data;
using TK1.Media.Collection;
using System.IO;
using TK1.PicDeveloper.Settings;
using TK1.PicDeveloper.Data;
using TK1.Data.Model.Presentation;
using TK1.Media;

namespace TK1.PicDeveloper
{
    public class PictureManager : ImageViewManager
    {
        #region CONST
        #endregion
        #region EVENTS
        public event EventHandler TotalPriceChanged;
        private void onTotalPriceChanged(EventArgs e)
        {
            EventHandler handler = TotalPriceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
        #region PRIVATE MEMBERS
        private PaperSizes selectedPicSize;
        private PaperTypes selectedPicType;

        private string clientPictureFolderPath = string.Empty;
        //private PictureDevelopmentCollection pictureDevelopments;
        private List<PicturePrice> priceList;

        private float totalPrice = 0;
        private int quantity = 0;

        PicDeveloperSettings settings = null;
        PersonView client = null;

        #endregion
        #region PUBIC ACTIONS
        //public Action<PictureDevelopment> AddPicture;
        //public Action<PictureDevelopment> RemovePicture;

        #endregion        
        #region PUBLIC PROPERTIES
        public float TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                //textBlockTotalPrice.Text = string.Format("Total: R$ {0:0.00}", totalPrice);
            }
        }
        public PaperSizes PicSize
        {
            get { return selectedPicSize; }
            set { selectedPicSize = value; }
        }
        public PaperTypes PicType
        {
            get { return selectedPicType; }
            set { selectedPicType = value; }
        }
        //public PictureDevelopmentCollection Pictures
        //{
        //    get { return pictureDevelopments; }
        //    set { pictureDevelopments = value; }
        //}
        public PersonView Client
        {
            get { return client; }
            set { client = value; }
        }

        #endregion

        public PictureManager(PicDeveloperSettings settings)
        {
            this.settings = settings;
            initialize();
        }

        public void CalculateTotalPrice()
        {
            calculateTotalPrice();
        }

        public void ClearList()
        {
            calculateTotalPrice();
            base.ClearList();
        }
        public void GetFolderPics()
        {
            base.GetFolderPics();
        }
        public void LoadCdromItems()
        {
            base.LoadCdromItems();
        }
        public void LoadUsbItems()
        {
            base.LoadUsbItems();
        }
        public void SavePics()
        {
            base.SavePics();
        }


        private void calculateTotalPrice()
        {
            float totalprice = 0;
            float unitprice = 0;
            int count = 0;

            var query = from el in priceList
                        where el.Size == selectedPicSize & el.Type == selectedPicType
                        select el.Price;

            if (query.Count() > 0)
                unitprice = query.FirstOrDefault();

            foreach (ImageView imageView in pictures)
            {
                totalprice += imageView.Quantity * unitprice;
                count += (int)imageView.Quantity;
            }

            TotalPrice = totalprice;
            Quantity = count;
            onTotalPriceChanged(new EventArgs());
        }
        private string createInfoFile()
        {
            string info = "";
            string newline = Environment.NewLine;
            info += string.Format("Registro {0} " + newline, DateTime.Now);
            //info += string.Format("Nome do cliente: {0} " + newline, textBoxClientName.Text);
            //info += string.Format("Telefone do cliente: {0} " + newline, textBoxClientPhone.Text);
            info += string.Format("Tamanho de foto: {0} " + newline, selectedPicSize);
            info += string.Format("Tipo do papel: {0} " + newline, selectedPicType);
            info += string.Format("Total de fotos: {0} " + newline, pictureCounter);
            info += string.Format("Preço total: R$ {0} " + newline, TotalPrice);
            return info;
        }
        private void finish()
        {
            pictures.Clear();
            //wrapPanelPictures.Children.Clear();
            Quantity = 0;
            TotalPrice = 0;
            //textBoxClientName.Text = "";
            //textBoxClientPhone.Text = "";
        }
        private void initialize()
        {
            TotalPrice = 0;

            priceList = new List<PicturePrice>();

            client = new PersonView();
            client.Name = "Nome";
            client.EmailList.Add(new EmailAddress() { Address = "andre.v.mattos@gmail.com" });
            client.PhoneList.Add(new TelephoneNumber() { Code = "051", Number = "85757025" });
        }
        private void loadPrices()
        {
            //if (priceList == null)
            //    priceList = new PicturePriceCollection();

            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow10x15)),
            //    Size = PaperSizes._10x15,
            //    Type = PaperTypes.Gloss
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow13x18)),
            //    Size = PaperSizes._13x18,
            //    Type = PaperTypes.Gloss
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow15x21)),
            //    Size = PaperSizes._15x21,
            //    Type = PaperTypes.Gloss
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow20x25)),
            //    Size = PaperSizes._20x25,
            //    Type = PaperTypes.Gloss
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow20x30)),
            //    Size = PaperSizes._20x30,
            //    Type = PaperTypes.Gloss
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceGlow30x45)),
            //    Size = PaperSizes._30x45,
            //    Type = PaperTypes.Gloss
            //});

            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular10x15)),
            //    Size = PaperSizes._10x15,
            //    Type = PaperTypes.Regular
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular13x18)),
            //    Size = PaperSizes._13x18,
            //    Type = PaperTypes.Regular
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular15x21)),
            //    Size = PaperSizes._15x21,
            //    Type = PaperTypes.Regular
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular20x25)),
            //    Size = PaperSizes._20x25,
            //    Type = PaperTypes.Regular
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular20x30)),
            //    Size = PaperSizes._20x30,
            //    Type = PaperTypes.Regular
            //});
            //priceList.Add(new PicturePrice
            //{
            //    Price = StringConverter.ToFloat(parameterCollection.GetValue(ParameterNames.PriceRegular30x45)),
            //    Size = PaperSizes._30x45,
            //    Type = PaperTypes.Regular
            //});


        }


        #region EVENT HANDLERS
        #endregion

    }
}
