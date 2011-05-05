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
using AppEvents;
using System.IO.IsolatedStorage;
using System.IO;

namespace AppEvents
{
    public class JsonStorageProvider : IEventStorageProvider
    {
        private string _storageKey = "eventstore";

        public EventStore LoadEventStore()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using (var stream = new IsolatedStorageFileStream(_storageKey, FileMode.OpenOrCreate, FileAccess.Read, store))
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            var output = sr.ReadToEnd();

                            return Newtonsoft.Json.JsonConvert.DeserializeObject<EventStore>(output);
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public void SaveEventStore(EventStore eventStore)
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                using (var stream = new IsolatedStorageFileStream(_storageKey, FileMode.Create, FileAccess.Write, store))
                {
                    var output = Newtonsoft.Json.JsonConvert.SerializeObject(eventStore);
                    using (var sw = new StreamWriter(stream))
                    {
                        sw.Write(output);
                    }
                }
            }
            catch { }
        }
    }
}
