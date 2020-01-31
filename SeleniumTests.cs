using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace SeleniumNUnit
{
    [TestFixture]
    public class SeleniumTests : IDisposable
    {
        IWebDriver driver;

        public SeleniumTests()
        {
            driver = new ChromeDriver("./");
        }

        [SetUp]
        public void Initialize()
        {
            driver.Url = "https://stackoverflow.com/";
        }

        [Test]
        public void CheckTitleOfMainPage()
        {
            Assert.True(driver.Title == "Stack Overflow - Where Developers Learn, Share, & Build Careers");
            Assert.True(driver.WindowHandles.Count == 1);
        }

        [Test]
        public void CheckIfLoginButtonClickRedirectsToLoginPage()
        {
            driver.FindElement(By.LinkText("Log in")).Click();

            Assert.True(driver.Title == "Log In - Stack Overflow");
            Assert.True(driver.WindowHandles.Count == 1);
        }

        [Test]
        public void CheckIfInputsHasClearedContentAfterEnterBadCredentials()
        {
            driver.FindElement(By.LinkText("Log in")).Click();

            driver.FindElement(By.Id("email")).SendKeys("example@mail.com");
            driver.FindElement(By.Id("password")).SendKeys("bad!Password123");
            driver.FindElement(By.Id("submit-button")).Click();

            Assert.True(driver.FindElement(By.Id("email")).Text == string.Empty);
            Assert.True(driver.FindElement(By.Id("password")).Text == string.Empty);
            Assert.True(driver.WindowHandles.Count == 1);
        }

        [Test]
        public void CheckIfForgotPasswordButtonClickRedirectsToForgotPasswordPage()
        {
            driver.FindElement(By.LinkText("Log in")).Click();
            driver.FindElement(By.LinkText("Forgot password?")).Click();

            Assert.True(driver.Title == "Account Recovery - Stack Overflow");
            Assert.True(driver.WindowHandles.Count == 1);
        }

        [Test]
        public void CheckIfIsInTheSamePageAfterLoginError()
        {
            driver.FindElement(By.LinkText("Log in")).Click();

            driver.FindElement(By.Id("email")).SendKeys("example@mail.com");
            driver.FindElement(By.Id("password")).SendKeys("bad!Password123");
            driver.FindElement(By.Id("submit-button")).Click();

            Assert.True(driver.Title == "Log In - Stack Overflow");
            Assert.True(driver.WindowHandles.Count == 1);
        }

        [Test]
        public void CheckIfBackProperlyToHomePageFromLoginPage()
        {
            driver.FindElement(By.LinkText("Log in")).Click();
            driver.Navigate().Back();

            Assert.True(driver.Title == "Stack Overflow - Where Developers Learn, Share, & Build Careers");
            Assert.True(driver.WindowHandles.Count == 1);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}