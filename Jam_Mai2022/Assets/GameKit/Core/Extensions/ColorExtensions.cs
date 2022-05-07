using UnityEngine;

public static class ColorExtensions
{
    //Set transparency on color
    public static Color SetTransparency(this Color _color,float _alpha) 
    {
        _color = new Color(_color.r,_color.g,_color.b,_alpha);
        return _color;
    }
}