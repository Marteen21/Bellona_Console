using Bellona_Console.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    class SpellWithCooldown : Spell{
        private System.Threading.Timer cooldownTimer;
        private int cooldown;
        private bool isitReady;
        public SpellWithCooldown(uint i, ConstController.WindowsVirtualKey kb, int cooldown) : base(i, kb) {
            this.cooldown = cooldown;
        }
        
    }
}
