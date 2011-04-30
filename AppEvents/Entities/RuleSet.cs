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
using System.ComponentModel;

namespace AppEvents
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
