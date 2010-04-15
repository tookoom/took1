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

namespace Took1.CarControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool SearchFromFocus = false;
        bool SearchToFocus = false;
        bool ChangesMade = false;
        int ActiveId = -1;

        AMXmlDatabase db;
        List<AMFuelEntry> ActiveFuelEntryList = new List<AMFuelEntry>();
        public MainWindow()
        {
            InitializeComponent();
            comboBoxFuelType.Items.Add("GNV");
            comboBoxFuelType.Items.Add("Gasolina");
            comboBoxFuelType.SelectedIndex = 0;
            textBlockFuelUnitName.Text = "m3";
            textBoxDay.Text = DateTime.Now.Day.ToString();
            textBoxMonth.Text = DateTime.Now.Month.ToString();
            textBoxYear.Text = DateTime.Now.Year.ToString();
            textBoxSearchFrom.Text = "0";

            try
            {
                db = new AMXmlDatabase();
                textBoxSearchTo.Text = (db.FuelEntryList.Count-1).ToString();
                FillFuelData();
                if (db.FuelEntryList.Count>0)
                    textBoxFuelUnitValue.Text = db.FuelEntryList.Last().UnitValue.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "EXCEPTION in MainWindow");
            }
            if (db.ErrorState)
                MessageBox.Show(db.ErrorMessage, "ERROR in AMXmlDataBase initialization");

            FuelStats(0, db.FuelEntryList.Count() - 1);
        }

        private void ClearFuelFields()
        {
            //textBlockFuelUnitName.Text = "";
            textBoxFuelKmMark.Text = "";
            textBoxFuelNote.Text = "";
            textBoxFuelQuant.Text = "";
            textBoxFuelTotalValue.Text = "";
            //textBoxFuelUnitValue.Text = "";
        }
        private void DeleteFuelEntry()
        {
            if (ActiveId < 0) return;

            if (comboBoxFuelType.SelectedIndex < 0) comboBoxFuelType.SelectedIndex = 0;
            if (textBoxFuelQuant.Text == "") textBoxFuelQuant.Text = "0";
            if (textBoxFuelUnitValue.Text == "") textBoxFuelUnitValue.Text = "0";
            if (textBoxFuelTotalValue.Text == "") textBoxFuelTotalValue.Text = "0";

            int Id = ActiveId;

            var Query = from el in db.FuelEntryList
                        where el.Id == ActiveId
                        select el;

            if (Query.Count() > 0)
            {
                AMFuelEntry f = Query.First();
                db.FuelEntryList.Remove(f);
            }
            ClearFuelFields();
            ActiveId = -1;
            ChangesMade = true;
            FillFuelData();
            FuelStats(0, db.FuelEntryList.Count());
        }
        private void FillFuelData()
        {
            ActiveFuelEntryList.Clear();
            foreach (AMFuelEntry f in db.FuelEntryList)
            {
                ActiveFuelEntryList.Add(f);
            }
            ShowFuelData();
        }
        private void FuelStats(int SearchFrom, int SearchTo)
        {
            float SumGnvQty = 1;
            float SumGasQty = 0;
            float SumMoney = 1;
            float Km = 1;

            string ret = Environment.NewLine;
            string aux = "(no período da pesquisa)" + ret;

            if (SearchFrom >= SearchTo)
            {
                textBoxOutput.Text = "SearchFrom >= SearchTo";
                return;
            }

            /*KM*/
            var KmQuery = from el in db.FuelEntryList
                          where el.Id >= SearchFrom & el.Id <= SearchTo
                          select el.KmMark;
            if (KmQuery.Count() > 0)
                Km = KmQuery.Last() - KmQuery.First();

            /*GNV*/
            var GnvQuantQuery = from el in db.FuelEntryList
                             where el.Id > SearchFrom & el.Id <= SearchTo & el.Type=="GNV"
                             select el.Quantity;
            if (GnvQuantQuery.Count() > 0)
            {
                SumGnvQty = GnvQuantQuery.Sum();
                aux += String.Format("GNV: \n");
                aux += String.Format("- {0:N} m3 \n",SumGnvQty);
                aux += String.Format("- Consumo = {0:N} km/m3 \n", Km / SumGnvQty);
            }

            /*GASOLINA*/
            var GasQuantQuery = from el in db.FuelEntryList
                                where el.Id > SearchFrom & el.Id <= SearchTo & el.Type == "Gasolina"
                                select el.Quantity;
            if (GasQuantQuery.Count() > 0)
            {
                SumGasQty = GasQuantQuery.Sum();
                aux += String.Format("Gasolina: \n");
                aux += String.Format("- {0:N} L \n", SumGasQty);
                aux += String.Format("- Consumo = {0:N} km/L \n", Km / SumGasQty);
            }

            /*DINHEIRO*/
            var MoneyQuery = from el in db.FuelEntryList
                                where el.Id > SearchFrom & el.Id <= SearchTo
                                select el.TotalValue;
            if (MoneyQuery.Count() > 0)
            {
                SumMoney = MoneyQuery.Sum();
                aux += String.Format("Total: \n");
                aux += String.Format("- Distância: {0:N} km \n", Km);
                aux += String.Format("- Gasto: {0:N} R$ \n", SumMoney);
                aux += String.Format("- Consumo: {0} R$/km \n", SumMoney / Km);
            }

            textBoxOutput.Text = aux;

            var ListQuery = from el in db.FuelEntryList
                            where el.Id >= SearchFrom & el.Id <= SearchTo
                            select el;

            ActiveFuelEntryList.Clear();
            foreach (AMFuelEntry f in ListQuery)
            {
                ActiveFuelEntryList.Add(f);
            }
            ShowFuelData();
        }
        private int GetIdFromString(string Line)
        {
            //MessageBox.Show(Line);
            int Start = 0;
            for (int i = 0; i < Line.Length; i++)
            {
                if (Line[i] != ' ') { Start = i; break; }
            }
            Line = Line.Substring(Start);
            Line = Line.Substring(0, Line.IndexOf("\t"));
            //MessageBox.Show("#" + Line + "#");
            return int.Parse(Line);
        }
        private void ShowFuelData()
        {
            listBoxData.Items.Clear();
            var Query = from el in ActiveFuelEntryList
                        orderby el.Id descending
                        select el;

            foreach (AMFuelEntry f in Query)
            {
                string aux = String.Format("{8,3}\t{0} \t {1:N} {2} \t {4:N} [{3:N}R$/{2}] \t {5:d} \t {6}km \t {7}", f.Type, f.Quantity, f.UnitName, f.UnitValue, f.TotalValue, f.TimeStamp, f.KmMark, f.Note, f.Id);
                listBoxData.Items.Add(aux);
            }
        }
        private void ShowFuelEntry(int Id)
        {
            ActiveId = Id;
            AMFuelEntry f = new AMFuelEntry();
            var Query = from el in db.FuelEntryList
                        where el.Id == Id
                        select el;
            if (Query.Count() > 0) f = Query.First();

            textBlockFuelUnitName.Text = f.UnitName;
            textBoxFuelKmMark.Text = f.KmMark.ToString();
            textBoxFuelNote.Text = f.Note;
            textBoxFuelQuant.Text = f.Quantity.ToString();
            textBoxFuelTotalValue.Text = f.TotalValue.ToString();
            textBoxFuelUnitValue.Text = f.UnitValue.ToString();
            textBoxDay.Text = f.TimeStamp.Day.ToString();
            textBoxMonth.Text = f.TimeStamp.Month.ToString();
            textBoxYear.Text = f.TimeStamp.Year.ToString();
            if (f.Type == "GNV") comboBoxFuelType.SelectedIndex = 0;
            else comboBoxFuelType.SelectedIndex = 1;
        }
        private void UpdateFuelEntry()
        {
            if (ActiveId < 0) return;

            if (comboBoxFuelType.SelectedIndex < 0) comboBoxFuelType.SelectedIndex = 0;
            if (textBoxFuelQuant.Text == "") textBoxFuelQuant.Text = "0";
            if (textBoxFuelUnitValue.Text == "") textBoxFuelUnitValue.Text = "0";
            if (textBoxFuelTotalValue.Text == "") textBoxFuelTotalValue.Text = "0";
            if (textBoxDay.Text == "") textBoxDay.Text = DateTime.Now.Day.ToString();
            if (textBoxMonth.Text == "") textBoxMonth.Text = DateTime.Now.Month.ToString();
            if (textBoxYear.Text == "") textBoxYear.Text = DateTime.Now.Year.ToString();
            int Id = ActiveId;

            DateTime Dt;
            try
            {
                Dt = new DateTime(int.Parse(textBoxYear.Text), int.Parse(textBoxMonth.Text), int.Parse(textBoxDay.Text));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception in DateTime");
                return;
            }

            var Query = from el in db.FuelEntryList
                        where el.Id == ActiveId
                        select el;

            if (Query.Count() >0)
            {
                AMFuelEntry f = Query.First();
                f.Id = Id;
                f.Type = (string)comboBoxFuelType.SelectedItem;
                f.Quantity = float.Parse(textBoxFuelQuant.Text);
                f.UnitName = textBlockFuelUnitName.Text;
                f.UnitValue = float.Parse(textBoxFuelUnitValue.Text);
                f.TotalValue = float.Parse(textBoxFuelTotalValue.Text);
                f.TimeStamp = Dt;
                f.KmMark = Convert.ToInt32(textBoxFuelKmMark.Text);
                f.Note = textBoxFuelNote.Text;
            }
            
            ChangesMade = true;
            FillFuelData();
            FuelStats(0, db.FuelEntryList.Count() - 1);
        }

        private void buttonFuelAdd_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxFuelType.SelectedIndex < 0) comboBoxFuelType.SelectedIndex = 0;
            if (textBoxFuelQuant.Text == "") textBoxFuelQuant.Text = "0";
            if (textBoxFuelUnitValue.Text == "") textBoxFuelUnitValue.Text = "0";
            if (textBoxFuelTotalValue.Text == "") textBoxFuelTotalValue.Text = "0";
            if (textBoxFuelKmMark.Text == "") textBoxFuelKmMark.Text = "0";
            if (textBoxDay.Text == "") textBoxDay.Text = DateTime.Now.Day.ToString();
            if (textBoxMonth.Text == "") textBoxMonth.Text = DateTime.Now.Month.ToString();
            if (textBoxYear.Text == "") textBoxYear.Text = DateTime.Now.Year.ToString();


            int Id = db.FuelEntryList.Count();
            DateTime Dt;
            try
            {
                Dt = new DateTime(int.Parse(textBoxYear.Text), int.Parse(textBoxMonth.Text), int.Parse(textBoxDay.Text));
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Exception in DateTime");
                return;
            }
            db.FuelEntryList.Add(new AMFuelEntry
            {
                Id = Id,
                Type = (string)comboBoxFuelType.SelectedItem,
                Quantity = float.Parse(textBoxFuelQuant.Text),
                UnitName = textBlockFuelUnitName.Text,
                UnitValue = float.Parse(textBoxFuelUnitValue.Text),
                TotalValue = float.Parse(textBoxFuelTotalValue.Text),
                TimeStamp = Dt,
                KmMark = Convert.ToInt32(textBoxFuelKmMark.Text),
                Note = textBoxFuelNote.Text
            });
            ChangesMade = true;
            FillFuelData();
            FuelStats(0, db.FuelEntryList.Count() - 1);
            ClearFuelFields();
        }
        private void buttonFuelDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteFuelEntry();

        }
        private void buttonFuelFill_Click(object sender, RoutedEventArgs e)
        {
            FillFuelData();

            int SearchFrom;
            int SearchTo;

            try
            {
                SearchFrom = 0;
                SearchTo = ActiveFuelEntryList.Count-1;
                textBoxSearchFrom.Text = SearchFrom.ToString();
                textBoxSearchTo.Text = SearchTo.ToString();
            }
            catch (Exception ex)
            {
                textBoxOutput.Text = ex.Message;
                return;
            }

            FuelStats(SearchFrom, SearchTo);

        }
        private void buttonFuelUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateFuelEntry();
        }
        private void buttonSaveDataBase_Click(object sender, RoutedEventArgs e)
        {
            ChangesMade = false;
            db.GenerateDBFile();
        }
        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            int SearchFrom;
            int SearchTo;

            try
            {
                SearchFrom = int.Parse(textBoxSearchFrom.Text);
                SearchTo = int.Parse(textBoxSearchTo.Text);
            }
            catch (Exception ex)
            {
                textBoxOutput.Text = ex.Message;
                return;
            }

            FuelStats(SearchFrom, SearchTo);
        }

        private void comboBoxFuelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)comboBoxFuelType.SelectedItem == "GNV") textBlockFuelUnitName.Text = "m3";
            if ((string)comboBoxFuelType.SelectedItem == "Gasolina") textBlockFuelUnitName.Text = "L";
        }

        private void labelFuelTotalValue_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                float Unit = float.Parse(textBoxFuelUnitValue.Text);
                float Quant = float.Parse(textBoxFuelQuant.Text);
                textBoxFuelTotalValue.Text = (Quant * Unit).ToString();
            }
            catch { }
        }
        private void labelFuelUnitValue_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                float Total = float.Parse(textBoxFuelTotalValue.Text);
                float Quant = float.Parse(textBoxFuelQuant.Text);
                textBoxFuelUnitValue.Text = (Total / Quant).ToString();
            }
            catch { }
        }
        private void labelFuelQuant_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                float Total = float.Parse(textBoxFuelTotalValue.Text);
                float Unit = float.Parse(textBoxFuelUnitValue.Text);
                textBoxFuelQuant.Text = (Total / Unit).ToString();
            }
            catch { }
        }

        private void listBoxData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int Id = GetIdFromString((string)listBoxData.SelectedItem);
            if (SearchFromFocus)
                textBoxSearchFrom.Text = Id.ToString();
            if (SearchToFocus)
                textBoxSearchTo.Text = Id.ToString();

            //ShowFuelEntry(Id);
        }
        private void listBoxData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxData.SelectedIndex >= 0)
            {
                int Id = GetIdFromString((string)listBoxData.SelectedItem);
                //MessageBox.Show(Id.ToString());
                ShowFuelEntry(Id);
            }
        }

        private void textBoxSearchTo_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SearchFromFocus = false;
            SearchToFocus = true;
        }
        private void textBoxSearchFrom_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SearchFromFocus = true;
            SearchToFocus = false;
        }
        private void textBoxFuelQuant_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = textBoxFuelQuant.CaretIndex;
            if (textBoxFuelQuant.Text.Contains(".")) textBoxFuelQuant.Text = textBoxFuelQuant.Text.Replace(".", ",");
            textBoxFuelQuant.CaretIndex = index;
        }
        private void textBoxFuelUnitValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = textBoxFuelUnitValue.CaretIndex;
            if (textBoxFuelUnitValue.Text.Contains(".")) textBoxFuelUnitValue.Text = textBoxFuelUnitValue.Text.Replace(".", ",");
            textBoxFuelUnitValue.CaretIndex = index;
        }
        private void textBoxFuelTotalValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = textBoxFuelTotalValue.CaretIndex;
            if (textBoxFuelTotalValue.Text.Contains(".")) textBoxFuelTotalValue.Text = textBoxFuelTotalValue.Text.Replace(".", ",");
            textBoxFuelTotalValue.CaretIndex = index;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ChangesMade)
            {
                MessageBoxResult res;
                res = MessageBox.Show("Database não salva. Salvar?", "Salvar?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    ChangesMade = false;
                    db.GenerateDBFile();
                }
            }

        }

    }
}
