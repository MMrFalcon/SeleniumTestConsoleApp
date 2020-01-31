using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace SeleniumTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            IWebDriver driver = new ChromeDriver();

           // driver.Url = "http://ii.up.krakow.pl/";

            driver.Url = "https://stackoverflow.com/";

            checkTitle("Stack Overflow - Where Developers Learn, Share, & Build Careers", driver.Title);
            checkWindowHandles(1, driver.WindowHandles);

            driver.FindElement(By.LinkText("Log in")).Click();
            checkTitle("Log In - Stack Overflow", driver.Title);
            checkWindowHandles(1, driver.WindowHandles);

            //send bad credentials for log in
            driver.FindElement(By.Id("email")).SendKeys("example@mail.com");
            driver.FindElement(By.Id("password")).SendKeys("bad!Password123");
            driver.FindElement(By.Id("submit-button")).Click();
            //test if it is the same page after error
            checkTitle("Log In - Stack Overflow", driver.Title);
            checkWindowHandles(1, driver.WindowHandles);
            checkFormElementContent("", driver.FindElement(By.Id("email")).Text);
            checkFormElementContent("", driver.FindElement(By.Id("password")).Text);

            //Back to home check title
            driver.Navigate().Back();
            checkTitle("Stack Overflow - Where Developers Learn, Share, & Build Careers", driver.Title);
            checkWindowHandles(1, driver.WindowHandles);

        }

        static void checkTitle(String expected, String fromDriver)
        {
                Assert.True(expected.Equals(fromDriver));
        }

        static void checkWindowHandles(int expected, IList<String> windows)
        {
                Assert.True(expected.Equals(windows.Count));
        }

        static void checkFormElementContent(String expected, String fromDriver)
        {
                Assert.True(expected.Equals(fromDriver));
        }
    }
}
