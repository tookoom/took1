using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Data.Controller;
using TK1.Bizz;

namespace TK1.Dev.UnitTest
{
    public class TK1DataUnitTest
    {
        public static void TestAudit()
        {
            AuditController audit = null;
            try
            {

                audit = new AuditController(AppNames.IntegraMdoSelling.ToString(), CustomerNames.Pietá.ToString());
                audit.StartProcessExecution();
                audit.WriteEvent("Teste", "Testanto");
                try
                {
                    throw new Exception("ExceptionTest");
                }
                catch (Exception exception)
                {
                    audit.WriteException("Exception", exception);
                }
                audit.FinishProcessExecution(true);
            }
            catch (Exception exception)
            {
                if (audit != null)
                    audit.FinishProcessExecution(false);
            }
        }
    }
}
