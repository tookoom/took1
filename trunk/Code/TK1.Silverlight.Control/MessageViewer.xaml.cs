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
using Numericon.Web.Silverlight.Collection;
using Numericon.Data.Presentation;

namespace Numericon.Web.Silverlight.Control
{
    public partial class MessageViewer : UserControl
    {
        #region PRIVATE MEMBERS
        private int currentMessageIndex = 0;
        private int currentMessageCount = 0;
        private OutputMessage currentMessage;
        private OutputMessageCollection messageCollection;

        #endregion
        #region PUBLIC PROPERTIES
        public OutputMessage CurrentMessage
        {
            get { return currentMessage; }
            set
            {
                if (messageCollection.Contains(value))
                    setCurrentMessage(value);
                else
                    messageCollection.Add(value);
            }
        }
        public OutputMessageCollection MessageCollection { get { return messageCollection; } }
        
        #endregion

        public MessageViewer()
        {
            InitializeComponent();
            messageCollection = new OutputMessageCollection();
            messageCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(messageCollection_CollectionChanged);

            setCurrentMessage(null);
        }

        private void ordenateMessageCollection()
        {
            //throw new NotImplementedException();
        }
        private void removeMessage(OutputMessage message)
        {
            if (message != null)
            {
                if (messageCollection.Contains(message))
                    messageCollection.Remove(message);
                showLastMessage();
            }
        }
        private void setBackgroundColor(OutputMessageTypes? outputMessageType)
        {
            Color color = new Color() { A = 0xFF, R = 0xE1, G = 0xE1, B = 0xE1 };

            if (outputMessageType != null)
            {
                switch (outputMessageType)
                {
                    case OutputMessageTypes.Error:
                        color = new Color() { A= 0xFF, R= 0xFD, G = 0x79, B = 0x53 };
                        break;
                    case OutputMessageTypes.Info:
                        color = new Color() { A= 0xFF, R= 0xC4, G = 0xE0, B = 0xFF };
                        break;
                    case OutputMessageTypes.Warning:
                        color = new Color() { A= 0xFF, R= 0xF3, G = 0xFB, B = 0xBB };
                        break;
                    default:
                        break;
                }

            }
            background.Fill = new SolidColorBrush(color);

        }
        private void setCurrentMessage(OutputMessage message)
        {
            currentMessage = message;
            currentMessageIndex = 0;
            if (currentMessage == null)
            {
                this.Visibility = Visibility.Collapsed;
                textBlockHeader.Text = null;
                textBlockMessage.Text = null;
                setBackgroundColor(null);
                setIcon(null);
            }
            else
            {
                this.Visibility = Visibility.Visible;
                textBlockHeader.Text = message.Caption;
                textBlockMessage.Text = message.Data;
                setBackgroundColor(currentMessage.OutputMessageType);
                setIcon(currentMessage.OutputMessageType);

                foreach (OutputMessage outputMessage in messageCollection)
                {
                    currentMessageIndex++;
                    if (outputMessage == currentMessage)
                        break;
                }
            }
            setIndex();
        }
        private void setIcon(OutputMessageTypes? outputMessageType)
        {
            iconDefault.Visibility = Visibility.Collapsed;
            iconError.Visibility = Visibility.Collapsed;
            iconInfo.Visibility = Visibility.Collapsed;
            iconWarning.Visibility = Visibility.Collapsed;

            if (outputMessageType != null)
            {
                switch (outputMessageType)
                {
                    case OutputMessageTypes.Error:
                        iconError.Visibility = Visibility.Visible;
                        break;
                    case OutputMessageTypes.Info:
                        iconInfo.Visibility = Visibility.Visible;
                        break;
                    case OutputMessageTypes.Warning:
                        iconWarning.Visibility = Visibility.Visible;
                        break;
                    default:
                        iconDefault.Visibility = Visibility.Visible;
                        break;
                }

            }
        }
        private void setIndex()
        {
            textBlockMessageIndex.Text = string.Format("{0}/{1}", currentMessageIndex, currentMessageCount); ;
        }
        private void showLastMessage()
        {
            OutputMessage message = null;
            if (messageCollection != null)
            {
                if (messageCollection.Count > 0) 
                    message = messageCollection[messageCollection.Count - 1];
            }
            setCurrentMessage(message);
        }
        private void showNextMessage()
        {
            bool showNext = false;
            foreach (OutputMessage message in messageCollection)
            {
                if (showNext)
                {
                    setCurrentMessage(message);
                    break;
                }
                if (message == currentMessage)
                    showNext = true;
            }
        }
        private void showPreviousMessage()
        {
            OutputMessage previousMessage = null;
            foreach (OutputMessage message in messageCollection)
            {
                if (message == currentMessage)
                {
                    if(previousMessage != null)
                        setCurrentMessage(previousMessage);
                    break;
                }
                previousMessage = message;
            }
        }


        #region EVENT HANDLERS
        void messageCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ordenateMessageCollection();
            if (e != null)
            {
                if (e.NewItems != null)
                {
                    showLastMessage();
                }
                if (e.OldItems != null)
                {
                    if (currentMessage != null)
                        if (e.OldItems.Contains(currentMessage))
                            showLastMessage();
                }
            }

            buttonNextMessage.IsEnabled = messageCollection.Count > 1;
            buttonPreviousMessage.IsEnabled = messageCollection.Count > 1;
            currentMessageCount = messageCollection.Count;
            setIndex();
        }


        
        #endregion

        #region UI EVENT HANDLERS
        private void buttonPreviousMessage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            showPreviousMessage();
        }
        private void buttonNextMessage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            showNextMessage();
        }
        private void buttonDeleteMessage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            removeMessage(currentMessage);
        }


        #endregion
    }
}
