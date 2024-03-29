﻿using System.Windows.Media;
using Reloaded.Memory.Kernel32;
using Reloaded.Memory.Sources;

namespace Heroes.Fun.AuraColorizer.Heroes;

/// <summary>
/// A version of <see cref="RgbaColor"/> where each of the four components is stored in a different memory location.
/// </summary>
public unsafe struct RgbaColorComponentPtrSet : IRgbaColor
{
    /// <summary>
    /// Range 0 - 255.
    /// </summary>
    public byte* R;

    /// <summary>
    /// Range 0 - 255.
    /// </summary>
    public byte* G;

    /// <summary>
    /// Range 0 - 255.
    /// </summary>
    public byte* B;

    /// <summary>
    /// Range 0 - 255.
    /// </summary>
    public byte* A;

    public RgbaColorComponentPtrSet(byte* r, byte* g, byte* b, byte* a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary>
    /// Changes Windows' memory permissions to allow the colour to be changed.
    /// </summary>
    public void GiveWritePermission()
    {
        Memory.Instance.ChangePermission((nuint)R, sizeof(byte), Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READWRITE);
        Memory.Instance.ChangePermission((nuint)G, sizeof(byte), Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READWRITE);
        Memory.Instance.ChangePermission((nuint)B, sizeof(byte), Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READWRITE);
        Memory.Instance.ChangePermission((nuint)A, sizeof(byte), Kernel32.MEM_PROTECTION.PAGE_EXECUTE_READWRITE);
    }

    /// <summary>
    /// Assigns a new color.
    /// </summary>
    public void SetColor(RgbaColor color)
    {
        *R = color.R;
        *G = color.G;
        *B = color.B;
        *A = color.A;
    }

    /// <summary>
    /// Retrieves the colour.
    /// </summary>
    public Color GetColor() => ToColor(this);

    public static Color ToColor(RgbaColorComponentPtrSet colorComponent) => Color.FromArgb(*colorComponent.A, *colorComponent.R, *colorComponent.G, *colorComponent.B);
    public static implicit operator Color(RgbaColorComponentPtrSet colorComponent) => ToColor(colorComponent);
}