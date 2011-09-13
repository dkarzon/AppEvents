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
using System.Linq;

namespace AppEvents
{
    public class Rule
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get; private set; }

		[EditorBrowsable(EditorBrowsableState.Never)]
		public List<Func<UserEventList, bool>> Operations { get; private set; }

        public Action<Rule> Action { get; private set; }

		private Rule()
		{
			Operations = new List<Func<UserEventList,bool>>();
		}

		public Rule(string name) : this()
		{
			Name = name;
		}

		public Rule When(string eventName, int count = 1)
		{
			//TODO - this part is not quite right yet...
			Func<UserEventList, bool> op = el => el.Any(e => e.Name == eventName && e.Occurrrences.Count >= count);

			Operations.Add(op);

			return this;
		}

		public Rule When(Func<UserEventList, bool> op)
		{
			Operations.Add(op);

			return this;
		}

		public Rule And(string eventName, int count = 1)
		{
			//TODO - this part is not quite right yet...
			Func<UserEventList, bool> op = el => el.Any(e => e.Name == eventName && e.Occurrrences.Count >= count);

			Operations.Add(op);

			return this;
		}

		public Rule And(Func<UserEventList, bool> op)
		{
			Operations.Add(op);

			return this;
		}

		public Rule AndNot(string eventName, int count = 1)
		{
			//TODO - What was I thinking?
			Func<UserEventList, bool> op = el => !el.Any(e => e.Name == eventName && e.Occurrrences.Count >= count);

			Operations.Add(op);

			return this;
		}

        /// <summary>
        /// Adds the action to the RuleSet for the Client to execute
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Rule Do(Action<Rule> action)
        {
            Action = action;

            return this;
        }

		internal bool RunOperations(UserEventList eventList)
		{
			foreach (var op in Operations)
			{
				if (!op(eventList)) return false;
			}
			return true;
		}
    }
}
