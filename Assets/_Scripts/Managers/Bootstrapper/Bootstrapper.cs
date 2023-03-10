using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Bootstrapper class initializes all services and adds them to the service locator
/// </summary>
public class Bootstrapper : BaseBootstrapper
{
    [Header("Settings")] public GameSettings GameSettings;
    public UISettings UISettings;

    [Header("Game Services")] public GroupPhaseService groupPhaseService;

    [Header("UI Services")] 
    public GroupPhaseServiceView GroupPhaseServiceView;

    public GroupResultsView GroupResultsView;

    protected override void InstantiateServices()
    {
        TryAddPrefab<GroupResultsView>(GroupResultsView);
        TryAddPrefab<GroupPhaseServiceView>(GroupPhaseServiceView);
        TryAddPrefab<GroupPhaseService>(groupPhaseService);

        List<BaseService> managers = Services.GetAll<BaseService>().OrderBy(m => m.Priority).ToList();
        for (int i = 0; i < managers.Count; i++)
        {
            Debug.Log("Initialize manager: " + string.Join(", ", managers[i].GetType().ToString()));
            managers[i].Initialize();
        }
    }

    protected override void InstantiateSettings()
    {
        TryAddPrefab<GameSettings>(GameSettings);
        TryAddPrefab<UISettings>(UISettings);
    }

    private static void TryAddPrefab<T>(Behaviour behaviour) where T : Object
    {
        if (behaviour != null)
        {
            Services.AddPrefab<T>(behaviour);
        }
    }

    protected override void ActivateAllServices()
    {
        ActivateAllManagers();
    }

    protected override void ActivateAllManagers()
    {
        foreach (BaseService m in Services.GetAll<BaseService>())
        {
            m.gameObject.SetActive(true);
        }
    }
}