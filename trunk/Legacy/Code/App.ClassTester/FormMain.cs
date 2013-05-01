using System;
using System.Linq;
using System.Windows.Forms;
using TK1.Data.Entity;
using TK1.Data.Entity.Source;
using TK1.Data.Entity.Model;




namespace TK1.ClassTester
{
    public partial class FormMain : Form
    {
        #region PRIVATE MEMBERS
        DataContext dataContext;

        #endregion
        #region PROPERTIES
        public string Input1
        {
            get { return textBoxInput1.Text; }
            set { textBoxInput1.Text = value; }
        }
        public string Input2
        {
            get { return textBoxInput2.Text; }
            set { textBoxInput2.Text = value; }
        }
        public string Input3
        {
            get { return textBoxInput3.Text; }
            set { textBoxInput3.Text = value; }
        }
        public string Input4
        {
            get { return textBoxInput4.Text; }
            set { textBoxInput4.Text = value; }
        }
        public string InputLabel1
        {
            set { labelInput1.Text = value; }
        }
        public string InputLabel2
        {
            set { labelInput2.Text = value; }
        }
        public string InputLabel3
        {
            set { labelInput3.Text = value; }
        }
        public string InputLabel4
        {
            set { labelInput4.Text = value; }
        }

        public string ActionText1
        {
            get { return buttonAction1.Text; }
            set { buttonAction1.Text = value; }
        }
        public string ActionText2
        {
            get { return buttonAction2.Text; }
            set { buttonAction2.Text = value; }
        }
        public string ActionText3
        {
            get { return buttonAction3.Text; }
            set { buttonAction3.Text = value; }
        }
        public string ActionText4
        {
            get { return buttonAction4.Text; }
            set { buttonAction4.Text = value; }
        }

        public string OutputLabel1
        {
            set { labelOutput1.Text = value; }
        }
        public string OutputLabel2
        {
            set { labelOutput2.Text = value; }
        }

        public string Output1
        {
            get { return textBoxOutput1.Text; }
            set { textBoxOutput1.Text = value + Environment.NewLine; }
        }
        public string Output2
        {
            get { return textBoxOutput2.Text; }
            set { textBoxOutput2.Text = value + Environment.NewLine; }
        }

        
        #endregion

        public FormMain()
        {
            TextBox.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            #region PADRAO
            InputLabel1 = "Entrada 1";
            InputLabel2 = "Entrada 2";
            InputLabel3 = "Entrada 3";
            InputLabel4 = "Entrada 4";

            OutputLabel1 = "Saída 1";
            OutputLabel2 = "Saída 2";

            ActionText1 = "Ação 1";
            ActionText2 = "Ação 2";
            ActionText3 = "Ação 3";
            ActionText4 = "Ação 4"; 
            #endregion

            //InputLabel1 = "Servidor";
            //InputLabel2 = "Usuário";
            //InputLabel3 = "Senha";
            //InputLabel4 = "Destinatário";

            //OutputLabel1 = "Active";
            //OutputLabel2 = "Inactive";

            ActionText1 = "Start";
            ActionText2 = "Stop";
            ActionText3 = "";
            ActionText4 = "";

            initialize();
        }

        private void initialize()
        {
            Input1 = "";
            Input2 = "";
            Input3 = "";
            Input4 = "";

            

            //dataContext = new DataContext();
            //loadDataContext();
        }
        private void loadDataContext()
        {
            dataContext.AppContextEntities.Add(new AppContext() { Name = "CarControl" });
            dataContext.AppContextEntities.Add(new AppContext() { Name = "LifeManager" });
            dataContext.AppContextEntities.Add(new AppContext() { Name = "PicDeveloper" });

            AppContext appContext = dataContext.AppContextEntities.Where(el => el.Name == "CarControl").FirstOrDefault();
            if (appContext != null)
            {
                dataContext.CategoryEntities.Add(new Category()
                {
                    AppContext = new EntityReference<AppContext>() { ReferenceKey = appContext.Key },
                    Name = "Category1",
                    Caption = "",
                });
                dataContext.CategoryEntities.Add(new Category()
                {
                    AppContext = new EntityReference<AppContext>() { ReferenceKey = appContext.Key },
                    Name = "Category2",
                    Caption = "",
                }); dataContext.CategoryEntities.Add(new Category()
                {
                    AppContext = new EntityReference<AppContext>() { ReferenceKey = appContext.Key },
                    Name = "Category3",
                    Caption = "",
                });
            }
        }

        private InputLanguage GetFarsiLanguage()
        {
            //Enumerate through InstalledInputLanguages which contains
            //all the keyboard layout you've installed in your windows.
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.LayoutName.ToLower() == "farsi")
                    return lang;
            }

            return null;
        }

        public void YourMethod()
        {
            InputLanguage lang = GetFarsiLanguage();
            if (lang == null)
                throw new NotSupportedException("Farsi Language keyboard is not installed.");

            //Set the current language of the system to
            //the InputLanguage instance you need.
            InputLanguage.CurrentInputLanguage = lang;
        }

        #region BUTTON EVENTS -> ACTIONS
        private void buttonAction1_Click(object sender, EventArgs e)
        {
            string message = string.Format("{0}",InputLanguage.CurrentInputLanguage.Culture.Name);
            writeOutput1(message);
        }
        private void buttonAction2_Click(object sender, EventArgs e)
        {
        }
        private void buttonAction3_Click(object sender, EventArgs e)
        {
        }
        private void buttonAction4_Click(object sender, EventArgs e)
        {
        } 
        #endregion

        #region BUTTON CLEAR EVENTS -> OUTPUT

        private void buttonClear1_Click(object sender, EventArgs e)
        {
            Output1 = "";
        }
        private void buttonClear2_Click(object sender, EventArgs e)
        {
            Output2 = "";
        } 
        #endregion

        #region TEXT CHANGED EVENTS -> INPUTS
        private void textBoxInput1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBoxInput2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBoxInput3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBoxInput4_TextChanged(object sender, EventArgs e)
        {

        } 
        #endregion

        #region UI METHODS
        private void writeOutput1(string message)
        {
            textBoxOutput1.Text += message + Environment.NewLine;
        }
        private void writeOutput2(string message)
        {
            textBoxOutput2.Text += message + Environment.NewLine;
        }
        #endregion


    }
}
