using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WebHelper
/// </summary>
public class WebHelper
{
	public WebHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void CreateMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey)
    {
        //string strScript = "<script language=JavaScript>alert('" +
        //                   alertMsg + "')</script>";
        //if (!(senderPage.IsStartupScriptRegistered(alertKey)))
        //    senderPage.RegisterStartupScript(alertKey, strScript);

        //ClientScript
    }
}