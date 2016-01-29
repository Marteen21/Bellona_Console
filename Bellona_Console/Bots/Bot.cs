using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Bellona_Console.Bots {
    public class Bot {
        private Timer botTimer;
        protected long ticks = 0;

        public Bot(uint tt) {
            SetupTimer(tt);
        }
        public virtual void BotEvent(Object source) {
        }
        private void SetupTimer(uint tt) {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            TimerCallback tcb = this.BotEvent;
            this.botTimer = new Timer(tcb, autoEvent, 1000, tt);
        }
    }
}
