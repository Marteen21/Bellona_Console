using Bellona_Console.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Bellona_Console.Bots {
    class ComplexBot {
        private System.Timers.Timer rotaTimer;
        private System.Timers.Timer movementTimer;
        protected bool needsToMove = false;

        protected Vector3 movementDestination;
        protected float rotationDestination;

        protected float rotationThreshhold;
        protected float positionThreshhold;

        protected long rotaTicks = 0;

        public ComplexBot(uint rotaInterval) {
            SetupTimer(out rotaTimer, rotaInterval);
            this.rotaTimer.Elapsed += RotaEvent;
        }
        public ComplexBot(uint rotaInterval, uint movementInterval) {
            SetupTimer(out rotaTimer, rotaInterval);
            SetupTimer(out movementTimer, movementInterval);
            this.rotaTimer.Elapsed += RotaEvent;
            this.movementTimer.Elapsed += MovementEvent;
        }

        protected virtual void MovementEvent(object sender, ElapsedEventArgs e) {
            throw new NotImplementedException();
        }

        protected virtual void RotaEvent(Object source, System.Timers.ElapsedEventArgs e) {
            throw new NotImplementedException();
        }
        private void SetupTimer(out System.Timers.Timer mTimer,uint tt) {
            mTimer = new System.Timers.Timer();
            mTimer.Interval = tt;
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
        }
    }
}
