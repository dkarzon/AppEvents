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
using System.Collections.Generic;
using System.Linq;

namespace AppEvents
{
    public class UserEventList : List<UserEvent>
    {
        public bool Fired(string eventName)
        {
            return this.Any(el => el.Name == eventName);
        }
        public bool Fired(string eventName, int count)
        {
            return this.Any(el => el.Name == eventName && el.Occurrrences.Count >= count);
        }
    }
}
