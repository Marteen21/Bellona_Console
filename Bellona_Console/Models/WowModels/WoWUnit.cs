using Bellona_Console.ConsoleInterface;
using Bellona_Console.MemoryReading;
using Bellona_Console.Other;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    #region Flags
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
        BigArray = 2,
    }
    #endregion
    public class WoWUnit {
        private WoWClass wowClass;
        private ShapeshiftForm shapeshift;
        private Role role;
        private uint level;
        private uint health;
        private uint maxHealth;
        private uint power;
        private uint maxPower;
        private uint holyPower;
        private uint secondaryPower;
        private uint faction;
        private UInt64 targetGUID;
        private MovementFlags movingInfo; 
        private bool isInCombat = false;
        private Vector3 position = new Vector3();
        private double rotation;
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
        public uint HolyPower {
            get {
                return holyPower;
            }

            set {
                holyPower = value;
            }
        }

        public uint Faction {
            get {
                return faction;
            }

            set {
                faction = value;
            }
        }
        internal Vector3 Position {
            get {
                return position;
            }

            set {
                position = value;
            }
        }

        public double Rotation {
            get {
                return rotation;
            }

            set {
                rotation = value;
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

        public ulong TargetGUID {
            get {
                return targetGUID;
            }

            set {
                targetGUID = value;
            }
        }

        public bool IsInCombat {
            get {
                return isInCombat;
            }

            set {
                isInCombat = value;
            }
        }

        internal MovementFlags MovingInfo {
            get {
                return movingInfo;
            }

            set {
                movingInfo = value;
            }
        }


        #endregion
        public WoWUnit() {
            this.MovingInfo = new MovementFlags(0);
        }
        public WoWUnit(BlackMagic w, GameObject go) {
            this.Refresh(w, go);
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
                this.SecondaryPower = w.ReadUInt((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.SecondaryPower);
                this.MovingInfo = new MovementFlags(w.ReadByte((uint)go.MovementArrayAddress + (uint)ConstOffsets.Movements.IsMoving8));
                this.HolyPower = w.ReadUInt((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.HolyPower);
                this.Faction = w.ReadUInt((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.Faction);
                this.TargetGUID = w.ReadUInt64((uint)go.DescriptorArrayAddress + (uint)ConstOffsets.Descriptors.TargetGUID);
                //byte temp = w.ReadByte((uint)go.BuffSmallArrayAddress + (uint)ConstOffsets.Descriptors.IsinCombat);
                this.IsInCombat = (w.ReadByte((w.ReadUInt((uint)go.BaseAddress + (uint)ConstOffsets.Movements.IsinCombatOffset1)) + (uint)ConstOffsets.Movements.IsinCombatOffset2+2)& 0x8)!=0;
                this.position.X = w.ReadFloat((uint)go.BaseAddress + (uint)ConstOffsets.Positions.X);
                this.position.Y = w.ReadFloat((uint)go.BaseAddress + (uint)ConstOffsets.Positions.Y);
                this.position.Z = w.ReadFloat((uint)go.BaseAddress + (uint)ConstOffsets.Positions.Z);
                float temprot = w.ReadFloat((uint)go.BaseAddress + (uint)ConstOffsets.Positions.Rotation);
                if (temprot > Math.PI) {
                    this.Rotation = -(2 * Math.PI - temprot);
                }
                else {
                    this.Rotation = temprot;
                }
                this.RefreshBuffs(w, go);
            }
            catch {
                Program.WowPrinter.Print(ConstStrings.ReadError);
            }

            //this.Position = new Vector3(x,y,z);
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
            while (true) {
                if (FillBuffsList(w, go)) {
                    break;
                }
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
                while (temp != 0 && temp < 121820) {
                    temp = w.ReadUInt(addr + (0x08 * i));
                    i++;
                    if (temp != 0 && temp < 121820) {
                        this.Buffs.Add(temp);
                    }
                }
                return true;
            }
            catch {
                Program.WowPrinter.Print(ConstStrings.BuffError);
                switch (this.AddressofTheBuffs) {
                    case BuffStorage.Unkown:
                        throw new NullReferenceException();
                    case BuffStorage.SmallArray:
                        this.AddressofTheBuffs = BuffStorage.Unkown;
                        break;
                    case BuffStorage.BigArray:
                        this.AddressofTheBuffs = BuffStorage.SmallArray;
                        break;
                }
                return false;
            }
        }
        public bool HasBuff(uint buffid) {
            return this.Buffs.Contains(buffid);
        }
        public bool HasBuffs(List<uint> buffidlist) {
            foreach (uint buffid in buffidlist) {
                if (!this.Buffs.Contains(buffid)) {
                    return false;
                }
            }
            return true;
        }
        public uint GetHealthPercent() {
            if(Health ==0 || maxHealth == 0) {
                return 0;
            }
            return ((100 * Health) / maxHealth);
        }
        public uint GetManaPercent() {
            if (Power == 0 || maxPower == 0) {
                return 0;
            }
            return ((100 * Power) / MaxPower);
        }

    }
}
