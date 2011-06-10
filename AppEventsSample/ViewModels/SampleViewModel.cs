using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace AppEventsSample.ViewModels
{
    public class SampleViewModel : BaseViewModel
    {
        private ObservableCollection<string> _firedRules;
        public ObservableCollection<string> FiredRules
        {
            get { return _firedRules; }
            set
            {
                _firedRules = value;
                NotifyPropertyChanged("FiredRules");
            }
        }


        public SampleViewModel()
        {
            FiredRules = new ObservableCollection<string>();
        }
    }
}
