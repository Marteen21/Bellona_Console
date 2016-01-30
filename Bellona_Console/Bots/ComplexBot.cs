using Bellona_Console.Controller;
using Bellona_Console.Models;
using Bellona_Console.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bellona_Console.Bots {
    class ComplexBot {
        private Timer rotaTimer;
        private Timer movementTimer;
        protected bool needsToMove = false;

        protected Vector3 movementDestination;
        protected float rotationDestination;


        protected long rotaTicks = 0;
        public ComplexBot(uint rotaInterval, uint movementInterval) {
            SetupTimer(out rotaTimer, RotaEvent, rotaInterval);
            SetupTimer(out movementTimer, MovementEvent, movementInterval);
        }

        protected virtual void MovementEvent(Object source) {
            throw new NotImplementedException();
        }

        protected virtual void RotaEvent(Object source) {
            throw new NotImplementedException();
        }
        private void SetupTimer(out Timer mTimer, TimerCallback tcb, uint tt) {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            mTimer = new Timer(tcb, autoEvent, 1000, tt);
        }

        protected bool RotateTowards(WoWUnit PlayerUnit, WoWUnit TargetUnit, double RotationThreshhold, bool DisableForward) {
            double mydiff = Angles.AngleDiff(Angles.Calculateangle(TargetUnit, PlayerUnit), PlayerUnit.Rotation);
            if (DisableForward) {
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, PlayerUnit.MovingInfo.IsMovingForward);
            }
            if ((Math.Abs(mydiff) < RotationThreshhold)) {
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, PlayerUnit.MovingInfo.IsTurningLeft);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, PlayerUnit.MovingInfo.IsTurningRight);
                return true;
            }
            else if (mydiff < 0) {
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_LEFT, !PlayerUnit.MovingInfo.IsTurningLeft);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_RIGHT, PlayerUnit.MovingInfo.IsTurningRight);
            }
            else if (mydiff > 0) {
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_RIGHT, !PlayerUnit.MovingInfo.IsTurningRight);
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_LEFT, PlayerUnit.MovingInfo.IsTurningLeft);
            }
            return false;
        }
        protected bool WalkingTowards(WoWUnit mPlayerUnit, Vector3 mTargetPosition, double mPositionThreshold) {
            if (Vector3.Distance(mPlayerUnit.Position, mTargetPosition) < mPositionThreshold) {
                SendKey.KeyUp(ConstController.WindowsVirtualKey.VK_UP, !mPlayerUnit.MovingInfo.IsMovingForward);
                return true;
            }
            else {
                SendKey.KeyDown(ConstController.WindowsVirtualKey.VK_UP, !mPlayerUnit.MovingInfo.IsMovingForward);
                return false;
            }
        }
        protected bool WalkingTowards(WoWUnit mPlayerUnit, WoWUnit mTargetUnit, double mPositionThreshhold) {
            return WalkingTowards(mPlayerUnit, mTargetUnit.Position, mPositionThreshhold);
        }
    }
}
