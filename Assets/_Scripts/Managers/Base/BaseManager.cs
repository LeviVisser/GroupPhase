using UnityEngine;

/// <summary>
/// Base class for all manager classes
/// When creating a new manager, you should add it to the bootstrapper for initialization and service locator assignment
/// </summary>
public abstract class BaseManager : GameBehaviour
{
    public uint Priority;
}