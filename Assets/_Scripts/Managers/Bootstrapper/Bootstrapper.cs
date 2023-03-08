using System.Collections.Generic;
using System.Linq;
using _Scripts.Game.Settings;
using _Scripts.Managers;
using UnityEngine;

public class Bootstrapper : BaseBootstrapper
{
    [Header("Settings")] 
    public GameSettings GameSettings;
    public UISettings UISettings;

    [Header("Managers")] 
    public GroupPhaseManager GroupPhaseManager;

    protected override void InstantiateManagers()
    {
        TryAddPrefab<GroupPhaseManager>(GroupPhaseManager);
        
        List<BaseManager> managers = Services.GetAll<BaseManager>().OrderBy(m => m.Priority).ToList();
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
        foreach (BaseManager m in Services.GetAll<BaseManager>())
        {
            m.gameObject.SetActive(true);
        }
    }
}