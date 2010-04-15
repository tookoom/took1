using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Took1.CarControl
{
    class AMXmlDatabase
    {
        private string _XmlFile;
        private string _FileName = "";
        private bool _StandardDBFileCreated;

        private string _ErrMsg = "";
        private bool _ErrState = false;

        public AMHeader Header = new AMHeader();
        public List<AMFuelEntry> FuelEntryList = new List<AMFuelEntry>();
        public List<AMExpenseEntry> ExpenseEntryList = new List<AMExpenseEntry>();

        public AMXmlDatabase() 
        {
            _StandardDBFileCreated = true;
            _FileName = "CarControl.amxdb";

            if (File.Exists(_FileName))
            {
                ReadFile();
                Header = ReadHeader(_XmlFile);
                _StandardDBFileCreated = false;
            }
            else GenerateStandardDBFile();
        }
        public bool StandardFileCreated
        {
            get { return _StandardDBFileCreated; }
        }
        public string FileName
        {
            get { return _FileName; }
        }
        public string XmlFile
        {
            get { return _XmlFile; }
        }

        private string GenerateStandardDBFile()
        {
            Header = new AMHeader()
            {
                Name = "Andre",
                CreationDate = DateTime.Now.ToString()
            };
            return GenerateDBFile();
        }
        public string GenerateDBFile()
        {
            int i = 0;
            foreach (AMFuelEntry f in FuelEntryList)
            {
                f.Id = i;
                i++;
            }
            i = 0;
            foreach (AMExpenseEntry e in ExpenseEntryList)
            {
                e.Id = i;
                i++;
            }
            var Query = new XElement("AMXmlDataBase",
                 new XElement("AMHeader",
                    new XElement("Name", Header.Name),
                    new XElement("CreationDate", Header.CreationDate)),

                from fuel in FuelEntryList
                select new XElement("AMFuelEntry",
                    new XElement("Id", fuel.Id),
                    new XElement("Type", fuel.Type),
                    new XElement("Quantity", fuel.Quantity.ToString()),
                    new XElement("UnitName", fuel.UnitName),
                    new XElement("UnitValue", fuel.UnitValue.ToString()),
                    new XElement("TotalValue", fuel.TotalValue.ToString()),
                    new XElement("TimeStamp", fuel.TimeStamp),
                    new XElement("KmMark", fuel.KmMark),
                    new XElement("Note", fuel.Note)),

                from expense in ExpenseEntryList
                select new XElement("AMExpenseEntry",
                    new XElement("Id", expense.Id),
                    new XElement("Type", expense.Type),
                    new XElement("Description", expense.Description),
                    new XElement("TotalValue", expense.TotalValue),
                    new XElement("TimeStamp", expense.TimeStamp),
                    new XElement("KmMark", expense.KmMark),
                    new XElement("Note", expense.Note))
                    );

            _XmlFile = Query.ToString();
            File.WriteAllText(_FileName, _XmlFile);
            return _XmlFile;
        }
        private XElement LoadXmlFile(XmlReader Reader)
        {
            XElement Ret;
            try
            {
                Ret = XElement.Load(Reader);
            }
            catch (Exception e)
            {
                SetErrorState("XmlComFile.LoadXml", e.Message);
                Ret = new XElement("NONE");
            }
            return Ret;
        }
        public AMHeader ReadHeader(string File)
        {
            ResetErrorState();
            StringReader StrReader = new StringReader(File);
            XmlReader Reader = XmlReader.Create(StrReader);
            XElement root = LoadXmlFile(Reader);
            if (ErrorState) return new AMHeader();

            IEnumerable<AMHeader> HeaderQuery =
                from el in root.Elements("AMHeader")
                let a = from s in el.Elements("Name") select s.Value
                let b = from s in el.Elements("CreationDate") select s.Value
                select new AMHeader { Name = a.First(), CreationDate = b.First() };

            if (HeaderQuery.Count() > 0)
                return HeaderQuery.First();
            else return new AMHeader();

        }
        public void ReadFile()
        {
            FuelEntryList.Clear();
            ExpenseEntryList.Clear();

            ResetErrorState();

            _XmlFile = File.ReadAllText(_FileName);
            StringReader StrReader = new StringReader(_XmlFile);
            XmlReader Reader = XmlReader.Create(StrReader);
            XElement root = LoadXmlFile(Reader);
            if (ErrorState) return;

            var FuelQuery =
                from el in root.Elements("AMFuelEntry")
                let a = from s in el.Elements("Type") select s.Value
                let b = from s in el.Elements("Quantity") select float.Parse(s.Value)
                let c = from s in el.Elements("UnitName") select s.Value
                let d = from s in el.Elements("UnitValue") select float.Parse(s.Value)
                let e = from s in el.Elements("TotalValue") select float.Parse(s.Value)
                let f = from s in el.Elements("TimeStamp") select Convert.ToDateTime(s.Value)
                let g = from s in el.Elements("KmMark") select float.Parse(s.Value)
                let h = from s in el.Elements("Note") select s.Value
                let i = from s in el.Elements("Id") select int.Parse(s.Value)
                select new AMFuelEntry
                {
                    Id = i.First(),
                    Type = a.First(),
                    Quantity = b.First(),
                    UnitName = c.First(),
                    UnitValue = d.First(),
                    TotalValue = e.First(),
                    TimeStamp = f.First(),
                    KmMark = g.First(),
                    Note = h.First()
                };

            foreach (AMFuelEntry el in FuelQuery)
            {
                FuelEntryList.Add(new AMFuelEntry
                {
                    Id = el.Id,
                    Type=el.Type,
                    Quantity=el.Quantity,
                    UnitName=el.UnitName,
                    UnitValue=el.UnitValue,
                    TotalValue=el.TotalValue,
                    TimeStamp=el.TimeStamp,
                    KmMark=el.KmMark,
                    Note=el.Note
                });
            }

            var ExpenseQuery =
                from el in root.Elements("AMExpenseEntry")
                let a = from s in el.Elements("Type") select s.Value
                let b = from s in el.Elements("Description") select s.Value
                let e = from s in el.Elements("TotalValue") select Convert.ToInt32(s.Value)
                let f = from s in el.Elements("TimeStamp") select Convert.ToDateTime(s.Value)
                let g = from s in el.Elements("KmMark") select Convert.ToInt32(s.Value)
                let h = from s in el.Elements("Note") select s.Value
                let i = from s in el.Elements("Id") select int.Parse(s.Value)
                select new AMExpenseEntry
                {
                    Id = i.First(),
                    Type = a.First(),
                    Description = b.First(),
                    TotalValue = e.First(),
                    TimeStamp = f.First(),
                    KmMark = g.First(),
                    Note = h.First()
                };

            foreach (AMExpenseEntry el in ExpenseQuery)
            {
                ExpenseEntryList.Add(new AMExpenseEntry
                {
                    Id = el.Id,
                    Type = el.Type,
                    Description = el.Description,
                    TotalValue = el.TotalValue,
                    TimeStamp = el.TimeStamp,
                    KmMark = el.KmMark,
                    Note = el.Note
                });
            }

        }

        protected void ResetErrorState()
        {
            _ErrMsg = "";
            _ErrState = false;
        }
        protected void SetErrorState(string Function, string Error)
        {
            _ErrMsg = "Exception in " + Function + ": " + Error;
            _ErrState = true;
        }
        public bool ErrorState
        {
            get { return _ErrState; }
        }
        public string ErrorMessage
        {
            get { return _ErrMsg; }
        }

    }
    public class AMHeader
    {
        public string Name { get; set; }
        public string CreationDate { get; set; }
    }
    public class AMFuelEntry
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float Quantity { get; set; }
        public string UnitName { get; set; }
        public float UnitValue { get; set; }
        public float TotalValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public float KmMark { get; set; }
        public string Note { get; set; }
    }
    public class AMExpenseEntry
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public float TotalValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public int KmMark { get; set; }
        public string Note { get; set; }
    }
}
