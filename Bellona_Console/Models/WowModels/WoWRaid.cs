using Bellona_Console.MemoryReading;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
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
