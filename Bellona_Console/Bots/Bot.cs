using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    public class Bot {
        private System.Timers.Timer botTimer;
        protected long ticks = 0;

        public Bot(uint tt) {
            SetupTimer(tt);
            this.botTimer.Elapsed += BotEvent;
        }
        public virtual void BotEvent(Object source, System.Timers.ElapsedEventArgs e) {
        }
        private void SetupTimer(uint tt) {
            this.botTimer = new System.Timers.Timer();
            this.botTimer.Interval = tt;
            this.botTimer.AutoReset = true;
            this.botTimer.Enabled = true;
        }
    }
}
