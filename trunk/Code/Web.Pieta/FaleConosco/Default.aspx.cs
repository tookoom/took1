using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta;
using System.IO;
using TK1.Bizz.Pieta.Const;
using TK1.Data;
using TK1.Bizz.Mdo;
using TK1.Bizz;

public partial class FaleConosco_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private string createMailBody()
    {
        string result = string.Empty;
        string contactType = radioButtonListContactType.SelectedValue;
        string timestamp = DateTime.Now.ToString();
        string name = textBoxContactName.Text;
        string mail = textBoxContactMail.Text;
        string phone = textBoxContactPhone.Text;
        string contact = radioButtonListResponseType.SelectedValue;
        string message = textBoxContactMessage.Text;



        try
        {
            string body = HtmlTemplates.GetContactMailTemplate();
            if (body.Contains(MailTemplateTags.SiteContact.Contact))
                body = body.Replace(MailTemplateTags.SiteContact.Contact, contact);
            if (body.Contains(MailTemplateTags.SiteContact.ContactType))
                body = body.Replace(MailTemplateTags.SiteContact.ContactType, contactType);
            if (body.Contains(MailTemplateTags.General.Mail))
                body = body.Replace(MailTemplateTags.General.Mail, mail);
            if (body.Contains(MailTemplateTags.General.Message))
                body = body.Replace(MailTemplateTags.General.Message, message);
            if (body.Contains(MailTemplateTags.General.Name))
                body = body.Replace(MailTemplateTags.General.Name, name);
            if (body.Contains(MailTemplateTags.SiteContact.Phone))
                body = body.Replace(MailTemplateTags.SiteContact.Phone, phone);
            if (body.Contains(MailTemplateTags.General.Timestamp))
                body = body.Replace(MailTemplateTags.General.Timestamp, timestamp);

            result = body;
        }
        catch (Exception exception)
        {
            AppLogController.WriteException("FaleConosco_Default.createMailBody", exception, true);
        }
        return result;
    }
    private void createMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey)
    {
        string strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";

        //if (!(senderPage.IsStartupScriptRegistered(alertKey)))
        //    senderPage.RegisterStartupScript(alertKey, strScript);

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", strScript);
    }
    private void sendMessage()
    {
        string subject = "Mensagem enviada através do site Pietá Imóveis";
        string body = createMailBody();
        AdminHelper.SendMail(subject, body, "emailContato");
        createMessageAlert(this, "Mensagem enviada", "");
    }


    protected void buttonSendMessage_OnClick(object sender, EventArgs e)
    {
        sendMessage();
    }

}