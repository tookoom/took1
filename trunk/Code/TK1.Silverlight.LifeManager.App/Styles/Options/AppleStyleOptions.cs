using System.Windows.Controls;
using System.Windows;
using DevExpress.AgMenu;
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Took1.Silverlight.LifeManager.App.Styles {
	public partial class AppleStyle : UserControl, IDemoItem {
		IDemoItemOwner owner;
		Panel optionsPanel;
		DataTemplate IDemoItem.OptionsPanelTemplate { get { return (DataTemplate)Resources["OptionsPanelTemplate"]; } }
		IDemoItemOwner IDemoItem.Owner { set { this.owner = value; } }
		string IDemoItem.XAMLBlock { get { return "demos/applestyle.xaml"; } }
		string IDemoItem.XMLBlock { get { return null; } }
		string IDemoItem.CSBlock { get { return "Demos.AppleStyle.xaml.cs"; } }

		Slider Slide(string name) { return (optionsPanel == null ? null : optionsPanel.FindName(name)) as Slider; }
		ToggleButton Check(string name) { return (optionsPanel == null ? null : optionsPanel.FindName(name)) as ToggleButton; }
		ToggleButton Check(object name) { return name as ToggleButton; }
		bool IsCheck(object name) {
			ToggleButton check = Check(name);
			return check == null ? false : check.IsChecked.Value;
		}
		bool IsCheck(string name) {
			ToggleButton check = Check(name);
			return check == null ? false : check.IsChecked.Value;
		}

		private void ApplyOptions() {
			if(Slide("mag") != null && Slide("scale") != null) {
				AgApplePanel.MaxDiff = Slide("mag").Value;
				AgApplePanel.MagWidth = Slide("magWidth").Value;
				menu1.RenderTransformOrigin = new Point(0.5, 0.5);
				menu1.RenderTransform = new ScaleTransform() { ScaleX = Slide("scale").Value, ScaleY = Slide("scale").Value, CenterX=0.5, CenterY=0.5 };
			}

		}
		private void OptionsPanel_Loaded(object sender, RoutedEventArgs e) {
			optionsPanel = sender as Panel;
			CopyOptions();
			ApplyOptions();
		}
		private void CopyOptions() {
			if(oldOptionsPanel == null) return;
			Slide("scale").Value = ((Slider)oldOptionsPanel.FindName("scale")).Value;
			Slide("mag").Value = ((Slider)oldOptionsPanel.FindName("mag")).Value;
			Slide("magWidth").Value = ((Slider)oldOptionsPanel.FindName("magWidth")).Value;
		}
		private void scale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			ApplyOptions();
		}
	}


}
