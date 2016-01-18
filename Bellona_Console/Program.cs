using Bellona_Console.Bots;
using Bellona_Console.ConsoleInterface;
using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (PlayerObject.Unit.WowClass == WoWClass.Warlock) {
                    WarlockDPS mybot = new WarlockDPS(wow, clientInfo, 100);
                }
                while (Console.ReadLine() != "STOP") {

                }
                WowPrinter.PrintExit(ConstStrings.GoodByeMessega);
                return;
            }

        }



    }
}
