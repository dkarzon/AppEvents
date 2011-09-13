using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppEvents.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FluentTests
    {
        public FluentTests()
        {

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

		[TestMethod]
		public void Basic_Function_Test()
		{
			var eventRun = false;
			//This is what I want, need to make it work now...
			AppEventsClient.New("testevent1").When("page1", 5).Do((r) => eventRun = true);

			//Clear the store
			AppEventsClient.Current.LoadStore(new EventStore());
			AppEventsClient.Current.Fire("page1");
			AppEventsClient.Current.Fire("page1");
			AppEventsClient.Current.Fire("page1");
			AppEventsClient.Current.Fire("page1");

			Assert.IsFalse(eventRun);
			//This 1 should do the action
			AppEventsClient.Current.Fire("page1");
			Assert.IsTrue(eventRun);
		}

		[TestMethod]
		public void And_Test()
		{
			//This is what I want, need to make it work now...
			AppEventsClient.New("testevent1").When("page1", 5).And("page2", 2).Do((r) => EventFired(r));
		}

		[TestMethod]
		public void And_Not_Test()
		{
			//This is what I want, need to make it work now...
			AppEventsClient.New("testevent1").When("page1", 5).AndNot("page2", 2).Do((r) => EventFired(r));
		}

		[TestMethod]
		public void Full_Test()
		{
			//This is what I want, need to make it work now...
			AppEventsClient.New("testevent1").When("page1", 5).And("page2", 2).AndNot("error").Do((r) => EventFired(r));
		}

		private void EventFired(Rule rule)
		{
		}

    }
}
