using System;
using System.Collections.Generic;
using System.Text;
using Heroes.Fun.AuraColorizer.Heroes;

namespace Heroes.Fun.AuraColorizer.Collections
{
    public static class BarrierColours
    {
        public static unsafe RgbaColorComponentPtrSet[] Colours =
        {
            new RgbaColorComponentPtrSet((byte*) 0x47244D, (byte*) 0x472452, (byte*) 0x472457, (byte*) 0x472443), // Power barrier
            new RgbaColorComponentPtrSet((byte*) 0x47245E, (byte*) 0x472463, (byte*) 0x472468, (byte*) 0x472443), // Flight Barrier
            new RgbaColorComponentPtrSet((byte*) 0x47246F, (byte*) 0x472474, (byte*) 0x472479, (byte*) 0x472443)  // Speed Barrier
        };
    }
}
