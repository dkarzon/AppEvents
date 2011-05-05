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
using Microsoft.Phone.Controls;

namespace AppEventsSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            AppEvents.EventClient.Current.Fire("testevent1");
        }

        private void Event2Button_Click(object sender, RoutedEventArgs e)
        {
            AppEvents.EventClient.Current.Fire("testevent2");
        }

        private void Event3Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	AppEvents.EventClient.Current.Fire("testevent3", false);
        }

        private void Rule3Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	AppEvents.EventClient.Current.RunRule("testrule3");
        }

        private void ResetButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AppEvents.EventClient.Current.LoadHistory(new AppEvents.EventStore());
        }
    }
}