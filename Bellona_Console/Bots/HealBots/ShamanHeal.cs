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

        public ShamanHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Shaman Resto Heal bot");
        }

        public override void Rota() {
            
        }
    }
}
