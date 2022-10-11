using System.Windows.Media;

namespace Heroes.Fun.AuraColorizer.Heroes;

public interface IRgbaColor
{
    /// <summary>
    /// Changes Windows' memory permissions to allow the colour to be changed.
    /// </summary>
    unsafe void GiveWritePermission();

    /// <summary>
    /// Assigns a new color.
    /// </summary>
    unsafe void SetColor(RgbaColor color);

    /// <summary>
    /// Retrieves the colour.
    /// </summary>
    Color GetColor();
}