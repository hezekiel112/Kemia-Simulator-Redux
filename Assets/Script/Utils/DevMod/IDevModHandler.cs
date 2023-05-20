/// <summary>
/// Interface de fonctions DevMod
/// </summary>
public interface IDevModHandler
{
    public virtual void OnStart(bool active) { }
    public virtual void OnUpdate(bool active) { }
}