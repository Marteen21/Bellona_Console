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
    public enum WoWClass : uint {
        None = 0,
        Warrior = 1,
        Paladin = 2,
        Hunter = 3,
        Rogue = 4,
        Priest = 5,
        DeathKnight = 6,
        Shaman = 7,
        Mage = 8,
        Warlock = 9,
        Druid = 11,
    }
    [Flags]
    public enum ShapeshiftForm {
        Normal = 0,
        Cat = 1,
        TreeOfLife = 2,
        Travel = 3,
        Aqua = 4,
        Bear = 5,
        Ambient = 6,
        Ghoul = 7,
        DireBear = 8,
        CreatureBear = 14,
        CreatureCat = 15,
        GhostWolf = 16,
        BattleStance = 17,
        DefensiveStance = 18,
        BerserkerStance = 19,
        EpicFlightForm = 27,
        Shadow = 28,
        Stealth = 30,
        Moonkin = 31,
        SpiritOfRedemption = 32
    }
    [Flags]
    public enum Role {
        Unknown = 0,
        DPS = 1,
        Healer = 2,
    }
    [Flags]
    public enum BuffStorage {
        Unkown = 0,
        SmallArray = 1,
        BigArray =2,
    }

    class WoWUnit {
        private WoWClass wowClass;
        private ShapeshiftForm shapeshift;
        private Role role;
        private uint level;
        private uint health;
        private uint maxHealth;
        private uint power;
        private uint maxPower;
        private uint secondaryPower;
        private bool isMoving = false;
        private List<uint> buffs = new List<uint>();
        private BuffStorage addressofTheBuffs = BuffStorage.Unkown;
        #region properties
        public WoWClass WowClass {
            get {
                return wowClass;
            }

            set {
                wowClass = value;
            }
        }

        public ShapeshiftForm Shapeshift {
            get {
                return shapeshift;
            }

            set {
                shapeshift = value;
            }
        }

        public Role Role {
            get {
                return role;
            }

            set {
                role = value;
            }
        }

        public uint Level {
            get {
                return level;
            }

            set {
                level = value;
            }
        }

        public uint Health {
            get {
                return health;
            }

            set {
                health = value;
            }
        }

        public uint MaxHealth {
            get {
                return maxHealth;
            }

            set {
                maxHealth = value;
            }
        }

        public uint Power {
            get {
                return power;
            }

            set {
                power = value;
            }
        }

        public uint MaxPower {
            get {
                return maxPower;
            }

            set {
                maxPower = value;
            }
        }

        public uint SecondaryPower {
            get {
                return secondaryPower;
            }

            set {
                secondaryPower = value;
            }
        }
        public bool IsMoving {
            get {
                return isMoving;
            }

            set {
                isMoving = value;
            }
        }
        public BuffStorage AddressofTheBuffs {
            get {
                return addressofTheBuffs;
            }

            set {
                addressofTheBuffs = value;
            }
        }

        public List<uint> Buffs {
            get {
                return buffs;
            }

            set {
                buffs = value;
            }
        }
        #endregion
        public WoWUnit() {

        }
        public WoWUnit(BlackMagic w, GameObject go) {
            this.Refresh(w,go);
        }
        public void Refresh(BlackMagic w, GameObject go) {
            try {
                this.WowClass = (WoWClass)w.ReadByte((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.Class8);
                this.Shapeshift = (ShapeshiftForm)w.ReadByte((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.ShapeShift);
                this.Role = Role.Unknown;
                this.Level = w.ReadUInt((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.Level);
                this.Health = w.ReadUInt((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.Health);
                this.MaxHealth = w.ReadUInt((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.MaxHealth);
                this.Power = w.ReadUInt((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.Power);
                this.MaxPower = w.ReadUInt((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.MaxPower);
                this.SecondaryPower = w.ReadUInt(((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.SecondaryPower));
                this.IsMoving = w.ReadByte((uint)go.MovementArrayAddress+(uint)ConstOffsets.Movements.IsMoving8) != 0x00;
                this.RefreshBuffs(w,go);
            }
            catch {
                Program.WowPrinter.Print(ConstStrings.ReadError);
                throw new Exception();
            }
        }
        private void RefreshBuffs(BlackMagic w, GameObject go) {
            this.Buffs.Clear();
            if (this.AddressofTheBuffs == BuffStorage.Unkown) {
                if ((uint)go.BuffBigArrayAddress >= (uint)w.MainModule.BaseAddress) {
                    this.AddressofTheBuffs = BuffStorage.BigArray;
                }
                else {
                    this.AddressofTheBuffs = BuffStorage.SmallArray;
                }
            }
            while (!FillBuffsList(w, go)) {
            }
            
            
        }
        private bool FillBuffsList(BlackMagic w, GameObject go) {
            uint addr = 0;
            uint i = 0;
            uint temp = 1;
            switch (this.AddressofTheBuffs) {
                case BuffStorage.Unkown:
                    throw new NullReferenceException();
                case BuffStorage.SmallArray:
                    addr = (uint)go.BuffSmallArrayAddress;
                    break;
                case BuffStorage.BigArray:
                    addr = (uint)go.BuffBigArrayAddress;
                    break;
            }
            try {
                while (temp != 0) {
                    temp = w.ReadUInt(addr + (0x08 * i));
                    i++;
                    if (temp != 0) {
                        this.Buffs.Add(temp);
                    }
                }
                return true;
            }
            catch {
                Program.WowPrinter.Print(ConstStrings.BuffError);
                this.AddressofTheBuffs = BuffStorage.Unkown;
                return false;
            }
        }

    }
}
