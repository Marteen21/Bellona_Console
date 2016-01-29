using Bellona_Console.Bots;
using Bellona_Console.Bots.HealBots;
using Bellona_Console.Bots.PvEDPSBots;
using Bellona_Console.ConsoleInterface;
using Bellona_Console.Controller;
using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bellona_Console {
    class Program {
        public static string PROCESS_WINDOW_TITLE = "World of Warcraft";    //Window title of the game
        private static BlackMagic wow;
        private static Printer wowPrinter = new Printer();
        private static WoWGlobal clientInfo;

        #region propertys
        public static BlackMagic Wow {
            get {
                return wow;
            }

            set {
                wow = value;
            }
        }

        internal static Printer WowPrinter {
            get {
                return wowPrinter;
            }

            set {
                wowPrinter = value;
            }
        }

        #endregion

        static void Main(string[] args) {
            WowPrinter.Print(ConstStrings.WelcomeMessage);
            if (args.Length > 0) {
                PROCESS_WINDOW_TITLE = args[0].ToString();  //If has arguments then connect to that window, not default
            }
            wowPrinter.Print(new Message("Connecting to window named " + PROCESS_WINDOW_TITLE));
            if (!Initializer.ConnectToGame(out wow, PROCESS_WINDOW_TITLE)) {
                WowPrinter.PrintExit(ConstStrings.InitError);            //Initialize if fail terminate the program
                return;
            }
            else {
                //Init success
                clientInfo = new WoWGlobal(wow);
                WowPrinter.Print(clientInfo);
                GameObject PlayerObject = new GameObject(wow, (UInt64)clientInfo.PlayerGUID);
                GameObject TargetObject = new GameObject(wow, (UInt64)clientInfo.TargetGUID);
                WoWRaid wr = new WoWRaid(wow);
                WoWParty wp = new WoWParty(wow);
                WowPrinter.Print(wp, 1);
                WowPrinter.Print(wr, 1);
                WowPrinter.Print(TargetObject); //For debug
                WalkBehindBot kutya = new WalkBehindBot(wow, clientInfo, 100, WalkTargetType.CurrentTarget, 1);
                //InitPvEBotBasedonClass(args, PlayerObject.Unit.WowClass);
                bool temp = true;
                while (temp) {
                    switch (Console.ReadKey().Key) {
                        case ConsoleKey.R:
                            RestartApp(args);
                            break;
                        case ConsoleKey.T:
                            temp = false;
                            break;
                    }
                }
                return;
            }

        }
        private static void RestartApp(string[] args) {
            System.Diagnostics.Process proci = new System.Diagnostics.Process();
            proci.StartInfo.FileName = Assembly.GetExecutingAssembly().Location;
            if (args.Length == 2) {
                proci.StartInfo.Arguments = args[0] + " " + args[1];
            }
            else if (args.Length == 1) {
                proci.StartInfo.Arguments = args[0];
            }
            proci.Start();
            Environment.Exit(0);
        }
        private static void InitPvPBotBasedonClass(string[] args, WoWClass myclass) {
            switch (myclass) {
                case WoWClass.Druid:
                    DruidDPS mydbot = new DruidDPS(wow, clientInfo, 100);
                    break;
                case WoWClass.Warlock:
                    if (args.Length > 1) {
                        WarlockDemoPVEDPS mywbot = new WarlockDemoPVEDPS(wow, clientInfo, 100, 1);
                    }
                    else {
                        WarlockDPS mywbot = new WarlockDPS(wow, clientInfo, 100);
                    }
                    break;
                case WoWClass.DeathKnight:
                    DeathKnightBloodDPS mydkbot = new DeathKnightBloodDPS(wow, clientInfo, 100);
                    break;
                case WoWClass.Paladin:
                    PaladinDPS mypbot = new PaladinDPS(wow, clientInfo, 100);
                    break;
                case WoWClass.Mage:
                    MageFireDPS mymbot = new MageFireDPS(wow, clientInfo, 100);
                    break;
                case WoWClass.Shaman:
                    ShamanHeal mysbot = new ShamanHeal(wow, clientInfo, 100, 1);
                    break;
                case WoWClass.Priest:
                    PriestDiscHeal mypdbot = new PriestDiscHeal(wow, clientInfo, 200);
                    break;

            }
        }
        private static void InitPvEBotBasedonClass(string[] args, WoWClass myclass) {
            switch (myclass) {
                case WoWClass.Druid:
                    DruidPVEDPS mydbot = new DruidPVEDPS(wow, clientInfo, 100,1);
                    break;
                case WoWClass.Warlock:
                    WarlockDemoPVEDPS mywbot = new WarlockDemoPVEDPS(wow, clientInfo, 100, 1);
                    break;
                case WoWClass.DeathKnight:
                    DeathKnightBloodDPS mydkbot = new DeathKnightBloodDPS(wow, clientInfo, 100);
                    break;
                case WoWClass.Paladin:
                    PaladinPVEDPS mypbot = new PaladinPVEDPS(wow, clientInfo, 100,1);
                    break;
                case WoWClass.Mage:
                    MageFirePVEDPS mymbot = new MageFirePVEDPS(wow, clientInfo, 100, 1);
                    break;
                case WoWClass.Shaman:
                    ShamanHeal mysbot = new ShamanHeal(wow, clientInfo, 100, 1);
                    break;
                case WoWClass.Priest:
                    PriestDiscHeal mypdbot = new PriestDiscHeal(wow, clientInfo, 200);
                    break;

            }
        }
    }
}
