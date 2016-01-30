using Bellona_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Other {
    class Angles {
        public static double Calculateangle(WoWUnit Target, WoWUnit Player) {
            float vx = Target.Position.X - Player.Position.X;
            float vy = Target.Position.Y - Player.Position.Y;
            return Math.Sign(vy) * Math.Acos((vx * 1 + vy * 0) / (Math.Sqrt(vx * vx + vy * vy)));
        }
        public static double AngleDiff(double rad2, double rad1) {
            double result = (rad1 - rad2);
            double temp2 = (rad1 - rad2 + 2 * Math.PI);
            if (Math.Abs(temp2) == Math.Min(Math.Abs(result), Math.Abs(temp2))) {
                result = temp2;
            }
            double temp3 = (rad1 - rad2 - 2 * Math.PI);
            if (Math.Abs(temp3) == Math.Min(Math.Abs(result), Math.Abs(temp3))) {
                result = temp3;
            }
            return result;
        }
        public static double Todegree(double rad) {
            return 180 * rad / Math.PI;
        }
    }
}
