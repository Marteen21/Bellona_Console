using Bellona_Console.MemoryReading;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Other {
    class Tester {
        private List<GameObject> gameobjects = new List<GameObject>();
        public Tester(BlackMagic w, GameObject Player, float threshhold) {
            GameObject TempObject = new GameObject(Initializer.FirstObject);
            int i = 0;
            while ((uint)TempObject.BaseAddress != 0) {
                i++;
                Console.WriteLine(TempObject.Unit.Position.X + " " + TempObject.Unit.Position.X + " " + TempObject.Unit.Position.Z);
                Console.WriteLine(Player.Unit.Position.X + " " + Player.Unit.Position.X + " " + Player.Unit.Position.Z);
                if (Vector3.Distance(TempObject.Unit.Position, Player.Unit.Position) < threshhold && TempObject.GUID != Player.GUID) {
                    gameobjects.Add(TempObject);
                    Console.WriteLine("Found one");
                }
                TempObject = new GameObject(w, (UIntPtr)w.ReadUInt(((uint)TempObject.BaseAddress + (uint)ConstOffsets.ObjectManager.NextObject)));
            }
        }
    }
}
