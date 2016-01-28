using Bellona_Console.ConsoleInterface;
using Bellona_Console.MemoryReading;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    class WoWParty {
        private UInt64 partyLeaderGUID;
        private UInt64 partyMember1GUID;
        private UInt64 partyMember2GUID;
        private UInt64 partyMember3GUID;
        private UInt64 partyMember4GUID;
       
        public WoWParty(BlackMagic w) {
            this.Refresh(w);
        }
        #region properties
        public ulong PartyLeaderGUID {
            get {
                return partyLeaderGUID;
            }

            set {
                partyLeaderGUID = value;
            }
        }

        public ulong PartyMember1GUID {
            get {
                return partyMember1GUID;
            }

            set {
                partyMember1GUID = value;
            }
        }

        public ulong PartyMember2GUID {
            get {
                return partyMember2GUID;
            }

            set {
                partyMember2GUID = value;
            }
        }

        public ulong PartyMember3GUID {
            get {
                return partyMember3GUID;
            }

            set {
                partyMember3GUID = value;
            }
        }

        public ulong PartyMember4GUID {
            get {
                return partyMember4GUID;
            }

            set {
                partyMember4GUID = value;
            }
        }
#endregion
        private void Refresh(BlackMagic w) {
            try {
                this.PartyLeaderGUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyLeaderGUID);
                this.PartyMember1GUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyMember1GUID);
                this.PartyMember2GUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyMember2GUID);
                this.PartyMember3GUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyMember3GUID);
                this.PartyMember4GUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyMember4GUID);
            }
            catch {
                Program.WowPrinter.Print(ConstStrings.ReadError);
            }
        }
    }
}
