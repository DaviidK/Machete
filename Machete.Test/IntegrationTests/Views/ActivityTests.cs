﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using Machete.Domain;
using Machete.Data;
using System.Linq;
using System.Collections.Generic;
using Machete.Service;
using Machete.Data.Infrastructure;
using System.Data.Entity;

namespace Machete.Test
{
    [TestClass]
    public class ActivityTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private sharedUI ui;
        private DatabaseFactory _dbfactory;
        private WorkerService _wserv;
        private WorkerRepository _wRepo;
        private IUnitOfWork _unitofwork;
        private MacheteContext DB;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext) { }

        [TestInitialize]
        public void SetupTest()
        {
            Database.SetInitializer<MacheteContext>(new MacheteInitializer());
            DB = new MacheteContext("machete");
            WorkerCache.Initialize(DB);
            LookupCache.Initialize(DB);
            _dbfactory = new DatabaseFactory();
            _wRepo = new WorkerRepository(_dbfactory);
            _unitofwork = new UnitOfWork(_dbfactory);
            _wserv = new WorkerService(_wRepo, _unitofwork);
            driver = new FirefoxDriver();
            baseURL = "http://localhost:4213/";
            ui = new sharedUI(driver, baseURL);
            verificationErrors = new StringBuilder();
            ui.login();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            //Logoff
            Assert.AreEqual("", verificationErrors.ToString());
            ui.WaitForElement(By.LinkText("Logoff"));
            driver.FindElement(By.LinkText("Logoff")).Click();
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                //ignoring errors if we can't close the browser.
            }
        }
        [ClassCleanup]
        public static void ClassCleanup() { }

        [TestMethod]
        public void SeActivity_Create_Validate()
        {
            //Arrange
            Activity _act = (Activity)Records.activity.Clone();
            //Act
            ui.activityCreate(_act);
            //Assert
            ui.activityValidate(_act);
        }

        [TestMethod]
        public void SeActivity_Create_ManySignins()
        {
            //Arrange
            int rowcount = 1;                        
            MacheteContext DB = new MacheteContext("machete"); //maps to Machete.Text\app.config ConnectionString
            IEnumerable<int> cardlist = DB.Workers.Select(q => q.dwccardnum).Distinct();
            Random rand = new Random();
            // There's a lot in this one line. Ask questions about it.
            //                         [DbSet] (LINQ)(Lambda Expression) (SQL-ish)
            IEnumerable<int> list = DB.Workers.Select(q => q.dwccardnum).Distinct();
            var count = list.Count();
            Assert.IsTrue(count > 10, "There were not enough workers found whose dwccardnumbers we can use! There were only: " + count);
            int numberOfSignins = rand.Next(count / 10) + 1; //+1 will never lead to zero here
            int originalNumberOfSignins = numberOfSignins;
            int numberOfSanctionedSignins = 0;
            IEnumerable<int> list1 = list.Take<int>(numberOfSignins);
            bool lastWorkerWasSanctioned = false;
            Activity _act = (Activity)Records.activity.Clone();
            ui.activityCreate(_act);

            for (var i = 0; i <= numberOfSignins; i++)
            {
                int cardNum = list1.ElementAt(i);
                ui.activitySignIn(cardNum);
                var sanctionedBox = ui.WaitForElement(By.XPath("/html/body/div[3]"));
                if (sanctionedBox != null && sanctionedBox.GetCssValue("display") == "block")
                {
                    Assert.IsTrue(ui.WaitThenClickElement(By.XPath("/html/body/div[3]/div[1]/a")), "Couldn't find button to close sanctionbox");
                    --numberOfSignins;
                    ++numberOfSanctionedSignins;
                    lastWorkerWasSanctioned = true;
                    continue;
                }
                Assert.IsTrue(ui.activitySignInValidate(cardNum, rowcount), "Sign in for worker " + i + " failed!" + (lastWorkerWasSanctioned ? "This worker came directly after an attempted signin of a sanctioned worker." : ""));
                
                rowcount += DB.Workers.Where(q => q.dwccardnum == cardNum).Count(); 
                lastWorkerWasSanctioned = false;
            }
            ui.WaitThenClickElement(By.Id("activityListTab"));
            ui.SelectOption(By.XPath("//*[@id='activityTable_length']/label/select"), "100");
            Assert.IsTrue(ui.WaitForElementValue(By.XPath("//table[@id='activityTable']/tbody/tr[@recordid='" + _act.ID + "']/td[4]"), numberOfSignins.ToString()), 
                "Not the right number of workers signed in. Expected: " + numberOfSignins + 
                ", got: " + ui.WaitForElement(By.XPath("//table[@id='activityTable']/tbody/tr[@recordid='" + _act.ID + "']/td[4]")).Text +
                ". Originally we intended to signin " +originalNumberOfSignins+ ", but we subtracted " + numberOfSanctionedSignins +
                " for signins that turned out to be sanctioned workers.");
        }
        
        [TestMethod]
        public void SeActivity_Create_signin_simple()
        {
            // Arrange
            Random rand = new Random();
            int rowcount = 1;
            MacheteContext DB = new MacheteContext("machete");
            var workers = DB.Workers;
            Activity _act= (Activity)Records.activity.Clone();
            ActivitySignin _asi = (ActivitySignin)Records.activitysignin.Clone();
            IEnumerable<int> cardlist = DB.Workers.Select(q => q.dwccardnum).Distinct();            
            // Act
            ui.activityCreate(_act);
            ui.activitySignIn(workers.First().dwccardnum);
            // Assert
            Assert.IsTrue(ui.activitySignInValidate(workers.First().dwccardnum, rowcount));
        }

        [TestMethod]
        public void SeActivity_Signin_random_worker()
        {
            //Arrange
            Activity _act = (Activity)Records.activity.Clone();
            var workers = DB.Workers;
            IEnumerable<int> cardList = workers.Select(q => q.dwccardnum).Distinct();
            Random rand = new Random();
            int randCard;
            int randCardIndex = rand.Next(cardList.Count());
            int rowCount = 1;
            randCard = cardList.ElementAt(randCardIndex);
            while (workers.First(c => c.dwccardnum == randCard).isSanctioned)
            {
                randCard = cardList.ElementAt(randCardIndex);
            }
            //Act
            ui.activityCreate(_act);
            ui.activitySignIn(randCard);

            //Assert
            Assert.IsTrue(ui.activitySignInValidate(randCard,rowCount));

        }

        [TestMethod]
        public void SeActivity_Signin_random_sactioned_worker()
        {
            //Arrange
            Person _per = (Person)Records.person.Clone();
            Worker _sanctionedW = (Worker)Records.worker.Clone();
            Activity _act = (Activity)Records.activity.Clone();

            var workers = DB.Workers;
            IEnumerable<int> cardList = workers.Where(q => q.memberStatus == Worker.iSanctioned || q.memberStatus == Worker.iExpelled).Select(q => q.dwccardnum).Distinct();
            Assert.AreNotEqual(0, cardList.Count());
            Random rand = new Random();
            int randCardIndex = rand.Next(cardList.Count());
            int randCard = cardList.ElementAt(randCardIndex);
            int rowcount = 1;

            //Act
            ui.personCreate(_per);
            _sanctionedW.ID = _per.ID;
            _sanctionedW.memberStatus = Worker.iSanctioned; 

            ui.workerCreate(_sanctionedW, sharedUI.SolutionDirectory() + "\\Machete.test\\jimmy_machete.jpg");
            ui.activityCreate(_act);
            ui.activitySignIn(randCard);

            //Assert
            Assert.IsFalse(ui.activitySignInValidate(randCard, rowcount));
            var sanctionedBox = ui.WaitForElement(By.XPath("/html/body/div[3]"));
            Assert.IsTrue(sanctionedBox != null && sanctionedBox.GetCssValue("display") == "block", "Sanctioned worker box is not visible like it should be.");
            ui.WaitThenClickElement(By.XPath("/html/body/div[3]/div[1]/a"));
        }
    }
}
