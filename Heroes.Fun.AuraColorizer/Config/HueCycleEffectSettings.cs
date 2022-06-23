using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Heroes.Fun.AuraColorizer.Config
{
    public class HueCycleEffectSettings
    {
        public bool  Enable    { get; set; }

        [Description("The duration of an entire indvidual hue cycle.")]
        public int   Duration  { get; set; }

        [Description("How many times the hue is changed during the Duration of one cycle.")]
        public float Framerate { get; set; }

        public HueCycleEffectSettings() { }
        public HueCycleEffectSettings(bool enable, int duration, float framerate)
        {
            Enable = enable;
            Duration = duration;
            Framerate = framerate;
        }

        public override string ToString() => $"Enabled: {Enable}, Duration: {Duration}, Framerate: {Framerate}";
    }
}
