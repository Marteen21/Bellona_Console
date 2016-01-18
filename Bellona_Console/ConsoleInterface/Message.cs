using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.ConsoleInterface {
    class Message {
        public static ConsoleColor Color = ConsoleColor.White;
        string msg;
        public string Msg {
            get {
                return msg;
            }

            set {
                msg = value;
            }
        }


        public Message(string m) {
            this.Msg = m;
        }
    }
}
