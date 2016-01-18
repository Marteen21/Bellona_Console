using Bellona_Console.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    class SpellWithCooldown : Spell{
        public SpellWithCooldown(uint i) : base(i) {
            throw new NotImplementedException();
        }
        public SpellWithCooldown(uint i, ConstController.WindowsVirtualKey kb) : base(i, kb) {
            throw new NotImplementedException();
        }
    }
}
