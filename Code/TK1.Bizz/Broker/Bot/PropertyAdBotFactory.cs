using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz.Broker.Bot.Sources;
using TK1.Html.Bot;

namespace TK1.Bizz.Broker.Bot
{
    public class PropertyAdBotFactory
    {
        public static IPropertyAdBot GetBotInstance(string source)
        {
            IPropertyAdBot bot = null;
            switch (source)
            {
                case "kijiji":
                    bot = new KijijiPropertyAdBot() { BaseUrl = "http://www.kijiji.ca", Source = source };
                    bot.Initialize();
                    break;

            }
            return bot;
        }
    }
}
