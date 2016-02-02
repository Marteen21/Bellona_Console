using Bellona_Console.MemoryReading;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    [Flags]
    public enum RaidMembers : uint {
        RaidMember1 = 1,
        RaidMember2 = 2,
        RaidMember3 = 3,
        RaidMember4 = 4,
        RaidMember5 = 5,
        RaidMember6 = 6,
        RaidMember7 = 7,
        RaidMember8 = 8,
        RaidMember9 = 9,
        RaidMember10 = 10,
    }
    class WoWRaid {
        private uint raidmembercount;
        private List<UInt64> raidMembers= new List<UInt64>();
        public WoWRaid(BlackMagic w) {
            this.Refresh(w);
        }

        public uint Raidmembercount {
            get {
                return raidmembercount;
            }

            set {
                raidmembercount = value;
            }
        }

        public List<UInt64> RaidMembers {
            get {
                return raidMembers;
            }

            set {
                raidMembers = value;
            }
        }

        private void Refresh(BlackMagic w) {
            Raidmembercount = w.ReadUInt((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.RaidMembers.TotalNumber);
            RaidMembers.Clear();
            for (uint i = 0; i < Raidmembercount; i++) {
                uint tempaddr=w.ReadUInt((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.RaidMembers.FirstRaidMemberAddress + i*(uint)ConstOffsets.RaidMembers.NextRaidMemberAddres);
                RaidMembers.Add(w.ReadUInt64(tempaddr));
            }
        }
    }
}
