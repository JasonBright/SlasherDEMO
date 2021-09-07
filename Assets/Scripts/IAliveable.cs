
public interface IAliveable
{
    float Health { get; }
    float MaxHealth { get; }
    bool IsAlive { get; }

    void Damage(float damage);
} 

