namespace EndSickness.Infrastructure.AbstractFactory;

public interface IAbstractFactory<T>
{
    T Create();
}
