using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Theme
{
    public string name;
    public int coinCost = 500;
    public ThemeState state;
    public enum ThemeState
    {
        Enabled,
        Disabled,
        Unpurchased
    }
}
