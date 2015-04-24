using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pandolfo.Const;
using TK1.Data;
using TK1.Bizz;
using TK1.Bizz.Pandolfo;

public partial class Contato_Default : System.Web.UI.Page
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
        string message = textBoxContactMessage.Text;



        try
        {
            string body = HtmlTemplates.GetContactMailTemplate();
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
            AppLogController.WriteException("Pandolfo.FaleConosco_Default.createMailBody", exception, true);
        }
        return result;
    }
    private void createMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey)
    {
        string strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", strScript);
    }
    private void sendMessage()
    {
        string subject = "Mensagem enviada através do site Pandolfo Imóveis";
        string body = createMailBody();
        var mailHelper = new PandolfoMailHelper();
        string mailTo = string.Empty;
        switch (radioButtonListContactType.SelectedValue)
        {
            case "Aluguel":
                mailTo = MailAdresses.Aluguel;
                break;
            case "Vendas":
                mailTo = MailAdresses.Vendas;
                break;
            default:
                mailTo = MailAdresses.Contato;
                break;
        }
        mailHelper.SendMail(subject, body, mailTo, true);
        createMessageAlert(this, "Mensagem enviada", "");
    }


    protected void buttonSendMessage_OnClick(object sender, EventArgs e)
    {
        sendMessage();
    }

}