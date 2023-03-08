using UnityEngine;

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
        InstantiateManagers();

        ActivateAllServices();
        _isInitialized = true;
    }


    protected abstract void InstantiateManagers();
    protected abstract void InstantiateSettings();

    protected abstract void ActivateAllServices();
    protected abstract void ActivateAllManagers();
}