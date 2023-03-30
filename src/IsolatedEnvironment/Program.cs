using IsolatedEnvironment;
using System.Text;

ICustomFile FileService = new CustomFileService();
FileService.WriteAllBytes("E:\\dupa.txt", Encoding.ASCII.GetBytes("Hello world"));
FileService.WriteAllBytes("E:\\dupa.txt", Encoding.ASCII.GetBytes("Ok business logic working"));

FileService = new PreventOverridingCustomFileDecorator(FileService);
FileService.WriteAllBytes("E:\\dupa.txt", Encoding.ASCII.GetBytes("Ok renaming business logic working"));
FileService.WriteAllBytes("E:\\dupa.txt", Encoding.ASCII.GetBytes("Ok renaming2 business logic working"));

//Todo: Try to make it an interface, so it can be used with dependency injection