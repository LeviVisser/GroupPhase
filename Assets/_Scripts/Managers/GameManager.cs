using UnityEngine;

/// <summary>
/// Starting point of the application, should be the only script implementing Unity's Awake(), Start(), and Update methods
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [Header("Bootstrapper")] [SerializeField]
    private Bootstrapper _bootstrapper;

    protected override void Awake()
    {   
        base.Awake();
        _bootstrapper.Initialize();
    }

    private void Start()
    {
    }

    public void ResetSimulation()
    {
        Services.Get<GroupPhaseService>().SetupMatches();
    }

    private void Update()
    {
        if (!_bootstrapper.IsInitialized) return;
    }

    private void FixedUpdate()
    {
        if (!_bootstrapper.IsInitialized) return;
    }

    private void LateUpdate()
    {
        if (!_bootstrapper.IsInitialized) return;
    }
}