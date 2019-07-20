using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugInInterface
{
    public interface IPlugIn
    {
	    string GetName();
	    int Calculate(int a, int b);
    }
}
