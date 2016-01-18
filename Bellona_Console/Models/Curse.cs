using Bellona_Console.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    public class Curse : DoT {
        private const uint cot = 1714;
        private const uint cote = 1490;
        private const uint cow = 702;
        private const uint coe = 18223;
        public Curse(uint i) : base(i) {

        }
        public Curse(uint i, ConstController.WindowsVirtualKey kb) : base(i, kb) {

        }
        public override bool ReCast(WoWGlobal wowinfo, WoWUnit unit) {
            if (!unit.HasBuff(cot) && !unit.HasBuff(cote) && !unit.HasBuff(cow) && !unit.HasBuff(coe)) {
                this.SendCast();
                return true;
            }
            else {
                return false;
            }
        }
    }
}
