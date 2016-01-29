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

namespace Bellona_Console.Bots {

    class HealPartyBot : Bot {

        protected BlackMagic wow;
        protected WoWGlobal wowinfo;
        protected GameObject Player;
        protected GameObject Target;
        protected GameObject Focus;
        protected WoWParty Party;
        public static readonly uint HealthForAoeHeal = 70;
        public static readonly uint HealthForSelfishHeal = 40;
        protected uint NumberofLowHPPartyMembers = 0;
        protected PartyMembers WhatToTarget;

        public HealPartyBot(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(tt) {
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            this.wowinfo.Refresh(wowProcess);
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            Target = new GameObject(wowProcess, this.wowinfo.TargetGUID);
            Focus = new GameObject(wowProcess, this.wowinfo.FocusGUID);
            Party = new WoWParty(wowProcess);
        }


        public override void BotEvent(Object source) {
            this.ticks++;
            this.wowinfo.Refresh(wow);
            Player = new GameObject(wow, this.wowinfo.PlayerGUID);
            Target = new GameObject(wow, this.wowinfo.TargetGUID);
            Focus = new GameObject(wow, this.wowinfo.FocusGUID);
            Party = new WoWParty(wow);
            Rota();

        }
        public virtual void Rota() {
            WhatToTarget = FindLowestPartyMember(out this.Target);
            switch (WhatToTarget) {
                case (PartyMembers.Player):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F1);
                    break;
                case (PartyMembers.PartyMember1):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F2);
                    break;
                case (PartyMembers.PartyMember2):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F3);
                    break;
                case (PartyMembers.PartyMember3):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F4);
                    break;
                case (PartyMembers.PartyMember4):
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F5);
                    break;
            }
        }
        private PartyMembers FindLowestPartyMember(out GameObject tempObject) {
            PartyMembers result = PartyMembers.Player;
            tempObject = new GameObject(wow, (UInt64)wowinfo.PlayerGUID);
            NumberofLowHPPartyMembers = 0;
            uint playerhp = tempObject.Unit.GetHealthPercent();
            if (playerhp < HealthForAoeHeal) {
                NumberofLowHPPartyMembers++;
            }
            if (playerhp < HealthForSelfishHeal) {
                return result;
            }
            else {
                for (int i = 0; i < this.Party.Party.Count; i++) {
                    GameObject temp2Object = new GameObject(wow, Party.Party[i]);
                    if (temp2Object.Unit.GetHealthPercent() < HealthForAoeHeal) {
                        NumberofLowHPPartyMembers++;
                    }
                    if (Vector3.Distance(Player.Unit.Position, temp2Object.Unit.Position) < 40 && GameObject.HPMin(ref tempObject, temp2Object)) {
                        result = (PartyMembers)i + 1;
                    }
                }
            }
            return result;
        }
    }
}
