using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// The view of the group phase service.
/// Contains card views of all match days
/// </summary>
public class GroupPhaseServiceView : BaseUIService
{
    [SerializeField] private Transform MatchDaysHolder;
    [SerializeField] private Button ShowResultsOverview;

    public UnityEvent MatchDayPlayed = new();

    protected override void OnInitialize()
    {
        base.OnInitialize();
        SetResultsOverviewButtonActive(false);
        ClearMatchCardDayView();
        AddListeners();
        Close();
    }

    private void AddListeners()
    {
        ShowResultsOverview.onClick.AddListener(delegate
        {
            Close();
            Services.Get<GroupResultsView>().Open();
        });
    }

    public void AddMatchDayToView(int day, Match match1, Match match2)
    {
        MatchDayCardView matchDayCardView =
            Instantiate(UISettings.UIPrefabs.MatchDayCardView, MatchDaysHolder);
        matchDayCardView.Initialize(day, match1, match2);
        matchDayCardView.MatchDayPlayed.AddListener(delegate { MatchDayPlayed.Invoke(); });
    }

    public void SetResultsOverviewButtonActive(bool value)
    {
        ShowResultsOverview.gameObject.SetActive(value);
    }

    public void ClearMatchCardDayView()
    {
        foreach (Transform child in MatchDaysHolder)
        {
            Destroy(child.gameObject);
        }
    }

    protected override IEnumerator HandleScreenOpening()
    {
        yield return new WaitForSeconds(OpenAnimTime);
        SetResultsOverviewButtonActive(false);
        SetCanvasGroupActive(true);
    }

    protected override IEnumerator HandleScreenClosing()
    {
        yield return new WaitForSeconds(CloseAnimTime);
        SetCanvasGroupActive(false);
    }
}