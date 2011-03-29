using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TK1.Bizz.Pieta;
using TK1.Bizz.Pieta.Data;
using System.IO;
using TK1.Bizz.Pieta.Const;

public partial class FaleConosco_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void buttonSendMessage_OnClick(object sender, EventArgs e)
    {
        sendMessage();
    }

    private void sendMessage()
    {
        string subject = "Mensagem enviada através do site Pietá Imóveis";
        string body = createMailBody();
        MailHelper.SendContactMail(subject, body);
        CreateMessageAlert(this, "Mensagem enviada", "");
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
            //if (File.Exists(Paths.ContactMailTemplate))
            //{
                string body = HtmlTemplates.GetContactMailTemplate();
                //string body = File.ReadAllText(Paths.ContactMailTemplate);
                //if (!string.IsNullOrEmpty(body))
                //{
                    if (body.Contains(MailTemplateTags.Contact))
                        body = body.Replace(MailTemplateTags.Contact, contact);
                    if (body.Contains(MailTemplateTags.ContactType))
                        body = body.Replace(MailTemplateTags.ContactType, contactType);
                    if (body.Contains(MailTemplateTags.Mail))
                        body = body.Replace(MailTemplateTags.Mail, mail);
                    if (body.Contains(MailTemplateTags.Message))
                        body = body.Replace(MailTemplateTags.Message, message);
                    if (body.Contains(MailTemplateTags.Name))
                        body = body.Replace(MailTemplateTags.Name, name);
                    if (body.Contains(MailTemplateTags.Phone))
                        body = body.Replace(MailTemplateTags.Phone, phone);
                    if (body.Contains(MailTemplateTags.Timestamp))
                        body = body.Replace(MailTemplateTags.Timestamp, timestamp);

                    result = body;
                //}
            //}
        }
        catch (Exception exception)
        {
            LogController.WriteException("FaleConosco_Default.createMailBody", exception);
        }
        return result;
    }

    public  void CreateMessageAlert(System.Web.UI.Page senderPage, string alertMsg, string alertKey)
    {
        string strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";

        //if (!(senderPage.IsStartupScriptRegistered(alertKey)))
        //    senderPage.RegisterStartupScript(alertKey, strScript);

        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", strScript);
    }

}