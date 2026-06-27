public interface IPoolable
{
    bool IsActive { get; }
    void Activate();
    void Deactivate();
}