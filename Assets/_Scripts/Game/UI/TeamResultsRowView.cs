using TMPro;
using UnityEngine;

/// <summary>
/// Row view of the total results of a team within the group
/// </summary>
public class TeamResultsRowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI position;
    [SerializeField] private TextMeshProUGUI team;
    [SerializeField] private TextMeshProUGUI played;
    [SerializeField] private TextMeshProUGUI win;
    [SerializeField] private TextMeshProUGUI draw;
    [SerializeField] private TextMeshProUGUI loss;
    [SerializeField] private TextMeshProUGUI goalsFor;
    [SerializeField] private TextMeshProUGUI goalsAgainst;
    [SerializeField] private TextMeshProUGUI goalDifference;
    [SerializeField] private TextMeshProUGUI points;

    public void Initialize(Team t, int position)
    {
        this.position.SetText(position.ToString());
        team.SetText(t.Name);
        played.SetText(t.MatchesPlayed.ToString());
        win.SetText(t.Wins.ToString());
        draw.SetText(t.Draws.ToString());
        loss.SetText(t.Losses.ToString());
        goalsFor.SetText(t.GoalsFor.ToString());
        goalsAgainst.SetText(t.GoalsAgainst.ToString());
        goalDifference.SetText(t.CalculateGoalDifference().ToString());
        points.SetText(t.Points.ToString());
    }
}