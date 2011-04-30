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
    public class UnitTest1
    {
        public UnitTest1()
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //Load the EventStore from somewhere
            var eventStore = new EventStore();
            AppEvents.EventClient.Current.LoadHistory(eventStore)
                //Add a rate-app Rule
                .Add(
                    RuleSet.When("rate-app",
                    el => el.Any(e => e.Name == "viewreport" && e.Occurrrences.Count > 1)
                        && el.Any(e => e.Name == "addedfavorite" && e.Occurrrences.Count > 1))
                    .Do((r) => RateApp()))
                //Add a Hi5 Rule
                .Add(RuleSet.When("Hi5",
                    el => el.Any(e => e.Name == "LiveTileOn" && e.Occurrrences.Max() > DateTime.Today))
                    .Do(r => Hi5(r))
            );

            //Fire some events
            AppEvents.EventClient.Current.Fire("LiveTileOn")
                .Fire("addedfavorite")
                .Fire("addedfavorite")
                .Fire("viewreport");

            //Now get the history and save it
            eventStore = AppEvents.EventClient.Current.GetEventStore();
            //Save(eventStore);
        }

        private void RateApp()
        {
        }

        private void Hi5(RuleSet rule)
        {

        }
    }
}
