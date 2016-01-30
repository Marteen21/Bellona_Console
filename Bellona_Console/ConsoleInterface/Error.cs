using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.ConsoleInterface {
    class Error {
        public static ConsoleColor Color = ConsoleColor.Red;
        public static uint ErrorCount = 0;

        string msg;
        #region propertys


        public string Msg {
            get {
                return msg;
            }

            set {
                msg = value;
            }
        }
        #endregion
        public Error() {
            this.Msg = "Unkown Error";
        }
        public Error(string m) {
            this.Msg = m;
        }
    }
}
