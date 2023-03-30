namespace EndSickness.Application.Common.Interfaces.FileStorage;

public interface ICustomFileDecorator : ICustomFile
{
    public ICustomFile CustomFile { get; }
}