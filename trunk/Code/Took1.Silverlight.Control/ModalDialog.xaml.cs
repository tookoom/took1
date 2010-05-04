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

namespace Numericon.Web.Silverlight.Controls
{
    /// <summary>
    /// Controle para exibição de outros controle em modo "modal" no ambiente de
    /// NCWebAdmin (cobre restante doubleAnimationUsingKeyFrames janela com branco semi-transparente)
    /// </summary>
    public partial class ModalDialog : UserControl
    {
        #region PUBLIC PROPERTIES
        public Rectangle BackgroundRectangle
        {
            get { return backgroundRectangle; }
        }
        public bool IsVisible
        {
            get { return this.Visibility == Visibility.Visible; }
            set { this.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }
        public object Content
        {
            get { return contentPresenter.Content; }
            set { contentPresenter.Content = value; }
        }
        public ModalDialog()
        {
            InitializeComponent();
        }

        #endregion
        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }
        public void Hide()
        {
            this.Visibility = Visibility.Collapsed;
        }


    }


}
