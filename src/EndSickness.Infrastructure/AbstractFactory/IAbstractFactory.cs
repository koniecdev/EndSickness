namespace EndSickness.Infrastructure.AbstractFactory;

public interface IAbstractFactory<T>
{
    public T Create();
}
