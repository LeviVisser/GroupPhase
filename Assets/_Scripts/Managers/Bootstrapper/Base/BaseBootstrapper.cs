using UnityEngine;

/// <summary>
/// Abstract base implementation of the bootstrapper
/// </summary>
public abstract class BaseBootstrapper : MonoBehaviour
{
    private static bool _isInitialized;

    public bool IsInitialized
    {
        get => _isInitialized;
    }

    public void Initialize()
    {
        if (_isInitialized)
        {
            Destroy(gameObject);
            return;
        }

        InstantiateSettings();
        InstantiateServices();

        ActivateAllServices();
        _isInitialized = true;
    }


    protected abstract void InstantiateServices();
    protected abstract void InstantiateSettings();

    protected abstract void ActivateAllServices();
    protected abstract void ActivateAllManagers();
}