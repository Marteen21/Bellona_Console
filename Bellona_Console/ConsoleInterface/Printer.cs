using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.ConsoleInterface {
    class Printer {
        public static ConsoleColor Color = ConsoleColor.DarkMagenta;
        public static int Paddistance = 30;
        public Printer() {  //Default Constructor
            Init();
        }
        public void Init() {
            SetConsoleColors();
        }
        public void SetConsoleColors() {
            Console.BackgroundColor = Printer.Color;
            Console.ForegroundColor = Message.Color;
            Console.Clear();

        }
        public void Print(Error e) {
            Console.ForegroundColor = Error.Color;
            Console.WriteLine(("Error " + "\"" +  e.Msg + "\""));
            Console.ForegroundColor = Message.Color;
        }
        public void Print(Message m) {
            Console.WriteLine(m.Msg);
        }
        public void Print(WoWGlobal w) {
            Type type = w.GetType();
            PropertyInfo[] properties = type.GetProperties();
            Console.WriteLine("Printing Info about a " + type.ToString());
            foreach (PropertyInfo property in properties) {
                if (property.Name.Contains("GUID")) {
                    Console.WriteLine(property.Name.ToString().PadRight(Paddistance) + String.Format("0x{0:X16}",property.GetValue(w, null)));

                }
                else {
                    Console.WriteLine(property.Name.ToString().PadRight(Paddistance) + property.GetValue(w, null));
                }
            }
        }
        public void Print(List<uint> bufflist, int padleft) {
            foreach(uint i in bufflist) {
                Console.WriteLine("".PadLeft(padleft) +i);
            }
        }
        public void Print(WoWUnit u) {
            Type type = u.GetType();
            PropertyInfo[] properties = type.GetProperties();
            Console.WriteLine("Printing Info about a " + type.ToString());
            foreach (PropertyInfo property in properties) {
                    Console.WriteLine(property.Name.ToString().PadRight(Paddistance) + property.GetValue(u, null));
            }
            Print(u.Buffs, 3);
        }
        public void Print(WoWUnit u, int padleft) {
            Type type = u.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties) {
                Console.WriteLine("".PadLeft(padleft)+(property.Name.ToString().PadRight(Paddistance) + property.GetValue(u, null)));
            }
            Print(u.Buffs,2*padleft);
        }
        public void Print(GameObject go) {
            Type type = go.GetType();
            PropertyInfo[] properties = type.GetProperties();
            Console.WriteLine("Printing Info about a " + type.ToString());
            foreach (PropertyInfo property in properties) {
                if (property.Name.Contains("GUID")) {
                    Console.WriteLine(property.Name.ToString().PadRight(Paddistance) + String.Format("0x{0:X16}", property.GetValue(go, null)));

                }
                else if(property.Name.Contains("Address")){
                    uint temp = (uint)(UIntPtr)property.GetValue(go, null);
                    Console.WriteLine(property.Name.ToString().PadRight(Paddistance) + String.Format("0x{0:X8}", temp));
                }
                else {
                    Console.WriteLine(property.Name.ToString().PadRight(Paddistance) + property.GetValue(go, null));
                }
            }
            Print(go.Unit,3);
        }
        public void PrintExit(Error e) {
            Print(e);
            Print(new Message("Press Enter to exit"));
            Console.ReadLine();
            return;
        }
        public void PrintExit(Message m) {
            Print(m);
            Print(new Message("Press Enter to exit"));
            Console.ReadLine();
            return;
        }
    }
}
