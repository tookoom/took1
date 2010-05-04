using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Took1.Web.Cloud;
using Took1.Web.Cloud.Data;
//using Took1.Silverlight.LifeManager.Data.Model;
//using Took1.Silverlight.LifeManager.Data.Collection;
//using Took1.Silverlight.LifeManager.Data.Source;

namespace Took1.Silverlight.LifeManager.Control
{
    public partial class EventEditor : UserControl
    {
        #region PRIVATE MEMBERS
        private DataSource dataSource;
        #endregion
        #region PUBLIC PROPERTIES
        public DataSource DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                if (dataSource != null)
                {
                    setDataSourceEventHandlers();
                    intializeValues();
                }
            }
        }


        #endregion        


        public EventEditor(DataSource dataSource)
        {
            InitializeComponent();
            this.DataSource = dataSource;
        }

        private void intializeValues()
        {
            listBoxEvents.ItemsSource = dataSource.EventEntities;
            setCurrentEvent(dataSource.CurrentValue.Event);
        }
        private void setCurrentEvent(Event ev)
        {
            contentControlEvent.Content = ev;
            if (listBoxEvents.Items.Contains(ev))
            {
                listBoxEvents.SelectedItem = ev;
                listBoxEvents.ScrollIntoView(ev);
            }
        }
        private void setDataSourceEventHandlers()
        {
            dataSource.CurrentValue.EventChanged += new EventHandler(dataSource_CurrentValue_EventChanged);
        }


        #region DATA SOURCE EVENT HANDLERS
        void dataSource_CurrentValue_EventChanged(object sender, EventArgs e)
        {
            if (dataSource != null)
                setCurrentEvent(dataSource.CurrentValue.Event);
        }

        #endregion
        #region UI EVENT HANDLERS
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxEvents.SelectedItem != null)
            {
                Event ev = listBoxEvents.SelectedItem as Event;
                if (dataSource.EventEntities.Contains(ev))
                {
                    dataSource.EventEntities.Remove(ev);
                    dataSource.CurrentValue.Event = null;
                }
            }
        }
        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            Event ev = new Event() { Name = "Novo Evento", RegistryTimestamp = DateTime.Now, EventTimeStamp = DateTime.Now };
            dataSource.EventEntities.Add(ev);
            dataSource.CurrentValue.Event = ev;
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var source = listBoxEvents.ItemsSource;
            listBoxEvents.ItemsSource = null;
            listBoxEvents.ItemsSource = source;
        }
        private void listBoxEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null)
            {
                if (e.AddedItems.Count > 0)
                {
                    Event ev = e.AddedItems[0] as Event;
                    contentControlEvent.Content = ev;
                }
            }
        }

        #endregion
    }
}
