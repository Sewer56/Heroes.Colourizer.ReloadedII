﻿using Heroes.Fun.AuraColorizer.Collections;
using Heroes.Fun.AuraColorizer.Configuration;
using Heroes.Fun.AuraColorizer.Enums;
using Heroes.Fun.AuraColorizer.Heroes;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using static Heroes.Fun.AuraColorizer.Utility;

namespace Heroes.Fun.AuraColorizer;

public class Program : IMod
{
    private const string ThisModId = "sonicheroes.fun.colourizer";
    private object _lock = new object();
    private IModLoader _modLoader;

    private RgbaColourAnimation<RgbaColorPtr>[] _jumpBallAnimations;
    private RgbaColourAnimation<RgbaColorPtr>[] _tornadoAnimations;
    private RgbaColourAnimation<RgbaColorPtr>[] _trailAnimations;
    private RgbaColourAnimation<RgbaColorComponentPtrSet>[] _formationGateAnimations;

    private Config.Config _config;
    private ILogger _logger;
    private string _modDirectory;

    private Task _setupColourizer;
    private Configurator _configurator;

    public unsafe void Start(IModLoaderV1 loader)
    {
        _modLoader = (IModLoader) loader;
        _logger = (ILogger) _modLoader.GetLogger();
        _modDirectory = _modLoader.GetDirectoryForModId(ThisModId);

        /* Your mod code starts here. */
        _modLoader.OnModLoaderInitialized += OnModLoaderInitialized;
        _setupColourizer = Task.Run(() =>
        {
            _configurator = new Configurator(_modDirectory);
            _config = _configurator.GetConfiguration<Config.Config>(0);
            _config.ConfigurationUpdated += configurable =>
            {
                _config = (Config.Config)configurable;
                _logger.WriteLine("[Colorizer] Config file changed. Cancelling current effects and applying new.", _logger.ColorGreenLight);
                lock (_lock)
                {
                    CancelAll();
                    Initialize();
                }
            };

            Initialize();
        });
    }

    private void OnModLoaderInitialized()
    {
        _setupColourizer.Wait();
        _modLoader.OnModLoaderInitialized -= OnModLoaderInitialized;
    }

    private void CancelAll()
    {
        _jumpBallAnimations.ForEach(animation => animation.Cancel());
        _tornadoAnimations.ForEach(animation => animation.Cancel());
        _trailAnimations.ForEach(animation => animation.Cancel());
        _formationGateAnimations.ForEach(animation => animation.Cancel());
    }

    private void Initialize()
    {
        _jumpBallAnimations = GetEnumValues<BallColourAddress>().Select(x => new RgbaColourAnimation<RgbaColorPtr>(new RgbaColorPtr((long)x), _config.BallColourHueCycle.Duration, _config.BallColourHueCycle.Framerate)).ToArray();
        _tornadoAnimations  = GetEnumValues<TornadoColourAddress>().Select(x => new RgbaColourAnimation<RgbaColorPtr>(new RgbaColorPtr((long)x), _config.TornadoColourHueCycle.Duration, _config.TornadoColourHueCycle.Framerate)).ToArray();
        _trailAnimations    = GetEnumValues<TrailColourAddress>().Select(x => new RgbaColourAnimation<RgbaColorPtr>(new RgbaColorPtr((long)x), _config.TrailColourHueCycle.Duration, _config.TrailColourHueCycle.Framerate)).ToArray();
        _formationGateAnimations  = BarrierColours.Colours.Select(x => new RgbaColourAnimation<RgbaColorComponentPtrSet>(x, _config.FormationGateColourHueCycle.Duration, _config.FormationGateColourHueCycle.Framerate)).ToArray();

        if (_config.BallColourHueCycle.Enable)           _jumpBallAnimations.ForEach(animation => animation.Start());
        if (_config.TornadoColourHueCycle.Enable)        _tornadoAnimations.ForEach(animation => animation.Start());
        if (_config.TrailColourHueCycle.Enable)          _trailAnimations.ForEach(animation => animation.Start());
        if (_config.FormationGateColourHueCycle.Enable)  _formationGateAnimations.ForEach(animation => animation.Start());
    }

    /* Mod loader actions. */
    public void Suspend()
    {
        if (_config.BallColourHueCycle.Enable)           _jumpBallAnimations.ForEach(animation => animation.Suspend());
        if (_config.TornadoColourHueCycle.Enable)        _tornadoAnimations.ForEach(animation => animation.Suspend());
        if (_config.TrailColourHueCycle.Enable)          _trailAnimations.ForEach(animation => animation.Suspend());
        if (_config.FormationGateColourHueCycle.Enable)  _formationGateAnimations.ForEach(animation => animation.Suspend());
    }

    public void Resume()
    {
        if (_config.BallColourHueCycle.Enable)           _jumpBallAnimations.ForEach(animation => animation.Resume());
        if (_config.TornadoColourHueCycle.Enable)        _tornadoAnimations.ForEach(animation => animation.Resume());
        if (_config.TrailColourHueCycle.Enable)          _trailAnimations.ForEach(animation => animation.Resume());
        if (_config.FormationGateColourHueCycle.Enable)  _formationGateAnimations.ForEach(animation => animation.Resume());
    }

    public void Unload()
    {
        Suspend();
        CancelAll();
    }

    public bool CanUnload()  => true;
    public bool CanSuspend() => true;

    /* Automatically called by the mod loader when the mod is about to be unloaded. */
    public Action Disposing { get; }
}