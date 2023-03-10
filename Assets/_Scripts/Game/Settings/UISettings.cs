using System;
using UnityEngine;

/// <summary>
/// Class containing all UI related settings like colors & UI prefabs
/// </summary>
public class UISettings : BaseSettings
{
    public UIPrefabs UIPrefabs;
    public UIColors UIColors;
}

[Serializable]
public class UIPrefabs
{
    public MatchDayCardView MatchDayCardView;
    public TeamResultsRowView TeamResultsRowView;
}

[Serializable]
public class UIColors
{
    public Color DefaultGreen;
    public Color DefaultRed;
}