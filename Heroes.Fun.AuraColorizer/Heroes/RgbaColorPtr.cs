﻿using System.Windows.Media;
using Reloaded.Memory.Kernel32;
using Reloaded.Memory.Sources;

namespace Heroes.Fun.AuraColorizer.Heroes;

/// <summary>
/// Represents a colour formed using the R,G,B,A components.
/// </summary>
public unsafe struct RgbaColorPtr : IRgbaColor
{
    /// <summary>
    /// Range 0 - 255.
    /// </summary>
    public RgbaColor* Color;

    /// <summary>
    /// Creates an RGBA colour.
    /// </summary>
    public RgbaColorPtr(RgbaColor* color)
    {
        Color = color;
    }

    /// <summary>
    /// Creates an RGBA colour.
    /// </summary>
    public RgbaColorPtr(long colorPtr)
    {
        Color = (RgbaColor*) colorPtr;
    }

    public void GiveWritePermission() => Memory.Instance.ChangePermission((nuint) Color, sizeof(RgbaColor), Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READWRITE);
    public void SetColor(RgbaColor color) => *Color = color;
    public Color GetColor() => *Color;
}