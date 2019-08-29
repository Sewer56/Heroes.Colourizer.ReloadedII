using System.IO;

namespace Heroes.Fun.AuraColorizer.Config
{
    public class Config : JsonSerializable<Config>
    {
        private const string ConfigFileName = "Config.json";

        public Config()
        { }

        /* Properties */
        public HueCycleEffectSettings BallColourHueCycle            { get; set; } = new HueCycleEffectSettings(true, 1337, 60);
        public HueCycleEffectSettings TornadoColourHueCycle         { get; set; } = new HueCycleEffectSettings(true, 1337, 60);
        public HueCycleEffectSettings TrailColourHueCycle           { get; set; } = new HueCycleEffectSettings(true, 1337, 60);
        public HueCycleEffectSettings FormationGateColourHueCycle   { get; set; } = new HueCycleEffectSettings(true, 1337, 60);

        /* Helpers */
        public static string FilePath(string modDirectory) => Path.Combine(modDirectory, ConfigFileName);
        public static Config FromJson(string modDirectory) => Config.FromPath(FilePath(modDirectory));
        public void ToJson(string modDirectory) => Config.ToPath(this, FilePath(modDirectory));
    }
}
