using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Models {
    [Flags]
    public enum RuneType {
        Blood = 0,
        Unholy = 1,
        Frost = 2,
    }
    public class DKSpellRuneCost {
        private List<Rune> costs;

        internal List<Rune> Costs {
            get {
                return costs;
            }

            set {
                costs = value;
            }
        }

        public DKSpellRuneCost(Rune r1) {
            this.Costs = new List<Rune>();
            this.Costs.Add(r1);
        }
        public DKSpellRuneCost(Rune r1, Rune r2) {
            this.Costs = new List<Rune>();
            this.Costs.Add(r1);
            this.Costs.Add(r2);
        }
    }
    public class Rune {
        private RuneType type;
        private uint cost;

        public RuneType Type {
            get {
                return type;
            }

            set {
                type = value;
            }
        }

        public uint Cost {
            get {
                return cost;
            }

            set {
                cost = value;
            }
        }

        public Rune(RuneType t, uint c) {
            this.Type = t;
            this.Cost = c;
        }
    }
}
