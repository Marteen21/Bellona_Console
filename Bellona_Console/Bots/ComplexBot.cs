using Bellona_Console.Controller;
using Bellona_Console.Models;
using Bellona_Console.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Bellona_Console.MemoryReading;

namespace Bellona_Console.Bots {
    [Flags]
    public enum ComplexBotStance : uint {
        FollowFocus = 0,
        AoEAtFocus = 1,
        DpsTargetRanged = 2,
        DpsTargetMelee = 3,
        DpsTargetBackMelee = 4,
        FollowTarget = 5,
        FollowTargetBack = 6,
        Idle = 7,
        Halt = 8,
    }
    class ComplexBot {
        private Timer rotaTimer;
        private Timer movementTimer;
        protected bool needsToMove = false;
        protected ComplexBotStance stance;
        private ComplexBotStance playerDPStype;

        public static readonly double RangedDPSRange = 28;
        public static readonly double MeleeDPSRange = 4;
        public static readonly double FollowFocusStartRange = 10;
        public static readonly double FollowFocusStopRange = 3;

        public static readonly double RangedDPSAngle = Math.PI * 0.4;
        public static readonly double MeleeDPSAngle = Math.PI * 0.06;
        public static readonly double FollowFocusAngle = Math.PI * 0.06;

        private bool myforward;
        private bool myleft;
        private bool myright;

        protected GameObject target;
        protected GameObject player;
        protected GameObject focus;



        protected long rotaTicks = 0;


        public ComplexBot(uint rotaInterval, uint movementInterval, ComplexBotStance mplayerDPStype) {
            SetupTimer(out rotaTimer, RotaEvent, rotaInterval, rotaInterval / 2);
            SetupTimer(out movementTimer, MovementEvent, movementInterval, movementInterval);
            this.playerDPStype = mplayerDPStype;
        }

        protected virtual void MovementEvent(Object source) {
            Program.ClientInfo.Refresh(Program.Wow);
            target = new GameObject(Program.Wow, Program.ClientInfo.TargetGUID);
            player = new GameObject(Program.Wow, Program.ClientInfo.PlayerGUID);
            focus = new GameObject(Program.Wow, Program.ClientInfo.FocusGUID);
        }

        protected virtual void RotaEvent(Object source) {
            throw new NotImplementedException();
        }
        private void SetupTimer(out Timer mTimer, TimerCallback tcb, uint period) {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            mTimer = new Timer(tcb, autoEvent, 1000, period);
        }
        private void SetupTimer(out Timer mTimer, TimerCallback tcb, uint period, uint dueTime) {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            mTimer = new Timer(tcb, autoEvent, dueTime, period);
        }

        protected bool RotateTowards(WoWUnit PlayerUnit, WoWUnit TargetUnit, double RotationThreshhold, bool DisableForward) {
            double mydiff = Angles.AngleDiff(Angles.Calculateangle(TargetUnit, PlayerUnit), PlayerUnit.Rotation);
            if ((Math.Abs(mydiff) < RotationThreshhold)) {
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, PlayerUnit.MovingInfo.IsTurningLeft, ref myleft);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, PlayerUnit.MovingInfo.IsTurningRight, ref myright);
                return true;
            }
            else if (mydiff < 0) {
                if (DisableForward) {
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, PlayerUnit.MovingInfo.IsMovingForward, ref myforward);
                }
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_LEFT, !PlayerUnit.MovingInfo.IsTurningLeft, ref myleft);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, PlayerUnit.MovingInfo.IsTurningRight, ref myright);
            }
            else if (mydiff > 0) {
                if (DisableForward) {
                    SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, PlayerUnit.MovingInfo.IsMovingForward, ref myforward);
                }
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_RIGHT, !PlayerUnit.MovingInfo.IsTurningRight, ref myright);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, PlayerUnit.MovingInfo.IsTurningLeft, ref myleft);
            }
            return false;
        }
        protected bool WalkingTowards(WoWUnit mPlayerUnit, Vector3 mTargetPosition, double mPositionThreshold) {
            if (Vector3.Distance(mPlayerUnit.Position, mTargetPosition) < mPositionThreshold) {
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, mPlayerUnit.MovingInfo.IsMovingForward, ref myforward);
                return true;
            }
            else {
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_UP, !mPlayerUnit.MovingInfo.IsMovingForward, ref myforward);
                return false;
            }
        }
        protected void Halt(WoWUnit mPlayerUnit) {
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, mPlayerUnit.MovingInfo.IsMovingForward, ref myforward);
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, mPlayerUnit.MovingInfo.IsTurningLeft, ref myleft);
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, mPlayerUnit.MovingInfo.IsTurningRight, ref myright);
        }
        protected bool WalkingTowards(WoWUnit mPlayerUnit, WoWUnit mTargetUnit, double mPositionThreshhold) {
            return WalkingTowards(mPlayerUnit, mTargetUnit.Position, mPositionThreshhold);
        }
        protected virtual ComplexBotStance SetStance() {
            if (focus.GUID == 0) {
                return playerDPStype;
            }
            if (focus.Unit.TargetGUID != 0) {
                //Tank is targetting something
                if (target.GUID != focus.Unit.TargetGUID) {
                    SendKey.Send(ConstController.WindowsVirtualKey.VK_F6);//Change Target to FocusTarget
                }
                if (Program.ClientInfo.Markers.CrossGUID == focus.GUID) {
                    return ComplexBotStance.AoEAtFocus;
                }
                return this.playerDPStype;
            }
            else if (Vector3.Distance(player.Unit.Position, focus.Unit.Position) > FollowFocusStartRange) {
                return ComplexBotStance.FollowFocus;
            }
            else if (stance == ComplexBotStance.FollowFocus && Vector3.Distance(player.Unit.Position,focus.Unit.Position)<=FollowFocusStopRange+0.5) {
                return ComplexBotStance.Halt;
            }
            return stance;
        }
        protected virtual void MoveBasedOnStance(ComplexBotStance st) {
            switch (st) {
                case ComplexBotStance.FollowFocus:
                    WalkingTowards(player.Unit, focus.Unit, FollowFocusStopRange);
                    RotateTowards(player.Unit, focus.Unit, FollowFocusAngle, false);
                    break;
                case ComplexBotStance.DpsTargetRanged:
                    if (target.GUID != 0) {
                        if (WalkingTowards(player.Unit, target.Unit, RangedDPSRange)) {
                            RotateTowards(player.Unit, target.Unit, RangedDPSAngle, true);
                        }
                        RotateTowards(player.Unit, target.Unit, MeleeDPSAngle, false);
                    }
                    break;
                case ComplexBotStance.AoEAtFocus:
                    WalkingTowards(player.Unit, focus.Unit, MeleeDPSRange);
                    RotateTowards(player.Unit, focus.Unit, FollowFocusAngle, false);
                    break;
                case ComplexBotStance.Idle:
                    break;
                case ComplexBotStance.Halt:
                    Halt(player.Unit);
                    break;
                default:
                    throw new NotImplementedException("The selected stance is not yet supported by Complexbot");
            }
        }
    }
}

