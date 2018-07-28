﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SimpleBot
{
    class Program
    {
        private const string QR_CODE = "//img[@alt='Scan me!']";
        private const string SEARCH_INPUT = "//input[@title='Search or start new chat']";
        private const string CHAT_INPUT = "composer_rich_textarea";
        private const string UNREAD = "//span[@class='im_dialog_badge badge'][contains(.,'')]";
        private const string TEXTING = "composer_rich_textarea";
        private const string BUTTON = "/html/body/div[1]/div[2]/div/div[2]/div[3]/div/div[3]/div[2]/div/div/div/form/div[3]/button";
        private const string TEXT = "im_message_text";
        private const string NEGARA = "//*[@id='ng-app']/body/div[1]/div/div[2]/div[2]/form/div[1]/div";
        private const string NEGARAO = "//*[@id='ng-app']/body/div[5]/div[2]/div/div/div[2]/div[1]/input";
        private const string NEGARA1 = "//*[@id='ng-app']/body/div[5]/div[2]/div/div/div[2]/div[2]/div/div[1]/ul/li/a";
        private const string NOHP = "phone_number";
        private const string SUBMIT = "//*[@id='ng-app']/body/div[1]/div/div[2]/div[1]/div/a";
        private const string KONF = "body > div.modal.fade.confirm_modal_window.in > div.modal-dialog > div > div > div.md_simple_modal_footer > button.btn.btn-md.btn-md-primary";
        private const string KODE = "phone_code";
        private const string MENU = "//*[@id='ng-app']/body/div[1]/div[1]/div/div/div[1]/div/a";
        private const string CONTACT = "//*[@id='ng-app']/body/div[1]/div[1]/div/div/div[1]/div/ul/li[2]/a";
        private const string CONTACT_NAME = "md_modal_list_peer_name";

        private static int TUNDA = 2000;
        private static IWebDriver _driver = null;

        static void Main(string[] args)
        {
            Console.Title = "Auto Reply Telegram";

            Start();
            Thread.Sleep(TUNDA);
            Console.Clear();

            Console.Write("Login Succeed...");

            Console.Clear();
            LoadContact();
            Console.Clear();
            while (baca());
        }

        static bool Start()
        { 
            try
            {
                _driver = new FirefoxDriver();
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(60);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                _driver.Navigate().GoToUrl("https://web.telegram.org/#/login");
                //_driver.Manage().Window.Maximize();
                /*var negara = _driver.FindElement(By.XPath(NEGARA));
                negara.Click();
                var negarao = _driver.FindElement(By.XPath(NEGARAO));
                negarao.SendKeys("indo");
                var negara1 = _driver.FindElement(By.XPath(NEGARA1));
                negara1.Click();*/
                Thread.Sleep(3000);
                var nohp = _driver.FindElement(By.Name(NOHP));
                Console.WriteLine("Silahkan Masukan No.hp:");
                string nhp = Console.ReadLine();
                nohp.SendKeys(nhp);
                var submit = _driver.FindElement(By.XPath(SUBMIT));
                submit.Click();
                var konf = _driver.FindElement(By.CssSelector(KONF));
                konf.Click();
                Console.WriteLine("Silahkan Masukan Kode Verifikasi:");
                string kode = Console.ReadLine();
                var kde = _driver.FindElement(By.Name(KODE));
                kde.SendKeys(kode);
            }
            catch (NoSuchElementException ee)
            {
                Console.WriteLine(ee);
                return true;
            }
            return true;        
    }
        static void LoadContact()
        {            
            Console.WriteLine("Loading Contact...");

            Thread.Sleep(TUNDA);
            var menu = _driver.FindElement(By.XPath(MENU));
            menu.Click();
            Thread.Sleep(TUNDA);
            var contact = _driver.FindElement(By.XPath(CONTACT));
            contact.Click();
            Thread.Sleep(TUNDA);
            //load nama kontak
            IList<IWebElement> all = _driver.FindElements(By.ClassName(CONTACT_NAME));            
            foreach (IWebElement kontak in all)
            {                
                Console.WriteLine(kontak.Text);
            }

            Console.WriteLine("Load Contact List Succeed...");

            _driver.Navigate().Back();
            Thread.Sleep(3000);
        }

        static bool baca()
        {
            try
            {
                Console.WriteLine("\nMencari Pesan Baru...");
                //klik pesan baru
                IList<IWebElement> unread = _driver.FindElements(By.XPath(UNREAD));//(By.XPath("//span[@class='im_dialog_badge badge']"));//(By.ClassName("im_dialog_badge_muted"));//
                foreach (IWebElement n in unread)
                {
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromHours(24);
                    n.Click();

                    Console.WriteLine("\nBaca Pesan...");
                    //no loop
                    //IWebElement stext = _driver.FindElement(By.ClassName(TEXT));//("//div[@class='im_message_text'][contains(.,'')]"));                  
                    //Console.WriteLine(stext.Text);

                    //loop show text
                    /*IList<IWebElement> text = _driver.FindElements(By.ClassName(TEXT));//XPath("//div[@class='im_message_text'][contains(.,'')]"));//("//div[@class='im_message_text'][contains(text(),'')]"));
                    foreach (IWebElement t in text)
                    {
                        Console.Write(t.Text);
                    }*/
                    //kirim balasan
                    var balas = _driver.FindElement(By.ClassName(TEXTING));
                    balas.SendKeys(("\nPesan diterima @") + DateTime.Now.ToString("dd/MM/yyy HH:mm:ss") + Keys.Enter);
                    //var klik = _driver.FindElement(By.XPath(BUTTON));
                    //klik.Click();   
                   

                    Console.WriteLine("\nBalas Pesan Sukses...");
                }
            }
            catch (NoSuchElementException ee)
            {
                Console.WriteLine(ee);
                return true;
            }
            return true;
        }
    }
}
