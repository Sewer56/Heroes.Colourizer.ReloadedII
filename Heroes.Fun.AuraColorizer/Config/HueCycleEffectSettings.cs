using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Fun.AuraColorizer.Config
{
    public class HueCycleEffectSettings
    {
        public bool  Enable    { get; set; }
        public int   Duration  { get; set; }
        public float Framerate { get; set; }

        public HueCycleEffectSettings() { }

        public HueCycleEffectSettings(bool enable, int duration, float framerate)
        {
            Enable = enable;
            Duration = duration;
            Framerate = framerate;
        }
    }
}
