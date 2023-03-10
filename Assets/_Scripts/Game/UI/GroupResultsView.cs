using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The result view of the group phase
/// Shows an overview of each team and their position in the group based on their score
/// </summary>
public class GroupResultsView : BaseUIService
{
    [SerializeField] private Transform teamResultsHolder;
    [SerializeField] private Button resetSimulation;
    protected override void OnInitialize()
    {
        base.OnInitialize();
        ClearTeamResultsHolder();
        AddListeners();
        Close();
    }

    private void AddListeners()
    {
        resetSimulation.onClick.AddListener(delegate
        {
            Close();
            GameManager.Instance.ResetSimulation();
        });
    }

    private void PopulateTeamResults()
    {
        ClearTeamResultsHolder();
        TeamResultsRowView rowViewObj = UISettings.UIPrefabs.TeamResultsRowView;
        List<Team> teams = Services.Get<GameSettings>().TeamSettings.Teams;
        teams.Sort((a,b) => a.Points.CompareTo(b.Points));
        teams.Reverse();
        for (int index = 0; index < teams.Count; index++)
        {
            Team t = teams[index];
            TeamResultsRowView rowView = Instantiate(rowViewObj, teamResultsHolder);
            rowView.Initialize(t, index + 1);
        }
    }
    
    private void ClearTeamResultsHolder()
    {
        foreach (Transform child in teamResultsHolder)
        {
            Destroy(child.gameObject);
        }
    }

    protected override IEnumerator HandleScreenOpening()
    {
        yield return new WaitForSeconds(OpenAnimTime);
        PopulateTeamResults();
        SetCanvasGroupActive(true);
    }

    protected override IEnumerator HandleScreenClosing()
    {
        yield return new WaitForSeconds(CloseAnimTime);
        SetCanvasGroupActive(false);
    }
}