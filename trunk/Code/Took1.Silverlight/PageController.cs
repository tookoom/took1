using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Took1.Silverlight
{
    public class PageController
    {
        #region PRIVATE MEMBERS
        UserControl userControl;

        double oldHeight;
        double oldWidth;
        double currentWidth;
        double currentHeight;
        double minWidth = 940;
        double minHeigth = 600;

        #endregion
        public bool IsResizable { get; set; }

        public PageController(UserControl userControl)
        {
            Application.Current.Host.Content.Resized += new EventHandler(content_Resized);
            this.userControl = userControl;
            IsResizable = true;
        }

        private void content_Resized(object sender, EventArgs e)
        {
            if (IsResizable)
            {
                currentWidth = Application.Current.Host.Content.ActualWidth;
                currentHeight = Application.Current.Host.Content.ActualHeight;

                if (currentWidth > minWidth)
                    userControl.Width = currentWidth;
                else userControl.Width = minWidth;

                if (currentHeight > minHeigth)
                    userControl.Height = currentHeight;
                else userControl.Height = minHeigth;
            }
        }
    }
}
