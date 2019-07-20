using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PlugInInterface;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			// The code provided will print ‘Hello World’ to the console.
			// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
			Assembly pi;
			try
			{
				pi = Assembly.LoadFrom(@".\PMultiplier.dll");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Console.ReadKey();
				return;
			}

			IPlugIn a;
			try
			{
				a = pi.CreateInstance("PMultiplier.Multiplier") as IPlugIn;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Console.ReadKey();

				return;
			}

			Console.WriteLine(a.GetName());
			Console.WriteLine(a.Calculate(1,2));
			Console.ReadKey();
			// Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
		}
	}

	class Adder : IPlugIn
	{
		public string GetName()
		{
			return "Adder V1";
		}

		public int Calculate(int a, int b)
		{
			return a + b;
		}
	}
}
