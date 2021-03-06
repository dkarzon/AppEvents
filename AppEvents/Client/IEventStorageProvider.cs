﻿using System;

namespace AppEvents
{
    public interface IEventStorageProvider
    {
        EventStore LoadEventStore();
        void SaveEventStore(EventStore eventStore);
    }
}
