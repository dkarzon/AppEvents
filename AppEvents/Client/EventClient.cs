﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AppEvents
{
    public class AppEventsClient : IHideObjectMembers
    {
        public static AppEventsClient Current = new AppEventsClient();

        private IEventStorageProvider _storageProvider;
        private EventStore EventStore;
        private List<Rule> Rules;

        private AppEventsClient()
        {
            Rules = new List<Rule>();
            EventStore = new EventStore();
        }

		private AppEventsClient(bool loadStore) : this()
		{
			if (loadStore)
			{
				FromStorage(new JsonStorageProvider());
			}
		}

		public static Rule New(string name)
		{
			if (Current == null)
			{
				Current = new AppEventsClient();
			}

			var rule = new Rule(name);

			Current.Rules.Add(rule);

			return rule;
		}

        /// <summary>
        /// Sets the Current instance of the EventClient
        /// </summary>
        /// <returns></returns>
        public static AppEventsClient New()
        {
            Current = new AppEventsClient();

            Current.FromStorage(new JsonStorageProvider());

            return Current;
        }

        /// <summary>
        /// Sets the Current instance of the EventClient
        /// </summary>
        /// <returns></returns>
        public static AppEventsClient New<T>() where T : IEventStorageProvider, new()
        {
            Current = new AppEventsClient();

            Current.FromStorage(new T());

            return Current;
        }

        /// <summary>
        /// Creates an instance of the AppEventsClient with a rule to add to the listener
        /// </summary>
        /// <param name="newRule">An event Rule to add</param>
        /// <returns></returns>
        public static AppEventsClient New(Rule newRule)
        {
            if (Current == null)
            {
                Current = new AppEventsClient();
                Current.FromStorage(new JsonStorageProvider());
            }

            Current.Add(newRule);

            return Current;
        }

        /// <summary>
        /// Creates an instance of the AppEventsClient with a rule to add to the listener and setting a storage provider
        /// </summary>
        /// <typeparam name="T">Storage Provider</typeparam>
        /// <param name="newRule">An event Rule to add</param>
        /// <returns></returns>
        public static AppEventsClient New<T>(Rule newRule) where T : IEventStorageProvider, new()
        {
            if (Current == null)
            {
                Current = new AppEventsClient();
                Current.FromStorage(new T());
            }

            Current.Add(newRule);

            return Current;
        }

        /// <summary>
        /// Loads the Event History of the current user
        /// </summary>
        /// <param name="eventStore">The event store loaded from storage</param>
        /// <returns></returns>
        public AppEventsClient LoadStore(EventStore eventStore)
        {
            if (eventStore != null)
            {
                EventStore = eventStore;
            }

            return this;
        }

        /// <summary>
        /// Loads the Storage Provider and attempts call its LoadEventStore() function
        /// </summary>
        /// <param name="storageProvider">An instance of an IEventStorageProvider</param>
        /// <returns></returns>
        public AppEventsClient FromStorage(IEventStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
            //try getting the EventStore
            EventStore = _storageProvider.LoadEventStore();
            if (EventStore == null)
            {
                EventStore = new EventStore();
            }

            return this;
        }

        /// <summary>
        /// Adds a new Ruleset to the event client
        /// </summary>
        /// <param name="newRule">The Rule to Add to the listening list</param>
        /// <returns></returns>
        public AppEventsClient Add(Rule newRule)
        {
            Rules.Add(newRule);
            return this;
        }

        /// <summary>
        /// Fires an event with the given Name
        /// </summary>
        /// <param name="eventName">The event name to fire</param>
        /// <param name="runRules">Weather or not the Rules should be run</param>
        /// <returns></returns>
        public AppEventsClient Fire(string eventName, bool runRules = true, bool saveStore = true)
        {
            EventStore.Fire(eventName);

            if (runRules)
            {
                Run();
            }

            if (saveStore)
            {
                SaveStore();
            }

            return this;
        }

        /// <summary>
        /// Manually run a specific Rule
        /// </summary>
        /// <param name="ruleName">Name of the rule to try run</param>
        /// <returns></returns>
        public AppEventsClient Run(string ruleName)
        {
            var rule = Rules.SingleOrDefault(r => r.Name == ruleName);
            if (rule != null)
            {
                RunRule(rule);
            }

            return this;
        }

        /// <summary>
        /// Runs all Rules in the list
        /// </summary>
        /// <returns></returns>
        public AppEventsClient Run()
        {
            //now check the rules
            foreach (var r in Rules)
            {
                RunRule(r);
            }

            return this;
        }

        /// <summary>
        /// INTERNAL - Runs the specified rule
        /// </summary>
        /// <param name="r">RuleSet to run</param>
        private void RunRule(Rule r)
        {
            //Make sure it hasn't been run before
            if (!EventStore.UserRules.Any(ur => ur.RuleName == r.Name))
            {
                //try run the rule
                if (r.RunOperations(EventStore.UserEvents))
                {
                    //success!
                    r.Action(r);
                    EventStore.UserRules.Add(new UserRule { RuleName = r.Name, Executed = DateTime.Now });
                }
            }
        }

        //Saves the EventStore if there is a Storage Provider set
        public AppEventsClient SaveStore()
        {
            if (_storageProvider != null)
            {
                _storageProvider.SaveEventStore(EventStore);
            }

            return this;
        }

        /// <summary>
        /// Gets the EventStore to save to storage
        /// </summary>
        /// <returns></returns>
        public EventStore GetEventStore()
        {
            return EventStore;
        }

    }
}
