using Bellona_Console.Controller;
using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Bellona_Console.Other;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.DPSBots {
    class WarlockDPS : DPSBot {
        private static DoT corruption = new DoT(172, ConstController.WindowsVirtualKey.VK_NUMPAD0);
        private static DoT haunt = new DoT(48181, ConstController.WindowsVirtualKey.VK_NUMPAD1);
        private static DoT unstableAffliction = new DoT(30108, ConstController.WindowsVirtualKey.VK_NUMPAD2);
        private static DoT baneofAgony = new DoT(980, ConstController.WindowsVirtualKey.VK_NUMPAD3);
        private static Spell shadowTrance = new Spell(17941, ConstController.WindowsVirtualKey.VK_NUMPAD4);
        private static DoT drainLife = new DoT(689, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        private static DoT drainSoul = new DoT(1120, ConstController.WindowsVirtualKey.VK_NUMPAD6);
        private static Spell fellFlame = new Spell(77799, ConstController.WindowsVirtualKey.VK_NUMPAD7);
        private static Curse curseoftheElements = new Curse(1490, ConstController.WindowsVirtualKey.VK_NUMPAD8);
        private static Spell healthstone = new Spell(1, ConstController.WindowsVirtualKey.VK_NUMPAD9);
        private static Spell soulSwapExhale = new Spell(86211, ConstController.WindowsVirtualKey.VK_PRIOR);
        private static Spell soulSwap = new Spell(2, ConstController.WindowsVirtualKey.VK_NEXT);

        private static Spell lifeTap = new Spell(1454, ConstController.WindowsVirtualKey.K_T);
        public WarlockDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Warlock Affliction DPS bot");
        }
        public override void Rota() {
            if (Player.Unit.GetHealthPercent() < 40) {
                healthstone.SendCast();
            }
            if (Player.Unit.GetHealthPercent() > 70 && Player.Unit.GetManaPercent() < 50) {
                lifeTap.SendCast();
            }
            if (!WarlockDPS.corruption.ReCast(this.wowinfo, this.Target.Unit) && !WarlockDPS.baneofAgony.ReCast(this.wowinfo, this.Target.Unit) && !WarlockDPS.shadowTrance.CastIfHasBuff(this.wowinfo, this.Player.Unit)) {
                if (this.Target.Unit.HasBuffs(new List<uint>() { corruption.ID, baneofAgony.ID, unstableAffliction.ID, }) && !this.Player.Unit.HasBuff(WarlockDPS.soulSwapExhale.ID)) {
                    WarlockDPS.soulSwap.SendCast();
                }
                if (this.Focus.GUID != 0 && this.Focus.GUID != this.Target.GUID) {
                    WarlockDPS.soulSwapExhale.CastIfHasBuff(this.wowinfo, this.Player.Unit);
                }
                if (!this.Player.Unit.MovingInfo.IsMoving) {
                    if(!WarlockDPS.unstableAffliction.ReCast(this.wowinfo, this.Target.Unit)) {
                        WarlockDPS.haunt.SendCast();
                        WarlockDPS.curseoftheElements.ReCast(this.wowinfo, this.Target.Unit);
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

