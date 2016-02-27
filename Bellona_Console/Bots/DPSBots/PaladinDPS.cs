using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;

namespace Bellona_Console.Bots.DPSBots {
    class PaladinDPS : DPSBot {

        private Spell plea = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD0);
        private Spell exorcism = new Spell(59578, ConstController.WindowsVirtualKey.VK_NUMPAD4); //art of war
        private Spell inquisition = new Spell(84963, ConstController.WindowsVirtualKey.VK_NUMPAD1);
        private Spell judgement = new Spell(89906, ConstController.WindowsVirtualKey.VK_NUMPAD2);
        private Spell templar = new Spell(90174, ConstController.WindowsVirtualKey.VK_NUMPAD3);
        private Spell crusader = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        private Spell hammer = new Spell(31884, ConstController.WindowsVirtualKey.VK_NUMPAD6);
        private Spell repetance = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD7);
        private Spell worldofglory = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD8);
        private Spell divineporpuse = new Spell(90174);
        private Spell stun = new Spell(0, ConstController.WindowsVirtualKey.K_5);
        private Spell bubi = new Spell(0, ConstController.WindowsVirtualKey.K_F);
        private Spell might = new Spell(79102, ConstController.WindowsVirtualKey.VK_PRIOR);
        private Spell truth = new Spell(31801, ConstController.WindowsVirtualKey.VK_NUMPAD9);


        public PaladinDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Paladin Retribution DPS bot");
        }

        public override void Rota() {
            if (!this.Player.Unit.HasBuff(might.ID)) {
                might.SendCast();
            }
            if (!this.Player.Unit.HasBuff(truth.ID)) {
                truth.SendCast();
            }
            repetance.SendCast();
            if (this.Player.Unit.GetManaPercent() < 50 && this.Player.Unit.GetHealthPercent() > 70) {
                plea.SendCast();
            }
            if (this.Player.Unit.HasBuff(exorcism.ID)) {
                exorcism.SendCast();
            }

            if (!this.Player.Unit.HasBuff(inquisition.ID) && (this.Player.Unit.HolyPower == 3 || this.Player.Unit.HasBuff(templar.ID))) {
                inquisition.SendCast();
            }
            judgement.SendCast();
            crusader.SendCast();

            if (this.Player.Unit.HolyPower == 3 || this.Player.Unit.HasBuff(templar.ID)) {
                if (this.Player.Unit.GetHealthPercent() < 60) {
                    worldofglory.SendCast();
                }
                else {
                    templar.SendCast();
                }

            }
            if (this.Target.Unit.GetHealthPercent() > 20 || this.Player.Unit.HasBuff(hammer.ID)) {
                hammer.SendCast();
            }
            if (this.Target.Unit.GetHealthPercent()<50 && this.Player.Unit.HasBuff(inquisition.ID) && this.Player.Unit.HolyPower <= 1) {
                stun.SendCast();
            } 
            if (this.Player.Unit.GetHealthPercent() < 20) {
                bubi.SendCast();
            }
        }
    }
}
