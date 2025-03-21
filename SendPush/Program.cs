using System;
using System.Configuration;
using System.Linq;
using PushoverClient;

namespace SendPush
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Get the settings defaults
            string appKey = ConfigurationManager.AppSettings["appKey"];
            string userGroupKey = ConfigurationManager.AppSettings["userGroupKey"];

            // Version without the commandline parser:
            {
                if (args.Count() < 2)
                {
                    Console.Error.WriteLine("Usage: <title> <message> {<from>} {<user>}");
                    Environment.Exit(1);
                }

                string optionsTitle = args[0];
                string optionsMessage = args[1];

                string optionsFrom;
                //  If we didn't get the app key passed in, use the default:
                if (args.Count() > 2 )
                {
                    optionsFrom = args[2];
                }
                else
                {
                    optionsFrom = appKey;
                }

                string optionsUser;
                //  If we didn't get the user key passed in, use the default:
                if (args.Count() > 3)
                {
                    optionsUser = args[3]; ;
                }
                else
                {
                    optionsUser = userGroupKey;
                }

                //  Make sure we have our required items:
                if (true)
                {
                    //  Send the message
                    Pushover pclient = new Pushover(optionsFrom);
                    PushResponse response = pclient.Push(optionsTitle, optionsMessage, optionsUser);
                }

            }
        }
    }
}
