using UnityEngine;
/// <summary>
/// Scriptable object containing all data of a team
/// Object gets reset when the teams are matched to each other
/// </summary>
[CreateAssetMenu(fileName = "Team", menuName = "ScriptableObjects/TeamObject", order = 1)]
public class Team : ScriptableObject
{
    public string Name;

    [Header("Should be a value between 0 and 100")]
    public int Strength;
    public int Defence;

    [HideInInspector] public int MatchesPlayed;
    [HideInInspector] public int Wins;
    [HideInInspector] public int Draws;
    [HideInInspector] public int Losses;
    [HideInInspector] public int GoalsFor;
    [HideInInspector] public int GoalsAgainst;
    [HideInInspector] public int Points;
    [HideInInspector] public int GoalDifference;

    public void ClearData()
    {
        MatchesPlayed = 0;
        Wins = 0;
        Draws = 0;
        Losses = 0;
        GoalsFor = 0;
        GoalsAgainst = 0;
        Points = 0;
        GoalDifference = 0;
    }

    public int CalculateGoalDifference()
    {
        return GoalDifference = GoalsFor - GoalsAgainst;
    }

    public void CalculatePoints(ScoringSettings scoring)
    {
        Points += Wins * scoring.WinPoints;
        Points += Draws * scoring.DrawPoints;
        Points += Losses * scoring.LossPoints;
    }
}