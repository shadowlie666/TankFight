using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using TankFight2._0.Properties;

namespace TankFight2._0
{
    class SoundManager
    {
        private static SoundPlayer start = new SoundPlayer();
        private static SoundPlayer add = new SoundPlayer();
        private static SoundPlayer blast = new SoundPlayer();
        private static SoundPlayer fire = new SoundPlayer();
        private static SoundPlayer hit = new SoundPlayer();

        public void InitSound()
        {
            start.Stream = Resources.start;
            add.Stream = Resources.add;
            blast.Stream = Resources.blast;
            fire.Stream = Resources.fire;
            hit.Stream = Resources.hit;
        }

        public static void PlayStart()
        {
            start.Play(); 
        }
    }
}
