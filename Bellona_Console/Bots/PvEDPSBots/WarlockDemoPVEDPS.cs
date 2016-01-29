﻿using Bellona_Console.Controller;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.PvEDPSBots {
    class WarlockDemoPVEDPS : PvEDPSBot {
        private Spell shadowTrance = new Spell(17941, ConstController.WindowsVirtualKey.VK_NUMPAD0);
        private Spell moltenCore = new Spell(71165, ConstController.WindowsVirtualKey.VK_NUMPAD1);
        private DoT immolate = new DoT(348, ConstController.WindowsVirtualKey.VK_NUMPAD2);
        private DoT corruption = new DoT(172, ConstController.WindowsVirtualKey.VK_NUMPAD3);
        private DoT baneofdoom = new DoT(603, ConstController.WindowsVirtualKey.VK_NUMPAD4);
        private DoT handofguldan = new DoT(86000, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        private Spell soulFire = new Spell(63167, ConstController.WindowsVirtualKey.VK_NUMPAD6);
        private Spell lifeTap = new Spell(1454, ConstController.WindowsVirtualKey.VK_NUMPAD7);

            
        private WalkerBot followFocus;

        public WarlockDemoPVEDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint dpsTimerInterval, uint walkerTimerInterval) : base(wowProcess, globalinfo, dpsTimerInterval) {
            Console.WriteLine("Initialize Warlock Demonology Complex PvE (Beta) bot");
            followFocus = new WalkerBot(this.wow, this.wowinfo, walkerTimerInterval, WalkTargetType.CurrentFocus,10);
        }
        public WarlockDemoPVEDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint dpsTimerInterval) : base(wowProcess, globalinfo, dpsTimerInterval) {
            Console.WriteLine("Initialize Warlock Demonology Complex PvE (Beta) bot");
        }
        public override void Rota() {
            if (Player.Unit.GetHealthPercent() > 40  && Player.Unit.GetManaPercent() < 50) {
                lifeTap.SendCast();
            }
            corruption.ReCast(this.wowinfo, this.Target.Unit);
            immolate.ReCast(this.wowinfo, this.Target.Unit);
            baneofdoom.ReCast(this.wowinfo, this.Target.Unit);
            if (this.Target.Unit.HasBuff(immolate.ID) && this.Target.Unit.HasBuff(corruption.ID) && this.Target.Unit.HasBuff(baneofdoom.ID)) {
                shadowTrance.CastIfHasBuff(this.wowinfo, this.Player.Unit);
                handofguldan.SendCast();
                if (!moltenCore.CastIfHasBuff(this.wowinfo, this.Player.Unit) && !soulFire.CastIfHasBuff(this.wowinfo, this.Player.Unit)) {
                    shadowTrance.SendCast();
                }
            }
        }
    }
}
