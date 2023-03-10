
/// <summary>
/// Base class for all service classes
/// When creating a new service, you should add it to the bootstrapper for initialization and service locator assignment
/// </summary>
public abstract class BaseService : GameBehaviour
{
    public uint Priority;
}