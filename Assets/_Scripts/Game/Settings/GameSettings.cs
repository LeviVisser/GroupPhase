using System;
using System.Collections.Generic;

/// <summary>
/// Class containing all settings needed to run the program.
/// Here you can place your otherwise 'random values'
/// They will be accessible via the GameBehaviour if inherited, or via the service locator as:
/// Services.Get<GameSettings>().<Replace with class name of needed settings class>
/// </summary>
public class GameSettings : BaseSettings
{
    public TeamSettings TeamSettings;
    public MatchSettings MatchSettings;
    public ScoringSettings ScoringSettings;
}

[Serializable]
public class TeamSettings
{
    public List<Team> Teams = new();
}

[Serializable]
public class MatchSettings
{
    public int MaxGoalCount;
}

[Serializable]
public class ScoringSettings
{
    public int WinPoints;
    public int DrawPoints;
    public int LossPoints;
}