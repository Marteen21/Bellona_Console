using Bellona_Console.Controller;
using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    class WarlockDPS : DPSBot {
        private static DoT corruption = new DoT(172, ConstController.WindowsVirtualKey.K_5);
        private static DoT unstableAffliction = new DoT(30108, ConstController.WindowsVirtualKey.K_7);
        private static DoT haunt = new DoT(48181, ConstController.WindowsVirtualKey.K_6);
        private static DoT baneofAgony = new DoT(980, ConstController.WindowsVirtualKey.K_8);
        private static Spell shadowTrance = new Spell(17941, ConstController.WindowsVirtualKey.K_9);
        private static DoT drainLife = new DoT(689, ConstController.WindowsVirtualKey.K_Ö);
        private static DoT drainSoul = new DoT(1120, ConstController.WindowsVirtualKey.K_Ü);
        private static Spell fellFlame = new Spell(77799, ConstController.WindowsVirtualKey.K_Ő);
        private static Curse curseoftheElements = new Curse(1490, ConstController.WindowsVirtualKey.K_Á);
        private static Spell lifeTap = new Spell(1454, ConstController.WindowsVirtualKey.K_T);

        public WarlockDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
        }
        public override void Rota() {
            if (!WarlockDPS.corruption.ReCast(this.wowinfo, this.Target.Unit) && !WarlockDPS.baneofAgony.ReCast(this.wowinfo, this.Target.Unit) && !WarlockDPS.shadowTrance.IfCast(this.wowinfo, this.Target.Unit)) {
                if (!this.Player.Unit.IsMoving) {
                    if (!WarlockDPS.unstableAffliction.ReCast(this.wowinfo, this.Target.Unit) && !WarlockDPS.haunt.ReCast(this.wowinfo, this.Target.Unit)) {
                        WarlockDPS.drainLife.ReCast(this.wowinfo, this.Target.Unit);
                    }
                }
                else {
                    if (!WarlockDPS.curseoftheElements.ReCast(this.wowinfo, this.Target.Unit)) {
                        WarlockDPS.fellFlame.SendCast();
                    }
                }

            }
        }
    }

}

