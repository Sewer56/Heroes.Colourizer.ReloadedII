using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Heroes.Fun.AuraColorizer.Heroes;
using Reloaded.Memory.Kernel32;
using Reloaded.Memory.Pointers;
using Reloaded.Memory.Sources;
using Reloaded.WPF.Animations.Samples;

namespace Heroes.Fun.AuraColorizer
{
    /// <summary>
    /// Allows for animation of a given <see cref="RgbaColor"/> at a provided address.
    /// </summary>
    public class RgbaColourAnimation<TRgbaColor> : IDisposable where TRgbaColor : IRgbaColor
    {
        /// <summary>
        /// Allows to manipulate individual colour.
        /// </summary>
        public TRgbaColor Color;

        /// <summary>
        /// The animation object executed.
        /// </summary>
        public CycleColorAnimation ColorAnimation { get; set; }

        /// <summary>
        /// Length of the animation in milliseconds.
        /// </summary>
        public int Duration { get; private set; }

        /// <summary>
        /// Frames per second for the animation.
        /// </summary>
        public float FramesPerSecond { get; private set; }

        /// <summary>
        /// The amount of times the animation should be repeated. Set <see cref="ulong.MaxValue"/> for infinite repetitions.
        /// </summary>
        public ulong Repeat { get; set; }

        private Color _originalColor;

        public unsafe RgbaColourAnimation(TRgbaColor color, int duration, float framesPerSecond, ulong repeat = ulong.MaxValue)
        {
            Color           = color;
            Duration        = duration;
            FramesPerSecond = framesPerSecond;
            Repeat          = repeat;

            Color.GiveWritePermission();

            _originalColor = Color.GetColor();
            ColorAnimation  = new CycleColorAnimation(color => Color.SetColor(color), _originalColor, duration, framesPerSecond);
        }

        /// <summary>
        /// Starts the colour animation.
        /// If an animation is already running, cancels the animation and starts a new one.
        /// </summary>
        public void Start()
        {
            ColorAnimation.Animate();
        }

        /// <summary>
        /// Resumes the colour animation.
        /// </summary>
        public void Resume()
        {
            ColorAnimation.Resume();
        }

        /// <summary>
        /// Pauses the colour animation.
        /// </summary>
        public void Suspend()
        {
            ColorAnimation.Pause();
        }

        /// <summary>
        /// Cancels the current animation.
        /// </summary>
        public void Cancel()
        {
            Color.SetColor(_originalColor);
            ColorAnimation.Cancel(100);
        }

        ~RgbaColourAnimation()
        {
            Dispose();
        }

        public void Dispose()
        {
            ColorAnimation?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
