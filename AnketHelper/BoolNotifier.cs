using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AnketHelper
{
    class BoolNotifyPropertyChanged : INotifyPropertyChanged
    {
        private bool _val;

        public bool val
        {
            get
            {
                return _val;
            }
            set
            {
                _val = value;
                OnPropertyChanged("val");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
