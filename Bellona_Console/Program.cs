using Bellona_Console.Bots;
using Bellona_Console.Bots.ComplexBots;
using Bellona_Console.Bots.DPSBots;
using Bellona_Console.Bots.HealBots;
using Bellona_Console.Bots.PVEDPSBots;
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
        public static string[] myargs;

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

        public static WoWGlobal ClientInfo {
            get {
                return clientInfo;
            }

            set {
                clientInfo = value;
            }
        }

        #endregion

        static void Main(string[] args) {
            myargs = args;
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
                ClientInfo = new WoWGlobal(wow);
                WowPrinter.Print(ClientInfo);
                GameObject PlayerObject = new GameObject(wow, (UInt64)ClientInfo.PlayerGUID);
                GameObject TargetObject = new GameObject(wow, (UInt64)ClientInfo.TargetGUID);
                WoWRaid wr = new WoWRaid(wow);
                WoWParty wp = new WoWParty(wow);
                WowPrinter.Print(wp, 1);
                WowPrinter.Print(wr, 1);
                WowPrinter.Print(TargetObject); //For debug
                //TestBot tb = new TestBot(100,100, ComplexBotStance.DpsTargetRanged);
                //WalkBehindBot kutya = new WalkBehindBot(wow, clientInfo, 100, WalkTargetType.CurrentTarget, 1);
                InitPvEBotBasedonClass(args, PlayerObject.Unit.WowClass);
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
        public static void RestartApp(string[] args) {
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
                    DruidDPS mydbot = new DruidDPS(wow, ClientInfo, 100);
                    break;
                case WoWClass.Warlock:
                    if (args.Length > 1) {
                        WarlockDemoPVEDPS mywbot = new WarlockDemoPVEDPS(wow, ClientInfo, 100, 1);
                    }
                    else {
                        WarlockDPS mywbot = new WarlockDPS(wow, ClientInfo, 100);
                    }
                    break;
                case WoWClass.DeathKnight:
                    DeathKnightBloodDPS mydkbot = new DeathKnightBloodDPS(wow, ClientInfo, 100);
                    break;
                case WoWClass.Paladin:
                    PaladinDPS mypbot = new PaladinDPS(wow, ClientInfo, 100);
                    break;
                case WoWClass.Mage:
                    MageFireDPS mymbot = new MageFireDPS(wow, ClientInfo, 100);
                    break;
                case WoWClass.Shaman:
                    ShamanHeal mysbot = new ShamanHeal(wow, ClientInfo, 100, 1);
                    break;
                case WoWClass.Priest:
                    PriestDiscHeal mypdbot = new PriestDiscHeal(wow, ClientInfo, 200);
                    break;
                case WoWClass.Warrior:
                    WarriTank mypwbot = new WarriTank(wow, ClientInfo, 100);
                    break;

            }
        }
        private static void InitPvEBotBasedonClass(string[] args, WoWClass myclass) {
            switch (myclass) {
                case WoWClass.Druid:
                    DruidPVEDPS mydbot = new DruidPVEDPS(wow, ClientInfo, 100, 1);
                    break;
                case WoWClass.Warlock:
                    WarlockDemoComplex mywbot = new WarlockDemoComplex(100, 100);
                    break;
                case WoWClass.DeathKnight:
                    DeathKnightBloodTank mydkbot = new DeathKnightBloodTank(wow, ClientInfo, 100);
                    break;
                case WoWClass.Paladin:
                    PaladinPVEDPS mypbot = new PaladinPVEDPS(wow, ClientInfo, 100, 1);
                    break;
                case WoWClass.Mage:
                    MageFirePVEDPS mymbot = new MageFirePVEDPS(wow, ClientInfo, 100, 1);
                    break;
                case WoWClass.Shaman:
                    ShamanHeal mysbot = new ShamanHeal(wow, ClientInfo, 100, 1);
                    break;
                case WoWClass.Priest:
                    PriestDiscHeal mypdbot = new PriestDiscHeal(wow, ClientInfo, 200);
                    break;
                case WoWClass.Rogue:
                    RogueComplex myrbot = new RogueComplex(100, 100);
                    break;
                case WoWClass.Warrior:
                    WarriTank mypwbot = new WarriTank(wow, ClientInfo, 100);
                    break;
            }
        }
    }
}
