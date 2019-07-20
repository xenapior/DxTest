using System;
using System.Diagnostics;
using System.Windows.Forms;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Windows;

namespace TestDxConsole
{
	public class Program
	{
		private static readonly DisposeCollector AutoCollector=new DisposeCollector();

		[STAThread]
		static void Main(string[] args)
		{
//			PrintAdapterInfo();

			RenderForm fm=new RenderForm();
			AutoCollector.Collect(fm);
			RenderformInput formInput=new RenderformInput(fm);	//TODO: refact to factory creation
			AutoCollector.Collect(formInput);
			LogicBasic logicModule=new LogicBasic(formInput);
			RenderContext mainRC = new RenderContext(fm, logicModule);
			AutoCollector.Collect(mainRC);
			
			RenderLoop.Run(fm,mainRC.RenderProc);

			AutoCollector.DisposeAndClear();
		}

		public static void PrintAdapterInfo()
		{
			Factory1 factory1 = new Factory1();
			Adapter1[] nAdapters = factory1.Adapters1;
			for (int i = 0; i < nAdapters.Length; i++)
			{
				Adapter1 adapter = factory1.Adapters1[i];
				Debug.WriteLine(adapter.Description1.Description);
				Debug.WriteLine(adapter.Description1.DedicatedVideoMemory/1024/1024+"MB video memory.");
//				Output[] displays = adapter.Outputs;
//				foreach (var display in displays)
//				{
//					Debug.WriteLine(display.Description.DeviceName);
//					display.Dispose();
//				}
				Debug.WriteLine($"Adapter {SharpDX.Direct3D11.Device.GetSupportedFeatureLevel(adapter).ToString()}");
				adapter.Dispose();
			}
			factory1.Dispose();
		}
	}
}
