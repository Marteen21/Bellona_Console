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
using Bellona_Console.Models.Other;

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
        CCMarker = 9,
    }
    class ComplexBot {
        #region Constants
        public static readonly double RangedDPSRange = 28;
        public static readonly double MeleeDPSRange = 4;
        public static readonly double BackDPSRange = 1;
        public static readonly double FollowFocusStartRange = 20;
        public static readonly double FollowFocusStopRange = 5;

        public static readonly double RangedDPSAngle = Math.PI * 0.4;
        public static readonly double MeleeDPSAngle = Math.PI * 0.06;
        public static readonly double FollowFocusAngle = Math.PI * 0.06;
        #endregion

        #region PrivateVariables
        private Timer rotaTimer;
        private Timer movementTimer;
        private bool myforward;
        private bool myleft;
        private bool myright;
        private ComplexBotStance playerDPStype;
        #endregion

        #region ProtectedVariables
        protected GameObject target;
        protected GameObject player;
        protected GameObject focus;
        protected bool needsToMove = false;
        protected ComplexBotStance stance;
        protected long rotaTicks = 0;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexBot"/> class.
        /// </summary>
        /// <param name="rotaInterval">The value to assign the period of the RotationTimer. This must be an unsigned integer.</param>
        /// <param name="movementInterval">The value to assign the period of the MovementTimer. This must be an unsigned integer.</param>
        /// <param name="mplayerDPStype">The value to assign the bot DPS stance. This must be a <see cref="ComplexBotStance"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mplayerDPStype"/> is not an acceptable DPS type </exception>
        public ComplexBot(uint rotaInterval, uint movementInterval, ComplexBotStance mplayerDPStype) {
            SetupTimer(out rotaTimer, RotaEvent, rotaInterval, rotaInterval / 2);
            SetupTimer(out movementTimer, MovementEvent, movementInterval, movementInterval);
            this.playerDPStype = mplayerDPStype;
        }

        #region TimerEvents
        protected virtual void MovementEvent(Object source) {
            RefreshGameObjects();
        }
        protected virtual void RotaEvent(Object source) {
            throw new NotImplementedException();
        } 
        #endregion
        protected virtual void RefreshGameObjects() {
            Program.ClientInfo.Refresh(Program.Wow);
            target = new GameObject(Program.Wow, Program.ClientInfo.TargetGUID);
            player = new GameObject(Program.Wow, Program.ClientInfo.PlayerGUID);
            focus = new GameObject(Program.Wow, Program.ClientInfo.FocusGUID);
        }


        private void SetupTimer(out Timer mTimer, TimerCallback tcb, uint period) {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            mTimer = new Timer(tcb, autoEvent, 1000, period);
        }
        private void SetupTimer(out Timer mTimer, TimerCallback tcb, uint period, uint dueTime) {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            mTimer = new Timer(tcb, autoEvent, dueTime, period);
        }


        protected virtual ComplexBotStance SetStance() {
            if (focus.GUID == 0) {
                return playerDPStype;
            }
            else {
                if (Program.ClientInfo.Markers.getGUIDByMarkerType(MarkerType.cross) == focus.GUID) {   //AOE STANCE
                    return ComplexBotStance.AoEAtFocus;
                }
                else if (focus.Unit.TargetGUID != 0) {
                    //Tank is targetting something
                    if (target.GUID != focus.Unit.TargetGUID) {
                        SendKey.Send(ConstController.WindowsVirtualKey.VK_F6);//Change Target to FocusTarget
                    }

                    return this.playerDPStype;
                }
                else if (Vector3.Distance(player.Unit.Position, focus.Unit.Position) > FollowFocusStartRange) {
                    return ComplexBotStance.FollowFocus;
                }
                else if (stance == ComplexBotStance.FollowFocus && Vector3.Distance(player.Unit.Position, focus.Unit.Position) <= FollowFocusStopRange + 0.5) {
                    return ComplexBotStance.Halt;
                }
            }
            return stance;
        }
        protected virtual void MoveBasedOnStance(ComplexBotStance st) {
            switch (st) {
                case ComplexBotStance.FollowFocus:
                    FollowFocusMovement();
                    break;
                case ComplexBotStance.DpsTargetRanged:
                    DpsTargetRangedMovement();
                    break;
                case ComplexBotStance.AoEAtFocus:
                    AoEAtFocusMovement();
                    break;
                case ComplexBotStance.Idle:
                    IdleMovement();
                    break;
                case ComplexBotStance.Halt:
                    HaltMovement();
                    break;
                case ComplexBotStance.DpsTargetBackMelee:
                    DpsTargetBackMeleeMovement();
                    break;
                default:
                    throw new NotImplementedException("The selected stance is not yet supported by Complexbot");
            }
        }
        #region DefaultMovements
        protected virtual void FollowFocusMovement() {
            WalkingTowards(player.Unit, focus.Unit, FollowFocusStopRange);
            RotateTowards(player.Unit, focus.Unit, FollowFocusAngle, false);
        }
        protected virtual void AoEAtFocusMovement() {
            WalkingTowards(player.Unit, focus.Unit, MeleeDPSRange);
            RotateTowards(player.Unit, focus.Unit, FollowFocusAngle, false);
        }
        protected virtual void DpsTargetRangedMovement() {
            if (target.GUID != 0) {
                if (WalkingTowards(player.Unit, target.Unit, RangedDPSRange)) {
                    RotateTowards(player.Unit, target.Unit, RangedDPSAngle, true);//In Range
                }
                RotateTowards(player.Unit, target.Unit, MeleeDPSAngle, false); //Not in range
            }
        }
        protected virtual void DpsTargetMeleeMovement() {
            if (target.GUID != 0) {
                if (WalkingTowards(player.Unit, target.Unit, MeleeDPSRange)) {
                    RotateTowards(player.Unit, target.Unit, MeleeDPSAngle, true);//In Range
                }
                RotateTowards(player.Unit, target.Unit, MeleeDPSAngle, false);//Not in range
            }
        }
        protected virtual void DpsTargetBackMeleeMovement() {
            if (target.GUID != 0) {
                if (WalkingTowards(player.Unit, Vector3.Behindtarget(target), BackDPSRange)) {
                    RotateTowards(player.Unit, target.Unit, MeleeDPSAngle, true);//In Range
                }
                else {
                    RotateTowards(player.Unit, Vector3.Behindtarget(target), MeleeDPSAngle, false);//Not in range
                }
            }
        }
        protected virtual void FollowTargetMovement() {
            throw new NotImplementedException("The selected stance is not yet supported by Complexbot");
        }
        protected virtual void FollowTargetBack() {
            throw new NotImplementedException("The selected stance is not yet supported by Complexbot");
        }
        protected virtual void IdleMovement() {
            //NOP
        }
        protected virtual void HaltMovement() {
            Halt(player.Unit);
        }
        protected virtual void CCMarker(MarkerType mt, List<uint> ccspellids) {
            
        }
        #endregion
        #region MovementControlling
        protected bool RotateTowards(WoWUnit PlayerUnit, Vector3 TargetUnitPos, double RotationThreshhold, bool DisableForward) {
            double mydiff = Angles.AngleDiff(Angles.Calculateangle(TargetUnitPos, PlayerUnit.Position), PlayerUnit.Rotation);
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

        protected bool RotateTowards(WoWUnit PlayerUnit, WoWUnit TargetUnit, double RotationThreshhold, bool DisableForward) {
            return RotateTowards(PlayerUnit, TargetUnit.Position, RotationThreshhold, DisableForward);
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
        protected bool WalkingTowards(WoWUnit mPlayerUnit, WoWUnit mTargetUnit, double mPositionThreshhold) {
            return WalkingTowards(mPlayerUnit, mTargetUnit.Position, mPositionThreshhold);
        }
        protected void Halt(WoWUnit mPlayerUnit) {
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, mPlayerUnit.MovingInfo.IsMovingForward, ref myforward);
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, mPlayerUnit.MovingInfo.IsTurningLeft, ref myleft);
            SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, mPlayerUnit.MovingInfo.IsTurningRight, ref myright);
        }
        #endregion
    }
}

