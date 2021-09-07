

public interface IContainer
{
    void InjectTo(object target);
    void AddDependency<T>(object dependency) where T : class;
}
