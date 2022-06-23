using System.ComponentModel;
using System.IO;
using Heroes.Fun.AuraColorizer.Configuration;

namespace Heroes.Fun.AuraColorizer.Config
{
    public class Config : Configurable<Config>
    {
        [DisplayName("Jump Ball Hue Cycle")]
        [Description("Control the hue cycle effects on the characters' jumping ball.")]
        public HueCycleEffectSettings BallColourHueCycle            { get; set; } = new HueCycleEffectSettings(true, 1337, 60);

        [DisplayName("Tornado Colour Hue Cycle")]
        [Description("Control the hue cycle effects on the speed characters' tornado ability.")]
        public HueCycleEffectSettings TornadoColourHueCycle         { get; set; } = new HueCycleEffectSettings(true, 1337, 60);

        [DisplayName("Jump Trail Hue Cycle")]
        [Description("Control the hue cycle effects on the trails left by the characters when performing an air dash/homing attack.")]
        public HueCycleEffectSettings TrailColourHueCycle           { get; set; } = new HueCycleEffectSettings(true, 1337, 60);

        [DisplayName("Formation Gate Hue Cycle")]
        [Description("Control the hue cycle effects on the individual formation switch gates.")]
        public HueCycleEffectSettings FormationGateColourHueCycle   { get; set; } = new HueCycleEffectSettings(true, 1337, 60);
    }
}
