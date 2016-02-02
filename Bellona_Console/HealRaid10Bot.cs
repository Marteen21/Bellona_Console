using Bellona_Console.Bots;
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

namespace Bellona_Console {
    class HealRaid10Bot : Bot {

        protected BlackMagic wow;
        protected WoWGlobal wowinfo;
        protected GameObject Player;
        protected GameObject Target;
        protected GameObject Focus;
        protected WoWRaid Raid;
        public static readonly uint HealthForAoeHeal = 70;
        public static readonly uint HealthForSelfishHeal = 40;
        protected uint NumberofLowHPRaidMembers = 0;
        protected RaidMembers WhatToTarget;

        public HealRaid10Bot(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(tt) {
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            this.wowinfo.Refresh(wowProcess);
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            Target = new GameObject(wowProcess, this.wowinfo.TargetGUID);
            Focus = new GameObject(wowProcess, this.wowinfo.FocusGUID);
            Raid = new WoWRaid(wowProcess);
        }


        public override void BotEvent(Object source) {
            this.ticks++;
            this.wowinfo.Refresh(wow);
            Player = new GameObject(wow, this.wowinfo.PlayerGUID);
            Target = new GameObject(wow, this.wowinfo.TargetGUID);
            Focus = new GameObject(wow, this.wowinfo.FocusGUID);
            Raid = new WoWRaid(wow);
            Rota();

        }
        public virtual void Rota() {
            WhatToTarget = FindLowestRaidMember(out this.Target);
            switch (WhatToTarget) {
                case (RaidMembers.RaidMember1):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F2);
                    break;
                case (RaidMembers.RaidMember2):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F3);
                    break;
                case (RaidMembers.RaidMember3):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F4);
                    break;
                case (RaidMembers.RaidMember4):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F5);
                    break;
                case (RaidMembers.RaidMember5):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F6);
                    break;
                case (RaidMembers.RaidMember6):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F7);
                    break;
                case (RaidMembers.RaidMember7):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F8);
                    break;
                case (RaidMembers.RaidMember8):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F9);
                    break;
                case (RaidMembers.RaidMember9):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F10);
                    break;
                case (RaidMembers.RaidMember10):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F11);
                    break;
            }
        }
        private RaidMembers FindLowestRaidMember(out GameObject tempObject) {
            RaidMembers result = RaidMembers.RaidMember1;
            tempObject = new GameObject(wow, (UInt64)wowinfo.PlayerGUID);
            NumberofLowHPRaidMembers = 0;
            uint playerhp = tempObject.Unit.GetHealthPercent();
            if (playerhp < HealthForAoeHeal && playerhp != 0) {
                NumberofLowHPRaidMembers++;
            }
            else {
                for (int i = 0; i < this.Raid.RaidMembers.Count; i++) {
                    GameObject temp2Object = new GameObject(wow, Raid.RaidMembers[i]);
                    if (temp2Object.Unit.GetHealthPercent() < HealthForAoeHeal && temp2Object.Unit.Health != 0) {
                        NumberofLowHPRaidMembers++;
                    }
                    if (Vector3.Distance(Player.Unit.Position, temp2Object.Unit.Position) < 40 && GameObject.HPMin(ref tempObject, temp2Object) && temp2Object.Unit.Health != 0) {
                        result = (RaidMembers)i + 1;
                    }
                }
            }
            return result;
        }
    }
}
