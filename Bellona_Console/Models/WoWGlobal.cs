using Bellona_Console.ConsoleInterface;
using Bellona_Console.MemoryReading;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    public class WoWGlobal {
        private UInt64 playerGUID;
        private UInt64 targetGUID;
        private UInt64 focusGUID;
        private UInt64 party1GUID;
        private UInt64 party2GUID;
        private UInt64 party3GUID;
        private UInt64 party4GUID;
        private UInt64 party5GUID;
        private byte comboPoints;
        private byte runes;
        private bool spellIsPending;
        #region properties
        public ulong PlayerGUID {
            get {
                return playerGUID;
            }

            set {
                playerGUID = value;
            }
        }

        public ulong TargetGUID {
            get {
                return targetGUID;
            }

            set {
                targetGUID = value;
            }
        }

        public ulong FocusGUID {
            get {
                return focusGUID;
            }

            set {
                focusGUID = value;
            }
        }

        public ulong Party1GUID {
            get {
                return party1GUID;
            }

            set {
                party1GUID = value;
            }
        }

        public ulong Party2GUID {
            get {
                return party2GUID;
            }

            set {
                party2GUID = value;
            }
        }

        public ulong Party3GUID {
            get {
                return party3GUID;
            }

            set {
                party3GUID = value;
            }
        }

        public ulong Party4GUID {
            get {
                return party4GUID;
            }

            set {
                party4GUID = value;
            }
        }

        public ulong Party5GUID {
            get {
                return party5GUID;
            }

            set {
                party5GUID = value;
            }
        }

        public byte ComboPoints {
            get {
                return comboPoints;
            }

            set {
                comboPoints = value;
            }
        }

        public bool SpellIsPending {
            get {
                return spellIsPending;
            }

            set {
                spellIsPending = value;
            }
        }
        #endregion
        public WoWGlobal(BlackMagic w) {
            this.Refresh(w);
        }
        public void Refresh(BlackMagic w) {
            try {
                this.PlayerGUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.PlayerGUID);
                this.TargetGUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.CurrentTargetGUID);
                this.FocusGUID = w.ReadUInt64((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.FocusTargetGUID);
                this.ComboPoints = w.ReadByte((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.ComboPoints);
                this.SpellIsPending = !(w.ReadByte((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.SpellIsPending) == 0);
                this.runes = w.ReadByte((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.Globals.Runes);
            }
            catch {
                Program.WowPrinter.Print(ConstStrings.ReadError);
            }
        }
        public bool HasRunesFor(DKSpellRuneCost dkspc) {
            foreach (Rune r in dkspc.Costs) {
                if (!HasRune(r)) {
                    return false;
                }
            }
            return true;
        }
        private bool HasRune(Rune r) {
            switch (r.Type) {
                case RuneType.Blood:
                    if (r.Cost <= GetBloodRunes()) {
                        return true;
                    }
                    else {
                        return false;
                    }
                case RuneType.Frost:
                    if (r.Cost <= GetFrostRunes()) {
                        return true;
                    }
                    else {
                        return false;
                    }
                case RuneType.Unholy:
                    if (r.Cost <= GetUnholyRunes()) {
                        return true;
                    }
                    else {
                        return false;
                    }
            }
            return false;
        }
        public int GetBloodRunes() {
            int temp = this.runes & 0x03;
            return Convert.ToString(temp, 2).ToCharArray().Count(c => c == '1');

        }
        public int GetFrostRunes() {
            int temp = this.runes & 0x30;
            return Convert.ToString(temp, 2).ToCharArray().Count(c => c == '1');
        }
        public int GetUnholyRunes() {
            int temp = this.runes & 0x0C;
            return Convert.ToString(temp, 2).ToCharArray().Count(c => c == '1');
        }
    }

}
