using FormalTankFight.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace FormalTankFight
{
    class SoundManger
    {
        private static SoundPlayer startPlayer = new SoundPlayer();//soundplayer为media系统命名空间下的一个数据类型
        private static SoundPlayer addPlayer = new SoundPlayer();
        private static SoundPlayer blastPlayer = new SoundPlayer();
        private static SoundPlayer firePlayer = new SoundPlayer();
        private static SoundPlayer hitPlayer = new SoundPlayer();

        public static void InitSound()//不要每次播放声音的时候都创造一个对象初始化声音，统一把声音的初始化定义在一起
        {
            startPlayer.Stream = Resources.start;//stream为soundplayer数据类型里面的一个元素，用于设置声音源
            addPlayer.Stream = Resources.add;
            blastPlayer.Stream = Resources.blast;
            firePlayer.Stream = Resources.fire;
            hitPlayer.Stream = Resources.hit;
        }

      

        public static void PlayStart()
        {
            
            startPlayer.Play();//play方法用于播放声音
        }

        public static void PlayAdd()
        {
            addPlayer.Play();
        }

        public static void PlayBlast()
        {
            blastPlayer.Play();
        }

        public static void PlayFire()
        {
            firePlayer.Play();
        }

        public static void PlayHit()
        {
            hitPlayer.Play();
        }
    }
}
