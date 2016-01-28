using Bellona_Console.ConsoleInterface;
using Bellona_Console.MemoryReading;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    [Flags]
    public enum PartyMembers : uint {
        Player = 0,
        PartyMember1 = 1,
        PartyMember2 = 2,
        PartyMember3 = 3,
        PartyMember4 = 4,
    }
    class WoWParty {
        private List<UInt64> party= new List<UInt64>();


        public WoWParty(BlackMagic w) {
            this.Refresh(w);
        }

        public List<ulong> Party {
            get {
                return party;
            }

            set {
                party = value;
            }
        }
        private void Refresh(BlackMagic w) {
            UInt64 PartyMember1GUID = 0;
            UInt64 PartyMember2GUID = 0;
            UInt64 PartyMember3GUID = 0;
            UInt64 PartyMember4GUID = 0;
            try {
                PartyMember1GUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyMember1GUID);
                PartyMember2GUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyMember2GUID);
                PartyMember3GUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyMember3GUID);
                PartyMember4GUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PartyMember4GUID);
            }
            catch {
                Program.WowPrinter.Print(ConstStrings.ReadError);
            }
            finally {
                if (PartyMember1GUID != 0) {
                    Party.Add(PartyMember1GUID);
                    if (PartyMember2GUID != 0) {
                        Party.Add(PartyMember2GUID);
                        if (PartyMember3GUID != 0) {
                            Party.Add(PartyMember3GUID);
                            if (PartyMember4GUID != 0) {
                                Party.Add(PartyMember4GUID);
                            }
                        }
                    }
                }
            }
        }
    }
}
