using UnityEngine;

/// <summary>
/// A match contains 2 teams and plays out the match according to the teams strength and defence values.
/// </summary>
public class Match
{
    public Team Team1 { get; private set; }
    public Team Team2 { get; private set; }

    private int matchID;
    
    public Match(int matchID, Team team1, Team team2)
    {
        this.matchID = matchID;
        Team1 = team1;
        Team2 = team2;
    }
    
    public void PlayMatch(MatchDayCardView view)
    {
        int maxGoalCount = Services.Get<GameSettings>().MatchSettings.MaxGoalCount;
        // Generate goals team 1
        int teamOneGoalChance = Team1.Strength - Team2.Defence;
        int teamOneGoals = 0;
        for (int i = 0; i < maxGoalCount; i++)
        {
            if (teamOneGoalChance < 0)
            {
                break;
            }
            int RandomNumber = Random.Range(0, 100);
            if (RandomNumber >= teamOneGoalChance)
            {
                teamOneGoals++;
            }
        }
        
        // Generate goals team 2
        int teamTwoGoalChance = Team2.Strength - Team1.Defence;
        int teamTwoGoals = 0;
        for (int i = 0; i < maxGoalCount; i++)
        {
            if (teamTwoGoalChance < 0)
            {
                break;
            }
            int RandomNumber = Random.Range(0, 100);
            if (RandomNumber >= teamTwoGoalChance)
            {
                teamTwoGoals++;
            }
        }

        // Update played matched
        Team1.MatchesPlayed += 1;
        Team2.MatchesPlayed += 1;
        
        // Update goals
        Team1.GoalsFor += teamOneGoals;
        Team2.GoalsFor += teamTwoGoals;

        Team1.GoalsAgainst += teamTwoGoals;
        Team2.GoalsAgainst += teamOneGoals;

        if (teamOneGoals > teamTwoGoals)
        {
            Team1.Wins += 1;
            Team2.Losses += 1;
        } else if (teamTwoGoals > teamOneGoals)
        {
            Team2.Wins += 1;
            Team1.Losses += 1;
        } 
        
        // The match is a draw
        else if (teamOneGoals == teamTwoGoals)
        {
            Team1.Draws += 1;
            Team2.Draws += 1;
        }
        
        view.UpdateMatchResults(matchID,teamOneGoals, teamTwoGoals);
    }
}