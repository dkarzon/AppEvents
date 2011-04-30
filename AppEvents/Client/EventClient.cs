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
    public class EventClient : IHideObjectMembers
    {
        public static EventClient Current = new EventClient();


        private EventStore _eventStore;
        private List<RuleSet> _rules;

        private EventClient()
        {
            _rules = new List<RuleSet>();
            _eventStore = new EventStore();
        }

        /// <summary>
        /// Loads the Event History of the current user
        /// </summary>
        /// <param name="eventStore">The event store loaded from storage</param>
        /// <returns></returns>
        public EventClient LoadHistory(EventStore eventStore)
        {
            if (eventStore != null)
            {
                _eventStore = eventStore;
            }

            return this;
        }

        /// <summary>
        /// Adds a new Ruleset to the event client
        /// </summary>
        /// <param name="ruleSet">The RuleSet to Add</param>
        /// <returns></returns>
        public EventClient Add(RuleSet ruleSet)
        {
            _rules.Add(ruleSet);
            return this;
        }

        /// <summary>
        /// Fires an event with the given Name
        /// </summary>
        /// <param name="eventName">The event name to fire</param>
        /// <param name="runRules">Weather or not the Rules should be run</param>
        /// <returns></returns>
        public EventClient Fire(string eventName, bool runRules = true)
        {
            _eventStore.Fire(eventName);

            if (runRules)
            {
                RunRules();
            }

            return this;
        }

        /// <summary>
        /// Manually run a specific RuleSet
        /// </summary>
        /// <param name="ruleName">Name of the rule to run</param>
        /// <returns></returns>
        public EventClient RunRule(string ruleName)
        {
            var rule = _rules.SingleOrDefault(r => r.Name == ruleName);
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
        public EventClient RunRules()
        {
            //now check the rules
            foreach (var r in _rules)
            {
                RunRule(r);
            }

            return this;
        }

        /// <summary>
        /// INTERNAL - Runs the specified rule
        /// </summary>
        /// <param name="r">RuleSet to run</param>
        private void RunRule(RuleSet r)
        {
            //Make sure it hasn't been run before
            if (!_eventStore.UserRules.Any(ur => ur.RuleName == r.Name))
            {
                //try run the rule
                if (r.Operation(_eventStore.UserEvents))
                {
                    //success!
                    r.Action(r);
                    _eventStore.UserRules.Add(new UserRule { RuleName = r.Name, Executed = DateTime.Now });
                }
            }
        }

        /// <summary>
        /// Gets the EventStore to save to storage
        /// </summary>
        /// <returns></returns>
        public EventStore GetEventStore()
        {
            return _eventStore;
        }

    }
}
