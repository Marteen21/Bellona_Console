using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;

namespace Bellona_Console.Models {
    public class Spell {
        private uint id;
        private ConstController.WindowsVirtualKey keybind;
        //Bela
        public uint ID {
            get {
                return id;
            }

            set {
                id = value;
            }
        }
        public Spell(uint i) {
            this.ID = i;

        }
        public Spell(uint i, ConstController.WindowsVirtualKey kb) {
            this.ID = i;
            this.keybind = kb;
        }
        public void SendCast() {
            SendKey.Send(this.keybind);
        }

        public bool CastIfHasBuff(WoWGlobal wowinfo, WoWUnit unit) {
            if (unit.HasBuff(this.ID)) {
                this.SendCast();
                return true;
            }
            return false;
        }
    }
}
