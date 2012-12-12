using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Pandolfo
{
    public class MailTemplateTags
    {
        public class General
        {
            public const string Mail = "#TK1_TAG_MAIL#";
            public const string Message = "#TK1_TAG_MESSAGE#";
            public const string Name = "#TK1_TAG_NAME#";
            public const string Result = "#TK1_TAG_RESULT#";
            public const string Timestamp = "#TK1_TAG_TIMESTAMP#";
        }

        public class SiteContact
        {
            public const string ContactType = "#TK1_TAG_CONTACT_TYPE#";
            public const string Phone = "#TK1_TAG_PHONE#";
            public const string Contact = "#TK1_TAG_CONTACT#";
            public const string SiteAdID = "#TK1_TAG_SITE_AD_ID#";
            public const string SiteAdLink = "#TK1_TAG_SITE_AD_LINK#";

        }
        public class XmlFileLoad
        {
        }
    }
}
