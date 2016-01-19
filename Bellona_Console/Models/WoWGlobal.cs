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
