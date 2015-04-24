using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK1.Dev.UnitTest;
using TK1.Utility;

namespace App.ConsoleDev45
{
    class Program
    {
        static void Main(string[] args)
        {
            AppOutput.LogFileDirectory = @"D:\_TEMP\LOGS";
            AppOutput.LogFileName = @"TK1.DevApp";
            AppOutput.WriteConsole = true;
            AppOutput.WriteFile = true;
            AppOutput.WriteExecutionStart();
            try
            {
                runDevTests();
                //runFullTests();
            }
            catch (Exception exception)
            {
                AppOutput.Write(exception);
            }
            finally
            {
                AppOutput.WriteExecutionEnd();
                Console.ReadKey();
            }
        }

        private static void runDevTests()
        {
            //NetUnitTests.HttpGet();
            //BizzUnitTest.BrokerBotCleanupTest();
            //BizzUnitTest.BrokerBotFileFullTest();
            //BizzUnitTest.BrokerBotWebFull();
            //BizzUnitTest.BrokerBotWebSingleTest();
            //BizzUnitTest.BrokerDBTest();
            //BizzUnitTest.BingTest();
        }

        private static void runFullTests()
        {
        }
    }
}
