using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XPCTView;

namespace XPCTViewTest
{
	class Program
	{
		static void Main(string[] args)
		{
			MatlabInterface.InitMainWindow();
			int n = 0;
			while (!MatlabInterface.IsSettingReady())
			{
				Thread.Sleep(100);
				Console.WriteLine("Waiting."+(n++));
			}

			var s = MatlabInterface.GetSetting();
			Console.WriteLine(s.str);
			Console.WriteLine("Finished");
			Console.ReadKey();
		}
	}
}
