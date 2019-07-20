using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlugInInterface;

namespace PMultiplier
{
    public class Multiplier:IPlugIn
    {
	    public string GetName()
	    {
		    return "Multiplier V1";
	    }

	    public int Calculate(int a, int b)
	    {
		    return a*b;
	    }
    }
}
