using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;

using con=System.Console;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

namespace TestDxConsole
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			PrintAdapterInfo();

			RenderForm fm=new RenderForm();
			
			SwapChainDescription swcDesc = new SwapChainDescription
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(fm.ClientSize.Width, fm.ClientSize.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
				IsWindowed = true,
				OutputHandle = fm.Handle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Discard,
				Usage = Usage.RenderTargetOutput
			};
			Device device;
			SwapChain swapChain;
			Device.CreateWithSwapChain(DriverType.Hardware,DeviceCreationFlags.None,swcDesc,out device,out swapChain);
			var dc = device.ImmediateContext;
			var factory = swapChain.GetParent<Factory>();
			factory.MakeWindowAssociation(fm.Handle,WindowAssociationFlags.IgnoreAll);

			var backBuffer = Resource.FromSwapChain<Texture2D>(swapChain,0);
			var targetView=new RenderTargetView(device,backBuffer);

			RenderLoop.Run(fm, () =>
			{
				dc.ClearRenderTargetView(targetView,Color.AliceBlue);
				swapChain.Present(0, PresentFlags.None);
			});

			con.WriteLine("Program end");
			con.ReadKey();

			targetView.Dispose();
			backBuffer.Dispose();
			dc.Flush(); 
			dc.Dispose();
			swapChain.Dispose();
			device.Dispose();
			factory.Dispose();
			fm.Dispose();
		}

		private static void PrintAdapterInfo()
		{
			Factory1 fab = new Factory1();
			Adapter1[] nAdapters = fab.Adapters1;
			for (int i = 0; i < nAdapters.Length; i++)
			{
				Adapter1 adapter = fab.Adapters1[i];
				con.WriteLine(adapter.Description1.Description);
				con.WriteLine(adapter.Description1.DedicatedVideoMemory/1024/1024+"MB video memory.");
				Output[] displays = adapter.Outputs;
				foreach (var display in displays)
				{
					con.WriteLine(display.Description.DeviceName);
					display.Dispose();
				}
				con.WriteLine($"Adapter {Device.GetSupportedFeatureLevel(adapter).ToString()}");
				adapter.Dispose();
			}

			fab.Dispose();
		}
	}
}
