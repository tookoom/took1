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
using System.Reflection;
using TK1.Reflection;

namespace TK1.Controls
{
    /// <summary>
    /// Interaction logic for PropertyEditor.xaml
    /// </summary>
    public partial class PropertyEditor : UserControl
    {
        #region EVENTS
        public event PropertyValueChangedEventHandler PropertyValueChanged;
        private void onPropertyValueChanged(PropertyValueChangedEventArgs e)
        {
            PropertyValueChangedEventHandler handler = PropertyValueChanged;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }

        }

        public event EventHandler ItemRemoved;
        private void onItemRemoved(EventArgs e)
        {
            EventHandler handler = ItemRemoved;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }

        }
        #endregion
        #region PRIVATE MEMBERS
        bool canRemove;
        bool isExpanded;
        object source;

        #endregion
        #region PUBLIC PROPERTIES
        public bool CanRemove
        {
            get { return canRemove; }
            set
            {
                canRemove = value;
                setRemoveButtonVisibility();
            }

        }
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                expander.IsExpanded = value;
            }
        }
        public object Source
        {
            get { return source; }
            set { source = value; setProperties(); }
        }


        #endregion
        
        public PropertyEditor()
        {
            InitializeComponent();
        }

        public void Collapse()
        {
            expander.IsExpanded = false;
        }
        public void Expand()
        {
            expander.IsExpanded = true;
        }

        private void addCollection(object source)
        {
            if (source != null)
            {
                try
                {
                    PropertyValueEditor propertyValueEditor = new PropertyValueEditor()
                    {
                        PropertyName = "[Collection]",
                        PropertyValue = source,
                        Source = source
                    };
                    propertyValueEditor.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyValueEditor_PropertyValueChanged);
                    stackPanelProperties.Children.Add(propertyValueEditor);
                }
                catch (Exception exception) { }
            }
        }
        private void addProperty(PropertyInfo propertyInfo, object source)
        {
            if (propertyInfo != null & source != null)
            {
                try
                {
                    object value = propertyInfo.GetValue(source, null);
                    PropertyValueEditor propertyValueEditor = new PropertyValueEditor()
                    {
                        PropertyName = propertyInfo.Name,
                        PropertyValue = value,
                        Source = source
                    };
                    propertyValueEditor.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyValueEditor_PropertyValueChanged);
                    stackPanelProperties.Children.Add(propertyValueEditor);
                }
                catch (Exception exception) { }
            }
        }
        private void setProperties()
        {
            stackPanelProperties.Children.Clear();
            if (source == null)
            {
                textBlockPropertyName.Text = "[NULL]";
                buttonDelete.Visibility = Visibility.Collapsed;
                stackPanelProperties.Visibility = Visibility.Collapsed;
            }
            else
            {
                textBlockPropertyName.Text = source.GetType().Name;
                setRemoveButtonVisibility();
                stackPanelProperties.Visibility = Visibility.Visible;

                if (TypeCheck.IsCollection(source))
                {
                    addCollection(source);
                }
                else
                {
                    foreach (PropertyInfo propertyInfo in source.GetType().GetProperties())
                    {
                        addProperty(propertyInfo, source);
                    }
                }
            }
        }
        private void setRemoveButtonVisibility()
        {
            if (canRemove)
                buttonDelete.Visibility = Visibility.Visible;
            else
                buttonDelete.Visibility = Visibility.Collapsed;
        }

        #region EVENT HANDLERS

        private void buttonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            onItemRemoved(new EventArgs());
        }
        private void propertyValueEditor_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            onPropertyValueChanged(e);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

    }
}
