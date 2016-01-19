using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.MemoryReading {
    class ConstOffsets {
        internal enum ObjectManager : uint {
            CurMgrPointer = 0x9BE7E0,
            CurMgrOffset = 0x463C,
            NextObject = 0x3C,
            FirstObject = 0xC0,
            LocalGUID = 0x30,
            LocalDescriptorArray = 0xC,
            LocalBuffBigArray = 0xE9C,
            LocalBuffSmallArray = 0xE98,
            LocalMovementArray = 0x100,
        }
        internal enum Movements : uint {
            IsMoving8 = 0x38,
        }
        internal enum Globals : uint {
            SpellCooldDownPtr = 0xACD714,
            PlayerName = 0x9BE820,
            CurrentRealm = 0x9BE9AE,
            #region GUID-s
            PlayerGUID = 0x9BE818,
            CurrentTargetGUID = 0xAD7448,
            LastTargetGUID = 0xAD7450,
            FocusTargetGUID = 0xAD7468,
            MouseOverGUID = 0xAD7438,
            PetGUID = 0xB43B60,
            FollowGUID = 0x9D61D8,
            Arena1GUID = 0xB36140,
            Arena2GUID = Arena1GUID + 0x8,
            Arena3GUID = Arena2GUID + 0x8,
            Arena4GUID = Arena3GUID + 0x8,
            Arena5GUID = Arena4GUID + 0x8,
            PartyLeaderGUID = 0xB33370,
            PartyMember1GUID = 0xB33350,
            PartyMember2GUID = PartyMember1GUID + 0x8,
            PartyMember3GUID = PartyMember2GUID + 0x8,
            PartyMember4GUID = PartyMember3GUID + 0x8,
            #endregion
            #region ClientInfo
            LootWindow = 0xB45230,
            Timestamp = 0x9C0C7C,
            BuildNumber = 0xAB4214,
            GetMinimapZoneText = 0xAD7414,
            GetZoneText = 0xAD741C,
            GetSubZoneText = 0xAD7418,
            GetZoneID = 0xAD74B0,
            IsInGame = 0xAD7426,
            ContinentID = 0x8A2710,
            LastErrorMessage = 0xAD6828,
            IsLoadingOrConnecting = 0xABB9AC,
            GetCurrencyInfo = 0x914F48,
            GetHomeBindAreaId = 0x9D4D7C,
            #endregion
            ComboPoints = 0xAD74F1,
            Runes = 0xB36060,
            PetSpellBookNumSpells = 0xB33CA4,
            PetSpellBookNumSpellsPtr = 0xB33CA8,
            SpellIsTargetting = 0xACD654,
            SpellIsPending = 0xACD770,
            ScriptGetLocale = 0x9732FC,
            #region AddonInfo
            GetNumInstalledAddons = 0x93A74C,
            BaseAddons = 0x93A750,
            #endregion
            #region GuildInfo
            TotalGuildMembers = 0xB35ECC,
            GuildRosterInfoBase = 0xB35F64,
            #endregion
            #region Other
            CursorType = 0x93D250,
            MirrorTimer = 0xAD78D0,
            #endregion
        }
        internal enum Descriptors : uint {
            Class8 = 0x55,
            Level = 0xB0,
            Health = 0x58,
            MaxHealth = 0x70,
            Power = 0x5C,
            HolyPower = 0x60,
            SecondaryPower = 0x64,
            MaxPower = 0x74,
            TargetGUID = 0x40,
            ShapeShift = 0x1C7
        }

    }
}
