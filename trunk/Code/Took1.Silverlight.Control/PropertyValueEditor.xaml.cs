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
using System.Collections;
using System.Reflection;
using Numericon.Reflection;
using Numericon.Web.Silverlight.Controls;

namespace Numericon.Web.Silverlight.Control
{
    public partial class PropertyValueEditor : UserControl
    {
        #region CONST MEMBERS
        const string COMBO_BOX_ITEM_STYLE = "NCComboBoxItemStyle";
        const string COMBO_BOX_STYLE = "NCComboBoxStyle";
        const string EXPANDER_STYLE = "NCExpanderStyle";
        const string EXPANDER_STYLE_NO_BUTTON = "NCExpanderStyleNoButton";
        const string NUMERIC_UP_DOWN_STYLE = "NCNumericUpDownStyle";
        const string TEXT_BLOCK_STYLE = "NCTextBlockStyle";
        const string TEXT_BOX_STYLE = "NCTextBoxStyle";

        #endregion
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
        #endregion
        #region PRIVATE MEMBERS
        bool isCollection = false;
        string propertyName;
        object propertyValue;
        object source;


        #endregion
        #region PUBLIC PROPERTIES
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; setPropertyName(); }
        }
        public object PropertyValue
        {
            get { return propertyValue; }
            set { propertyValue = value; setPropertyValue(); }
        }
        public object Source
        {
            get { return source; }
            set { source = value; }
        }

        #endregion

        public PropertyValueEditor()
        {
            InitializeComponent();
        }

        private void addCollectionItem()
        {
            IList list = propertyValue as IList;
            if (list != null)
            {
                Type itemType = null;
                foreach (object obj in list)
                {
                    itemType = obj.GetType();
                    break;
                }
                if (itemType != null)
                {
                    object newItem = System.Activator.CreateInstance(itemType);
                    list.Add(newItem);
                    addPropertyEditor(newItem);
                }
            }
        }
        private void addPropertyEditor(object obj)
        {
            PropertyEditor propertyEditor = new PropertyEditor();
            propertyEditor.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyEditor_PropertyValueChanged);
            if (TypeCheck.IsCollection(propertyValue))
            {
                propertyEditor.ItemRemoved += new EventHandler(propertyEditor_ItemRemoved);
                propertyEditor.CanRemove = true;
            }
            propertyEditor.Source = obj;
            stackPanelPropertyEditor.Children.Add(propertyEditor);
        }

        private void removeCollectionItem(object item)
        {
            IList list = propertyValue as IList;
            if (list != null)
            {
                if (list.Contains(item))
                    list.Remove(item);
            }
            showCollectionPropertyEditor();
        }

        private void setPropertyName()
        {
            if (propertyName == null)
                textBlockName.Text = "[NULL]";
            else
                textBlockName.Text = propertyName;

        }
        private void setPropertyValue()
        {
            gridPropertyEditor.Visibility = Visibility.Collapsed;
            Style style = Application.Current.Resources[EXPANDER_STYLE_NO_BUTTON] as Style;

            if (propertyValue == null)
            {
                showNullPropertyEditor();
            }
            else
            {
                string propertyTypeName = propertyValue.GetType().ToString();

                if (TypeCheck.IsCollection(propertyValue))
                {
                    isCollection = true;
                    buttonCollectionItemAdd.Visibility = Visibility.Visible;

                    showCollectionPropertyEditor();
                    style = Application.Current.Resources[EXPANDER_STYLE] as Style;
                }
                else
                {
                    isCollection = false;
                    buttonCollectionItemAdd.Visibility = Visibility.Collapsed;

                    if (TypeCheck.IsString(propertyValue))
                        showStringPropertyEditor();

                    else if (TypeCheck.IsNumber(propertyValue))
                        showNumericPropertyEditor();

                    else if (TypeCheck.IsBool(propertyValue))
                        showBooleanPropertyEditor();

                    else if (TypeCheck.IsEnum(propertyValue))
                        showEnumPropertyEditor();

                    else
                    {
                        showDefaultPropertyEditor();
                        gridPropertyEditor.Visibility = Visibility.Visible;
                        style = Application.Current.Resources[EXPANDER_STYLE] as Style;
                        addPropertyEditor(propertyValue);
                    }
                }
            }
            expanderContent.Style = style;
        }
        
        private void showBooleanPropertyEditor()
        {
            Style comboBoxStyle = Application.Current.Resources[COMBO_BOX_STYLE] as Style;
            Style itemContainerStyle = Application.Current.Resources[COMBO_BOX_ITEM_STYLE] as Style;

            ComboBox comboBox = new ComboBox()
            {
                ItemsSource = new List<bool> { true, false },
                SelectedItem = propertyValue as bool?,
                Style = comboBoxStyle,
                ItemContainerStyle = itemContainerStyle
            };
            comboBox.SelectionChanged += new SelectionChangedEventHandler(comboBox_SelectionChanged);
            gridValue.Children.Add(comboBox);

        }
        private void showCollectionPropertyEditor()
        {
            TextBlock textBlock = new TextBlock() { Text = "[Collection]" };
            gridValue.Children.Add(textBlock);
            gridPropertyEditor.Visibility = Visibility.Visible;

            IEnumerable collection = propertyValue as IEnumerable;
            if (collection != null)
            {
                gridPropertyEditor.Visibility = Visibility.Visible;
                stackPanelPropertyEditor.Children.Clear();
                foreach (object obj in collection)
                    addPropertyEditor(obj);
            }
        }
        private void showDefaultPropertyEditor()
        {
            TextBlock textBlock = new TextBlock() { Text = propertyValue.ToString() };
            gridValue.Children.Add(textBlock);
        }
        private void showEnumPropertyEditor()
        {
            Style comboBoxStyle = Application.Current.Resources[COMBO_BOX_STYLE] as Style;
            Style itemContainerStyle = Application.Current.Resources[COMBO_BOX_ITEM_STYLE] as Style;

            var fields = propertyValue.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            List<object> items = new List<object>();
            foreach (var item in fields)
                items.Add(item.Name);
            ComboBox comboBoxEnum = new ComboBox()
            {
                ItemsSource = items,
                Style = comboBoxStyle,
                ItemContainerStyle = itemContainerStyle
            };
            comboBoxEnum.SelectionChanged += new SelectionChangedEventHandler(comboBoxEnum_SelectionChanged);
            if (comboBoxEnum.Items.Cast<string>().Contains(propertyValue.ToString()))
                comboBoxEnum.SelectedItem = propertyValue.ToString();
            gridValue.Children.Add(comboBoxEnum);
        }
        private void showNullPropertyEditor()
        {
            TextBlock textBlock = new TextBlock() { Text = "[NULL]" };
            gridValue.Children.Add(textBlock);
        }
        private void showNumericPropertyEditor()
        {
            Style style = Application.Current.Resources[NUMERIC_UP_DOWN_STYLE] as Style;
            
            double value = 0;
            double.TryParse(propertyValue.ToString(), out value);
            NumericUpDown numericUpDown = new NumericUpDown() { Value = value, Style = style };
            numericUpDown.ValueChanged += new RoutedPropertyChangedEventHandler<double>(numericUpDown_ValueChanged);
            gridValue.Children.Add(numericUpDown);
        }
        private void showStringPropertyEditor()
        {
            Style style = Application.Current.Resources[TEXT_BOX_STYLE] as Style;
            
            TextBox textBox = new TextBox() { Text = propertyValue.ToString(), Style = style };
            textBox.TextChanged += new TextChangedEventHandler(textBox_TextChanged);
            gridValue.Children.Add(textBox);
        }
        
        #region UI EVENT HANDLERS
        private void buttonCollectionItemAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            addCollectionItem();
        }
        
        void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null & source != null)
            {
                PropertyInfo propertyInfo = source.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(source, comboBox.SelectedItem, null);
                }
            }
            onPropertyValueChanged(new PropertyValueChangedEventArgs()
            {
                NewValue = comboBox == null ? null : comboBox.SelectedItem,
                OldValue = e == null ? null : e.OriginalSource,
                PropertyName = propertyName,
                Source = source
            });
        }
        void comboBoxEnum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null & source != null)
            {
                PropertyInfo propertyInfo = source.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    object value = null;
                    foreach (var item in propertyValue.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
                    {
                        if (item.Name == comboBox.SelectedItem as string)
                            value = item.GetValue(source);
                    }
                    propertyInfo.SetValue(source, value, null);
                    //propertyValue = textBox.Text;
                }
            }
            onPropertyValueChanged(new PropertyValueChangedEventArgs()
            {
                NewValue = comboBox == null ? null : comboBox.SelectedItem,
                OldValue = e == null ? null : e.OriginalSource,
                PropertyName = propertyName,
                Source = source
            });
        }
        void numericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (numericUpDown != null & source != null)
            {
                PropertyInfo propertyInfo = source.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    object value = numericUpDown.Value;
                    if (propertyInfo.PropertyType == typeof(int))
                        value = (int)numericUpDown.Value;
                    if (propertyInfo.PropertyType == typeof(float))
                        value = (float)numericUpDown.Value;
                    propertyInfo.SetValue(source, value, null);
                    //propertyValue = textBox.Text;
                }
            }
            onPropertyValueChanged(new PropertyValueChangedEventArgs()
            {
                NewValue = numericUpDown == null ? -9999999 : numericUpDown.Value,
                OldValue = e == null ? null : e.OriginalSource,
                PropertyName = propertyName,
                Source = source
            });
        }
        void propertyEditor_ItemRemoved(object sender, EventArgs e)
        {
            PropertyEditor propertyEditor = sender as PropertyEditor;
            if (propertyEditor != null)
            {
                removeCollectionItem(propertyEditor.Source);
            }
        }
        void propertyEditor_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            onPropertyValueChanged(e);
        }
        void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null & source != null)
            {
                PropertyInfo propertyInfo = source.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(source, textBox.Text, null);
                    //propertyValue = textBox.Text;
                }
            }
            onPropertyValueChanged(new PropertyValueChangedEventArgs()
            {
                NewValue = textBox == null ? "[NULL]" : textBox.Text,
                OldValue = e == null ? null : e.OriginalSource,
                PropertyName = propertyName,
                Source = source
            });

        }


        #endregion

    }
}
