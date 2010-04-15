using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Took1.Net;
using Took1.Data;
using Took1.Data.Presentation;

namespace Took1.MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region DELEGATES
        delegate void OutputMessageCallback(OutputMessage outputMessage);

        #endregion
        #region PRIVATE MEMBERS
		private NetworkCredential credential;
        private SmtpClient smtpClient;
        private OutputMessageCollection outputMessageCollection;
 
	    #endregion

        public MainWindow()
        {
            InitializeComponent();

            intialize();
        }

        private void intialize()
        {
            outputMessageCollection = new OutputMessageCollection();
            outputControl.ItemsSource = outputMessageCollection;

            writeError("erro");
            writeInfo("info");
            writeOutput("output");
            writeWarning("warning");

            credential = new NetworkCredential()
            {
                Domain = "",
                Password = "14351623",
                UserName = "andre.v.mattos@gmail.com"
            };
            contentCredential.Content = credential;

            smtpClient = new SmtpClient()
            {
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587
            };
            contentSmtpClient.Content = smtpClient;

            checkBoxAuthenticationRequired.IsChecked = true;

            textBoxMessageContent.Text = "This e-mail was sended @ " + DateTime.Now.ToLongTimeString();
            textBoxMailRecipient.Text = "andre.matos@numericon.com.br";
            textBoxMailSubject.Text = "Teste de envio de e-mail";
            textBoxSenderMail.Text = "andre.v.mattos@gmail.com";
            textBoxSenderName.Text = "André";

        }


        private void sendMail()
        {
            try
            {
                writeOutput("Sending e-mail");
                string message = textBoxMessageContent.Text;
                string subject = textBoxMailSubject.Text;
                string recipient = textBoxMailRecipient.Text;
                string senderMail = textBoxSenderMail.Text;
                string senderName = textBoxSenderName.Text;

                bool isHtml = checkBoxMessageIsHtml.IsChecked == null ? false : checkBoxMessageIsHtml.IsChecked.Value;

                smtpClient.Credential = credential;

                MailMessage.Send(senderMail, recipient, subject, message, isHtml, smtpClient);
                writeOutput("E-mail Sended");
            }
            catch (Exception exception)
            {
                writeError(string.Format("#### Exception:\n{0}", exception));
            }
        }
        private void setAuthenticationVisibility()
        {
            if (checkBoxAuthenticationRequired.IsChecked.HasValue)
            {
                if (checkBoxAuthenticationRequired.IsChecked.Value)
                    groupBoxNetworkCredential.IsEnabled = true;
                else
                    groupBoxNetworkCredential.IsEnabled = false;
            }
        }
        private void setOutputSource()
        {
            //var query = from el in outputMessageCollection select el;
            //if (!ShowError) query = from el in query
            //                        where el.OutputMessageType != OutputMessageTypes.Error
            //                        select el;
            //if (!ShowEvent) query = from el in query
            //                        where el.OutputMessageType != OutputMessageTypes.Event
            //                        select el;
            //if (!ShowInfo) query = from el in query
            //                       where el.OutputMessageType != OutputMessageTypes.Info
            //                       select el;
            //if (!ShowVerbose) query = from el in query
            //                          where el.OutputMessageType != OutputMessageTypes.Verbose
            //                          select el;
            //if (!ShowWarning) query = from el in query
            //                          where el.OutputMessageType != OutputMessageTypes.Warning
            //                          select el;
            //if (itemsControlOutput != null)
            //    itemsControlOutput.ItemsSource = query.ToList();
        }
        private void writeError(string message)
        {
            writeOutput(new OutputMessage()
            {
                Data = message,
                OutputMessageType = OutputMessageTypes.Error,
                Source = "",
                Timestamp = DateTime.Now
            });
        }
        private void writeInfo(string message)
        {
            writeOutput(new OutputMessage()
            {
                Data = message,
                OutputMessageType = OutputMessageTypes.Info,
                Source = "",
                Timestamp = DateTime.Now
            });
        }
        private void writeOutput(string message)
        {
            writeOutput(new OutputMessage()
            {
                Data = message,
                OutputMessageType = OutputMessageTypes.Verbose,
                Source = "",
                Timestamp = DateTime.Now
            });
        }
        private void writeWarning(string message)
        {
            writeOutput(new OutputMessage()
            {
                Data = message,
                OutputMessageType = OutputMessageTypes.Warning,
                Source = "",
                Timestamp = DateTime.Now
            });
        }
        private void writeOutput(OutputMessage outputMessage)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (outputMessage != null)
                    outputMessageCollection.Add(outputMessage);
                setOutputSource();
            }
            else
            {
                OutputMessageCallback callback = new OutputMessageCallback(writeOutput);
                this.Dispatcher.Invoke(callback, outputMessage);
            }
        }


        #region UI EVENT HANDLERS
        private void buttonMessageLoad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }
        private void buttonMessageClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            textBoxMessageContent.Text = string.Empty;
        }
        private void buttonSendMail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            sendMail();
        }
        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            setAuthenticationVisibility();
        }
        private void CheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            setAuthenticationVisibility();
        }

        private void outputControl_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (outputMessageCollection != null)
                outputMessageCollection.Clear();
        }


	    #endregion   
    }
}
