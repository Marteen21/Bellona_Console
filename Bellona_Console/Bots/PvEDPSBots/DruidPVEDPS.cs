using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;
using Bellona_Console.Bots.Other;

namespace Bellona_Console.Bots.PvEDPSBots {
    class DruidPVEDPS : PvEDPSBot {

        private DoT rake = new DoT(1822, ConstController.WindowsVirtualKey.K_2);
        private DoT rip = new DoT(1079, ConstController.WindowsVirtualKey.K_R);
        private DoT mangle = new DoT(33876, ConstController.WindowsVirtualKey.K_5);
        private DoT FF = new DoT(91565, ConstController.WindowsVirtualKey.K_4);
        private DoT pounce = new DoT(9005, ConstController.WindowsVirtualKey.K_1);
        private DoT shred = new DoT(58180, ConstController.WindowsVirtualKey.K_1);
        private Spell prowl = new Spell(5215);
        private Spell mark = new Spell(79061, ConstController.WindowsVirtualKey.K_B);
        private WalkBehindBot followBot;

        public DruidPVEDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Druid Feral(cat) PVE DPS bot");
        }
        public DruidPVEDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt, uint walkerTimerInterval) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Druid Feral(cat) PVE walker DPS bot");
            followBot = new WalkBehindBot(this.wow, this.wowinfo, walkerTimerInterval, WalkTargetType.CurrentTarget, 1);
        }

        public override void Rota() {
            base.Rota();
            if (this.wowinfo.FocusGUID != this.wowinfo.TargetGUID) {
                if (!Player.Unit.HasBuff(mark.ID) && Player.Unit.Shapeshift == ShapeshiftForm.Normal) {
                    mark.SendCast();
                }


                if (Player.Unit.Shapeshift == ShapeshiftForm.Cat) {
                    if (!Player.Unit.HasBuff(prowl.ID)) {
                        rake.ReCast(this.wowinfo, this.Target.Unit);
                        mangle.ReCast(this.wowinfo, this.Target.Unit);
                        FF.ReCast(this.wowinfo, this.Target.Unit);
                        if (this.wowinfo.ComboPoints == 5) {
                            rip.ReCast(this.wowinfo, this.Target.Unit);
                        }
                        // savage fasz meg a másik
                        if (this.Player.Unit.SecondaryPower > 60) {
                            shred.SendCast();
                        }
                    }
                    else {
                        pounce.SendCast();
                    }

                }
            }
        }

    }

}
