using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;

namespace Bellona_Console.Bots {
    class DruidDPS : DPSBot {

        private static DoT rake = new DoT(1822, ConstController.WindowsVirtualKey.K_2);
        private static DoT rip = new DoT(1079, ConstController.WindowsVirtualKey.K_R);
        private static DoT mangle = new DoT(33876, ConstController.WindowsVirtualKey.K_5);
        private static DoT FF = new DoT(91565, ConstController.WindowsVirtualKey.K_4);
        private static DoT pounce = new DoT(9005, ConstController.WindowsVirtualKey.K_1);
        private static DoT shred = new DoT(58180, ConstController.WindowsVirtualKey.K_1);
        private static Spell prowl = new Spell(5215);
        private static Spell mark = new Spell(79061, ConstController.WindowsVirtualKey.K_B);

        public DruidDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
        }
        public override void Rota() {

            if (!Player.Unit.HasBuff(mark.ID) && Player.Unit.Shapeshift == ShapeshiftForm.Normal) {
                DruidDPS.mark.SendCast();
            }

            if (Player.Unit.Shapeshift == ShapeshiftForm.Cat) {
                if (!Player.Unit.HasBuff(prowl.ID)) {
                    DruidDPS.rake.ReCast(this.wowinfo, this.Target.Unit);
                    DruidDPS.mangle.ReCast(this.wowinfo, this.Target.Unit);
                    DruidDPS.FF.ReCast(this.wowinfo, this.Target.Unit);
                    if (this.wowinfo.ComboPoints == 5) {
                        DruidDPS.rip.ReCast(this.wowinfo, this.Target.Unit);
                    }
                    if (this.Player.Unit.SecondaryPower > 60) {
                        DruidDPS.shred.SendCast();
                    }
                }
                else {
                    DruidDPS.pounce.SendCast();
                }

            }

        }

    }

}
