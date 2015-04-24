using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.ConsoleDev4.Tests;

namespace App.ConsoleDev4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                runDevTests();
                Console.ReadKey();
            }
            catch (Exception exception)
            {
                
            }
        }

        private static void runDevTests()
        {
            //runDataTests();
            runMailTests();
        }

        private static void runDataTests()
        {
            Console.WriteLine("Starting Data Tests");
            var dataTests = new DataTests();
            //Console.WriteLine("ClientAppLogWrite");
            //dataTests.ClientAppLogWrite();
            Console.WriteLine("ClientPropertyAddSelect");
            dataTests.ClientPropertyAddSelect();
        }
        private static void runMailTests()
        {
            var before = DateTime.Now;
            Console.WriteLine("Starting Mail Tests");
            var tests = new MailTests();
            Console.WriteLine("SendTK1Mail");
            tests.SendTK1Mail();
            var diff = DateTime.Now-before;
            Console.WriteLine(string.Format("Mail Tests Finished. Elapsed: {0}m {1}s {2}ms", (int)diff.TotalMinutes, (int)diff.TotalSeconds - (int)diff.TotalMinutes * 60, (int)diff.TotalMilliseconds - (int)diff.TotalSeconds * 1000));
            Console.WriteLine(diff.TotalMilliseconds.ToString());
        }
    }
}
