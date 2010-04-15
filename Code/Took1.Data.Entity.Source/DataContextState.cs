using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Took1.Data.Entity.Model;

namespace Took1.Data.Entity.Source
{
    public class DataContextState
    {
        #region EVENTS

        public event EventHandler ClientChanged;
        private void OnAppContextChanged(EventArgs e)
        {
            EventHandler handler = ClientChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ClientGroupChanged;
        private void OnCategoryChanged(EventArgs e)
        {
            EventHandler handler = ClientGroupChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ClientModelChanged;
        private void OnClientModelChanged(EventArgs e)
        {
            EventHandler handler = ClientModelChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ClientModelCommPortChanged;
        private void OnClientModelCommPortChanged(EventArgs e)
        {
            EventHandler handler = ClientModelCommPortChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        } 

        public event EventHandler ClientModelEventChanged;
        private void OnClientModelEventChanged(EventArgs e)
        {
            EventHandler handler = ClientModelEventChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ClientModelEventScriptChanged;
        private void OnClientModelEventScriptChanged(EventArgs e)
        {
            EventHandler handler = ClientModelEventScriptChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ClientModelSignalChanged;
        private void OnClientModelSignalChanged(EventArgs e)
        {
            EventHandler handler = ClientModelSignalChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler DataColBitmapChanged;
        private void OnDataColBitmapChanged(EventArgs e)
        {
            EventHandler handler = DataColBitmapChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler DncChanged;
        private void OnDncChanged(EventArgs e)
        {
            EventHandler handler = DncChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        } 

        public event EventHandler ScriptCodeChanged;
        private void OnScriptCodeChanged(EventArgs e)
        {
            EventHandler handler = ScriptCodeChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeMethodChanged;
        private void OnCodeMethodChanged(EventArgs e)
        {
            EventHandler handler = CodeMethodChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler CodeActionChanged;
        private void OnCodeActionChanged(EventArgs e)
        {
            EventHandler handler = CodeActionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeActionCollectionChanged;
        private void OnCodeActionCollectionChanged(EventArgs e)
        {
            EventHandler handler = CodeActionCollectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        } 

        public event EventHandler CodeActionInstanceChanged;
        private void OnCodeActionInstanceChanged(EventArgs e)
        {
            EventHandler handler = CodeActionInstanceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeMethodHeaderChanged;
        private void OnCodeMethodHeaderChanged(EventArgs e)
        {
            EventHandler handler = CodeMethodHeaderChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler CodeTypeChanged;
        private void OnCodeTypeChanged(EventArgs e)
        {
            EventHandler handler = CodeTypeChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler DataColGroupChanged;
        private void OnDataColGroupChanged(EventArgs e)
        {
            EventHandler handler = DataColGroupChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler DataColGroupScriptChanged;
        private void OnDataColGroupScriptChanged(EventArgs e)
        {
            EventHandler handler = DataColGroupScriptChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler EventTypeChanged;
        private void OnEventTypeChanged(EventArgs e)
        {
            EventHandler handler = EventTypeChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler ResourceChanged;
        private void OnResourceChanged(EventArgs e)
        {
            EventHandler handler = ResourceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ResourceScriptChanged;
        private void OnResourceScriptChanged(EventArgs e)
        {
            EventHandler handler = ResourceScriptChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShiftChanged;
        private void OnShiftChanged(EventArgs e)
        {
            EventHandler handler = ShiftChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ShiftValidityChanged;
        private void OnShiftValidityChanged(EventArgs e)
        {
            EventHandler handler = ShiftValidityChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public event EventHandler SignalChanged;
        private void OnSignalChanged(EventArgs e)
        {
            EventHandler handler = SignalChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SignalScriptChanged;
        private void OnSignalScriptChanged(EventArgs e)
        {
            EventHandler handler = SignalScriptChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler UserDataChanged;
        private void OnUserDataChanged(EventArgs e)
        {
            EventHandler handler = UserDataChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler UserDataGroupChanged;
        private void OnUserDataGroupChanged(EventArgs e)
        {
            EventHandler handler = UserDataGroupChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler UserGroupChanged;
        private void OnUserGroupChanged(EventArgs e)
        {
            EventHandler handler = UserGroupChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public event EventHandler UserGroupResourceChanged;
        private void OnUserGroupResourceChanged(EventArgs e)
        {
            EventHandler handler = UserGroupResourceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        public event EventHandler UserResourceChanged;
        private void OnUserResourceChanged(EventArgs e)
        {
            EventHandler handler = UserResourceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler UserResourceScriptChanged;
        private void OnUserResourceScriptChanged(EventArgs e)
        {
            EventHandler handler = UserResourceScriptChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
        #region PRIVATE MEMBERS
        string currentScriptCode = string.Empty;

        AppContext appContext = null;
        Category category = null;
        #endregion
        #region PUBLIC PROPERTIES
        public AppContext AppContext
        {
            get { return appContext; }
            set
            {
                appContext = value;
                OnAppContextChanged(new EventArgs());
            }

        }
        public Category Category
        {
            get { return category; }
            set
            {
                category = value;
                OnCategoryChanged(new EventArgs());
            }
        }

        #endregion

        public DataContextState() { }

        public void Reset()
        {
            var properties = this.GetType().GetProperties();
            if(properties!= null)
            {
                foreach (var property in properties)
                {
                    if (property.CanWrite)
                        property.SetValue(this, null, null);
                }
            }
        }

    }
}
