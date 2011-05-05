using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;

namespace AppEventsWM
{
    public class RuleSet
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Func<UserEventList, bool> Operation { get; private set; }

        public Action<RuleSet> Action { get; private set; }

        private RuleSet(string name, Func<UserEventList, bool> op)
        {
            Name = name;
            Operation = op;
        }

        public static RuleSet When(string ruleName, Func<UserEventList, bool> op)
        {
            var ruleset = new RuleSet(ruleName, op);

            return ruleset;
        }

        public RuleSet Do(Action<RuleSet> action)
        {
            Action = action;

            return this;
        }
    }
}
