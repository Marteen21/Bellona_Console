using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;

namespace Bellona_Console.Bots.HealBots {
    class ShamanHeal : HealPartyBot {

        Spell waters = new Spell(52127, ConstController.WindowsVirtualKey.K_6);



        public ShamanHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Shaman Resto Heal bot");
        }

        public override void Rota() {
            if (!this.Player.Unit.HasBuff(waters.ID)) {
                waters.SendCast();
            }
            
        }
    }
}
