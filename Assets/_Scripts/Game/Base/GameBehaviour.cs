using UnityEngine;

/// <summary>
/// Base game behaviour class for all in scene objects which need update functions
/// </summary>
public abstract class GameBehaviour : MonoBehaviour
{
    public virtual Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public virtual Vector3 LocalPosition
    {
        get => transform.localPosition;
        set => transform.localPosition = value;
    }

    public virtual Quaternion Rotation
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }

    public virtual Quaternion LocalRotation
    {
        get => transform.localRotation;
        set => transform.localRotation = value;
    }

    protected GameSettings GameSettings;
    protected UISettings UISettings;

    internal void Initialize()
    {
        GameSettings = Services.Get<GameSettings>();
        UISettings = Services.Get<UISettings>();
        OnInitialize();
    }

    /// <summary>
    /// When overriding HandleUpdate, do not forget to call base.HandleUpdate first!
    /// </summary>
    internal virtual void HandleUpdate()
    {
       
    }

    /// <summary>
    /// When overriding HandleFixedUpdate, do not forget to call base.HandleFixedUpdate first!
    /// </summary>
    internal virtual void HandleFixedUpdate()
    {
    }

    /// <summary>
    /// When overriding HandleLateUpdate, do not forget to call base.HandleLateUpdate first!
    /// </summary>
    internal virtual void HandleLateUpdate()
    {
    }

    /// <summary>
    /// Called from initialization
    /// </summary>
    protected abstract void OnInitialize();
}