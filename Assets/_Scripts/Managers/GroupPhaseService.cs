using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets up the macthes for all teams in the group
/// </summary>
public class GroupPhaseService : BaseService
{
    private int MatchDaysPlayed = 0;
    private int totalMatchDays;
    private GroupPhaseServiceView view;
    
    protected override void OnInitialize()
    {
        view = Services.Get<GroupPhaseServiceView>();
        if (!view)
        {
            Debug.LogError("View of group phase service not found");
        }
        SetupMatches();
        AddListeners();
    }

    private void AddListeners()
    {
        view.MatchDayPlayed.AddListener(delegate
        {
            MatchDaysPlayed++;
            if (MatchDaysPlayed >= totalMatchDays)
            {
                CalculateGroupPoints();
                view.SetResultsOverviewButtonActive(true);
            }
        });
    }

    private void CalculateGroupPoints()
    {
        List<Team> teams = Services.Get<GameSettings>().TeamSettings.Teams;
        foreach (Team team in teams)
        {
            team.CalculatePoints(Services.Get<GameSettings>().ScoringSettings);
        }
    }

    public void SetupMatches()
    {
        MatchDaysPlayed = 0;
        List<Team> teams = Services.Get<GameSettings>().TeamSettings.Teams;

        foreach (Team team in teams)
        {
            team.ClearData();
        }
        
        if (teams.Count % 2 != 0)
        {
            Debug.LogError("Not enough teams are present for correct matchmaking");
        }
        
        totalMatchDays = teams.Count - 1;
        int halfSize = teams.Count / 2;

        List<Team> teamsCopy = new();
        teamsCopy.AddRange(teams);
        teamsCopy.RemoveAt(0);

        int teamsSize = teamsCopy.Count;

        GroupPhaseServiceView view = Services.Get<GroupPhaseServiceView>();
        if (!view)
        {
            Debug.LogError("Group phase service view does not exist at this point");
            return;
        }
        view.ClearMatchCardDayView();
        for (int day = 0; day < totalMatchDays; day++)
        {
            Debug.LogFormat("Day {0}", day + 1);
            int teamIndex = day % teamsSize;
            
            Match match1 = new Match(1,teamsCopy[teamIndex], teams[0]);
            Match match2 = null;
            Debug.LogFormat("{0} vs {1}", teamsCopy[teamIndex], teams[0]);

            for (int index = 1; index < halfSize; index++)
            {
                int firstTeam = (day + index) % teamsSize;
                int secondTeam = (day + teamsSize - index) % teamsSize;
                match2 = new Match(2,teamsCopy[firstTeam], teamsCopy[secondTeam]);
                Debug.LogFormat("{0} vs {1}", teamsCopy[firstTeam], teamsCopy[secondTeam]);
            }
            
            view.AddMatchDayToView(day + 1,match1, match2);
        }
        view.Open();
    }
}