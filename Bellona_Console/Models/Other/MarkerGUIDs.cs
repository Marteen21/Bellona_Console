using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models.Other {
    [Flags]
    public enum MarkerType {
        none = 0,
        star = 1,
        circle = 2,
        diamond = 3,
        triagle = 4,
        moon = 5,
        square = 6,
        cross = 7,
        skull = 8,
    } 
    public class MarkerGUIDs {
        private ulong starGUID = 0;
        private ulong circleGUID = 0;
        private ulong diamondGUID = 0;
        private ulong triagleGUID = 0;
        private ulong moonGUID = 0;
        private ulong squareGUID = 0;
        private ulong crossGUID = 0;
        private ulong skullGUID = 0;

        public ulong StarGUID {
            get {
                return starGUID;
            }

        }

        public ulong CircleGUID {
            get {
                return circleGUID;
            }
        }

        public ulong DiamondGUID {
            get {
                return diamondGUID;
            }
        }

        public ulong TriagleGUID {
            get {
                return triagleGUID;
            }

        }

        public ulong MoonGUID {
            get {
                return moonGUID;
            }

        }

        public ulong SquareGUID {
            get {
                return squareGUID;
            }

        }

        public ulong CrossGUID {
            get {
                return crossGUID;
            }

        }

        public ulong SkullGUID {
            get {
                return skullGUID;
            }

        }

        public MarkerGUIDs(byte[] markerarray) {
            if (markerarray.Length != 64) {
                throw new ArgumentOutOfRangeException();
            }
            starGUID = BitConverter.ToUInt64(markerarray, 0);
            circleGUID = BitConverter.ToUInt64(markerarray, 8);
            diamondGUID = BitConverter.ToUInt64(markerarray, 16);
            triagleGUID = BitConverter.ToUInt64(markerarray, 24);
            moonGUID = BitConverter.ToUInt64(markerarray, 32);
            squareGUID = BitConverter.ToUInt64(markerarray, 40);
            crossGUID = BitConverter.ToUInt64(markerarray, 48);
            skullGUID = BitConverter.ToUInt64(markerarray, 56);
        }
        public ulong getGUIDByMarkerType(MarkerType mt) {
            switch (mt) {
                case MarkerType.star:
                    return this.StarGUID;
                case MarkerType.circle:
                    return this.CircleGUID;
                case MarkerType.diamond:
                    return this.DiamondGUID;
                case MarkerType.triagle:
                    return this.TriagleGUID;
                case MarkerType.moon:
                    return this.MoonGUID;
                case MarkerType.square:
                    return this.SquareGUID;
                case MarkerType.cross:
                    return this.CrossGUID;
                case MarkerType.skull:
                    return this.SkullGUID;
                default:
                    return 0;
            }
        }

    }
}
