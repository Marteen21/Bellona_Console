using Bellona_Console.Bots;
using Bellona_Console.ConsoleInterface;
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
        const string PROCESS_WINDOW_TITLE = "World of Warcraft";    //Window title of the game
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
            //Initialize if fail end program
            if (!Initializer.ConnectToGame(out wow, PROCESS_WINDOW_TITLE)) {
                WowPrinter.PrintExit(ConstStrings.InitError);
                return;
            }
            else {
                //Init success
                clientInfo = new WoWGlobal(wow);
                WowPrinter.Print(clientInfo);
                GameObject PlayerObject = new GameObject(wow, (UInt64)clientInfo.PlayerGUID);
                GameObject TargetObject = new GameObject(wow, (UInt64)clientInfo.TargetGUID);
                WowPrinter.Print(TargetObject);
                switch (PlayerObject.Unit.WowClass) {
                    case WoWClass.Druid:
                        DruidDPS mydbot = new DruidDPS(wow, clientInfo, 100);
                        break;
                    case WoWClass.Warlock:
                        WarlockDPS mywbot = new WarlockDPS(wow, clientInfo, 100);
                        break;
                    case WoWClass.DeathKnight:
                        DeathKnightDPS mydkbot = new DeathKnightDPS(wow, clientInfo, 100);
                        break;
                }
                bool temp = true;
                while (temp) {
                    switch (Console.ReadKey().Key) {
                        case ConsoleKey.R:
                            // Starts a new instance of the program itself
                            var fileName = Assembly.GetExecutingAssembly().Location;
                            System.Diagnostics.Process.Start(fileName);
                            Environment.Exit(0);
                            break;
                        case ConsoleKey.T:
                            temp = false;
                            break;
                    }
                }
                //WowPrinter.PrintExit(ConstStrings.GoodByeMessega);
                return;
            }

        }



    }
}
