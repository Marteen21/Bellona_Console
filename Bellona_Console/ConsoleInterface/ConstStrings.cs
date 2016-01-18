using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.ConsoleInterface {
    class ConstStrings {
        public static Error InitError = new Error("No compatible clients running");
        public static Error ReadError = new Error("Exception during memory read");
        public static Message WelcomeMessage = new Message("Welcome to Bellona semi-auto DPS bot for World of Warcraft 4.3.4 (15595)");
        public static Error BuffError = new Error("Exception during buff refresh, rechecking buff address...");
        public static Error GameObjectConstructorError = new Error("Exception during a creation of a GameObject");
        public static Message GoodByeMessega = new Message("The program reached its end");

    }
}
