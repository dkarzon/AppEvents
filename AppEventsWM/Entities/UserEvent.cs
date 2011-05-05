using System;
using System.Net;
using System.Collections.Generic;

namespace AppEventsWM
{
    public class UserEvent
    {
        public string Name { get; set; }
        public List<DateTime> Occurrrences { get; set; }

        public UserEvent()
        {
            Occurrrences = new List<DateTime>();
        }
    }
}
