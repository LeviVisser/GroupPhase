using System;
using System.Collections.Generic;
using _Scripts.Game.MatchGroups;

namespace _Scripts.Game.Settings
{
    public class GameSettings : BaseSettings
    {
        public TeamSettings TeamSettings;
    }

    [Serializable]
    public class TeamSettings
    {
        public List<Team> Teams = new();
    }
}