using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Card view of a match day.
/// Contains all info needed like the matches playing that day, the teams and their scoring after the match is done
/// </summary>
public class MatchDayCardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayTitle;
    [SerializeField] private TextMeshProUGUI homeTeamMatchOne;
    [SerializeField] private TextMeshProUGUI homeTeamMatchTwo;
    [SerializeField] private TextMeshProUGUI awayTeamMatchOne;
    [SerializeField] private TextMeshProUGUI awayTeamMatchTwo;

    [SerializeField] private TextMeshProUGUI scoreMatchOne;
    [SerializeField] private TextMeshProUGUI scoreMatchTwo;

    [SerializeField] private Button playMatches;

    public UnityEvent MatchDayPlayed = new();

    public void Initialize(int day, Match matchOne, Match matchTwo)
    {
        dayTitle.SetText("Day {0}", day);

        homeTeamMatchOne.SetText(matchOne.Team1.Name);
        awayTeamMatchOne.SetText(matchOne.Team2.Name);

        homeTeamMatchTwo.SetText(matchTwo.Team1.Name);
        awayTeamMatchTwo.SetText(matchTwo.Team2.Name);

        AddListeners(matchOne, matchTwo);
        playMatches.image.color = Services.Get<UISettings>().UIColors.DefaultGreen;
        playMatches.interactable = true;
    }

    private void AddListeners(Match matchOne, Match matchTwo)
    {
        playMatches.onClick.AddListener(delegate
        {
            matchOne.PlayMatch(this);
            matchTwo.PlayMatch(this);
            playMatches.image.color = Services.Get<UISettings>().UIColors.DefaultRed;
            playMatches.interactable = false;
            MatchDayPlayed.Invoke();
        });
    }

    public void UpdateMatchResults(int MatchID, int goalsTeam1, int goalsTeam2)
    {
        switch (MatchID)
        {
            case 1:
                scoreMatchOne.SetText("{0} - {1}", goalsTeam1, goalsTeam2);
                break;
            case 2:
                scoreMatchTwo.SetText("{0} - {1}", goalsTeam1, goalsTeam2);
                break;
        }
    }
}