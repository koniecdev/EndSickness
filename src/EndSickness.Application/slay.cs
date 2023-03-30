using EndSickness.Application.Common.Interfaces.FileStorage;
using System.Diagnostics;

namespace EndSickness.Application
{
    public class Slay
    {
        private readonly IEnumerable<ICustomFileSave> _customFiles;
        public Slay(IEnumerable<ICustomFileSave> customFiles)
        {
            _customFiles = customFiles;
        }
        public void SayHello()
        {
            foreach(var f in _customFiles)
            {
                Debug.WriteLine(f.ToString());
            }
        }
    }
}
