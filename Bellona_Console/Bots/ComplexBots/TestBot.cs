using Bellona_Console.Controller;
using Bellona_Console.MemoryReading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.ComplexBots {

    class TestBot : ComplexBot {


        public TestBot(uint rotaInterval, uint movementInterval, ComplexBotStance mplayerDPStype) : base(rotaInterval, movementInterval, mplayerDPStype) {

        }
        protected override void RotaEvent(object source) {

        }
        protected override void MovementEvent(object source) {
            base.MovementEvent(source);
            this.stance=base.SetStance();
            base.MoveBasedOnStance(this.stance);
        }
    }
}
