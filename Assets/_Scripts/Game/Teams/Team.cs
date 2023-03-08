using UnityEngine;

namespace _Scripts.Game.MatchGroups
{
    [CreateAssetMenu(fileName = "Team", menuName = "ScriptableObjects/TeamObject", order = 1)]
    public class Team : ScriptableObject
    {
        public string Name;
        public int Strength;
    }
}