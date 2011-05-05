using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AppEventsWM
{
    public interface IEventStorageProvider
    {
        EventStore LoadEventStore();
        void SaveEventStore(EventStore eventStore);
    }
}
