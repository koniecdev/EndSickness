namespace IsolatedEnvironment;

internal abstract class CustomFileDecorator : CustomFile
{
    public CustomFileDecorator(ICustomFile customFile)
    {
        CustomFile = customFile;
    }
    protected ICustomFile CustomFile { get; set; }
}