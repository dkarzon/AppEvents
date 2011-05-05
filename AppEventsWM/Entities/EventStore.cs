using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;

namespace AppEventsWM
{
    public class EventStore
    {
        public UserEventList UserEvents { get; set; }
        public List<UserRule> UserRules { get; set; }

        public EventStore()
        {
            UserEvents = new UserEventList();
            UserRules = new List<UserRule>();
        }

        public void Fire(string eventName)
        {
            var userEvent = UserEvents.SingleOrDefault(e => e.Name == eventName);
            if (userEvent == null)
            {
                userEvent = new UserEvent { Name = eventName };
                userEvent.Occurrrences.Add(DateTime.Now);

                UserEvents.Add(userEvent);
            }
            else
            {
                userEvent.Occurrrences.Add(DateTime.Now);
            }

        }

    }
}
