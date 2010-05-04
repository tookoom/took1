using System;
using System.Collections.Generic;
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

namespace TK1.PicDeveloper
{
	/// <summary>
	/// Interaction logic for ProgressReporter.xaml
	/// </summary>
	public partial class ProgressReporter
	{
        #region PRIVATE MEMBERS
        int maxProgress;

        #endregion
        #region PUBLIC MEMBERS
        public string ProgressMessage
        {
            get { return ""; }
        }
        public int MaxProgress
        {
            get { return maxProgress; }
            set
            {
                maxProgress = value;
                if (maxProgress > 0)
                {
                    progressBar.Maximum = maxProgress;
                    progressBar.Visibility = Visibility.Visible;
                }
                else
                {
                    progressBar.Visibility = Visibility.Collapsed;
                }
            }
        }
        public int CurrentProgress
        {
            get;
            set;
        }

        #endregion

		public ProgressReporter()
		{
			this.InitializeComponent();
            MaxProgress = 0;
            CurrentProgress = 0;
		}
        public void SetCurrentProgress(int currentProgress)
        {
            CurrentProgress = currentProgress;
            if (MaxProgress > 0)
            {
                textBlockProgress.Text = string.Format("{0}/{1}", CurrentProgress, MaxProgress);
            }
            else
            {
                textBlockProgress.Text = string.Format("{0}", CurrentProgress);
            }
            progressBar.Value = currentProgress;
        }

	}
}