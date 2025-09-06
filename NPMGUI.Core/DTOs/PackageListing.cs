using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPMGUI.Core.DTOs
{
    public class PackageListing
    {
        public Dictionary<string, string>? Dependencies { get; set; }
        public Dictionary<string, string>? DevDependencies { get; set; }
    }
}
