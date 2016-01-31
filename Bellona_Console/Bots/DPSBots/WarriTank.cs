using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;

namespace Bellona_Console.Bots.DPSBots {
    class WarriTank : DPSBot {

        Spell shildslam = new Spell(0, ConstController.WindowsVirtualKey.K_1);
        Spell revenge = new Spell(0, ConstController.WindowsVirtualKey.K_2);
        DoT rend = new DoT(94009, ConstController.WindowsVirtualKey.K_3);
        Spell defensivestance = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD0);
        Spell commandingshout = new Spell(469, ConstController.WindowsVirtualKey.VK_NUMPAD1);
        Spell devastate = new Spell(0, ConstController.WindowsVirtualKey.K_4);
        Spell demoralizingshout = new Spell(1160, ConstController.WindowsVirtualKey.VK_NUMPAD9);
        Spell tunderclap = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD3);
        Spell shockwave = new Spell(87096, ConstController.WindowsVirtualKey.K_C);
        Spell shieldblock = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD4);
        Spell laststandmacro = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        Spell shieldwall = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD6);
        Spell berserkerage = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD7);
        Spell cleave = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD2);
        Spell retaliation = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD8);


        public WarriTank(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Warrior Tank bot");
        }

        public override void Rota() {
            if (this.Player.Unit.Shapeshift != ShapeshiftForm.DefensiveStance) {
                defensivestance.SendCast();
            }
            if (this.Player.Unit.IsInCombat) {
                berserkerage.SendCast();
                retaliation.SendCast();
            }
            if (this.Player.Unit.GetHealthPercent() < 30) {
                laststandmacro.SendCast();
            }
            if (this.Player.Unit.GetHealthPercent() < 50) {
                shieldwall.SendCast();
            }
            if (this.Player.Unit.GetHealthPercent() < 80) {
                shieldblock.SendCast();
            }
            

            if (!this.Player.Unit.HasBuff(commandingshout.ID)) {
                commandingshout.SendCast();
            }
            rend.ReCast(this.wowinfo, this.Target.Unit);
            revenge.SendCast();
            shildslam.SendCast();
            tunderclap.SendCast();
            if (this.Player.Unit.HasBuff(shockwave.ID)) {
                shockwave.SendCast();
            }
            if (!this.Target.Unit.HasBuff(demoralizingshout.ID)) {
                demoralizingshout.SendCast();
            }
            devastate.SendCast();


        }

    }
}
