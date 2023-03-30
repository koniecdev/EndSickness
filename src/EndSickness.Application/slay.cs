using EndSickness.Application.Common.Interfaces.FileStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndSickness.Application
{
    public class Slay
    {
        private readonly IEnumerable<ICustomFile> _customFiles;
        public Slay(IEnumerable<ICustomFile> customFiles)
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
