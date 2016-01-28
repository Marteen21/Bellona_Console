using Bellona_Console.Controller;
using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Bellona_Console.Other;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    [Flags]
    public enum WalkTargetType : uint {
        CurrentTarget = 0,
        CurrentFocus = 1,
        Other = 2,
    }
    public class WalkerBot : Bot {
        protected BlackMagic wow;
        protected WoWGlobal wowinfo;
        protected GameObject Player;
        protected GameObject WalkTarget;
        private bool forward = false;
        private bool left = false;
        private bool right = false;
        private WalkTargetType WhatToFollow;
        public static readonly float PositionThreshhold = 5;
        public static readonly double RotationThreshhold = 10 * Math.PI / 180;


        public WalkerBot(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt, WalkTargetType wtf) : base(tt) {
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            this.wowinfo.Refresh(wowProcess);
            this.WhatToFollow = wtf;
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            setWalkTarget(out WalkTarget);
        }
        public override void BotEvent(Object source, System.Timers.ElapsedEventArgs e) {
            this.ticks++;
            this.wowinfo.Refresh(wow);
            Player = new GameObject(wow, this.wowinfo.PlayerGUID);
            setWalkTarget(out WalkTarget);
            if (WalkTarget.GUID != 0) {
                if (Vector3.Distance(Player.Unit.Position, WalkTarget.Unit.Position) > PositionThreshhold) {
                    if (!forward || !Player.Unit.IsMoving) {
                        SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_UP, ref forward);
                    }
                    double mydiff = AngleDiff(Calculateangle(), Player.Unit.Rotation);
                    if ((Math.Abs(mydiff) < RotationThreshhold)) {
                        SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, ref left);
                        SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, ref right);
                    }
                    else if (mydiff<0) {
                        SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_LEFT, ref left);
                    }
                    else if (mydiff>0) {
                        SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_RIGHT, ref right);
                    }
                }
                else if (forward || left || right) {
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, ref forward);
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, ref left);
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, ref right);
                }
            }
            else {
                if (forward || left || right) {
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, ref forward);
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, ref left);
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, ref right);
                }
            }
        }
        private double Calculateangle() {
            float vx = WalkTarget.Unit.Position.X - Player.Unit.Position.X;
            float vy = WalkTarget.Unit.Position.Y - Player.Unit.Position.Y;
            return Math.Sign(vy) * Math.Acos((vx * 1 + vy * 0) / (Math.Sqrt(vx * vx + vy * vy)));
        }
        private static double AngleDiff(double rad2, double rad1) {
            double result = (rad1 - rad2);
            double temp2 = (rad1 - rad2 + 2 * Math.PI);
            if(Math.Abs(temp2)==Math.Min(Math.Abs(result), Math.Abs(temp2))){
                result = temp2;
            }
            double temp3 = (rad1 - rad2 - 2 * Math.PI);
            if (Math.Abs(temp3) == Math.Min(Math.Abs(result), Math.Abs(temp3))){
                result = temp3;
            }
            return result;
        }
        private static double Todegree(double rad) {
            return 180 * rad / Math.PI;
        }
        private void setWalkTarget(out GameObject wtarget) {
            switch (this.WhatToFollow) {
                case WalkTargetType.CurrentFocus:
                    wtarget = new GameObject(this.wow, (UInt64)this.wowinfo.FocusGUID);
                    return;
                case WalkTargetType.CurrentTarget:
                    wtarget = new GameObject(this.wow, (UInt64)this.wowinfo.TargetGUID);
                    return;
                case WalkTargetType.Other:
                    throw new NotImplementedException();
            }
            wtarget = new GameObject(this.wow,(UInt64)this.wowinfo.FocusGUID);
        }
    }


}

