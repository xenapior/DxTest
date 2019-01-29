using System;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.Windows;

using con=System.Console;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

namespace TestDxConsole
{
	public class Program
	{
		private static readonly DisposeCollector AutoCollector=new DisposeCollector();

		[STAThread]
		static void Main(string[] args)
		{
			PrintAdapterInfo();

			RenderForm fm=new RenderForm();
			AutoCollector.Collect(fm);

			Device device;
			DeviceContext dc;
			RenderTargetView targetView;
			SwapChain swapChain = InitWindow(fm, out device, out dc, out targetView);

			RenderLoop.Run(fm, () =>
			{
				dc.ClearRenderTargetView(targetView,Color.Black);
				RenderScene(dc);
				swapChain.Present(0, PresentFlags.None);
			});

			dc.Flush(); 
			con.WriteLine("Program end");
			con.ReadKey();

			AutoCollector.DisposeAndClear();
		}

		private static void RenderScene(DeviceContext dc)
		{
			//todo: add rendering code
			return;
		}

		private static SwapChain InitWindow(RenderForm fm, out Device device, out DeviceContext dc,
			out RenderTargetView targetView)
		{
			SwapChainDescription swcDesc = new SwapChainDescription
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(fm.ClientSize.Width, fm.ClientSize.Height, new Rational(60, 1),
					Format.R8G8B8A8_UNorm),
				IsWindowed = true,
				OutputHandle = fm.Handle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Discard,
				Usage = Usage.RenderTargetOutput
			};
			SwapChain swapChain;
			Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swcDesc, out device, out swapChain);
			AutoCollector.Collect(device);
			AutoCollector.Collect(swapChain);

			dc = device.ImmediateContext;
			AutoCollector.Collect(dc);
			var factory = swapChain.GetParent<Factory>();
			AutoCollector.Collect(factory);
			factory.MakeWindowAssociation(fm.Handle, WindowAssociationFlags.IgnoreAll);

			var backBuffer = swapChain.GetBackBuffer<Texture2D>(0);
			AutoCollector.Collect(backBuffer);
			targetView = new RenderTargetView(device, backBuffer);
			AutoCollector.Collect(targetView);
			return swapChain;
		}

		public static void PrintAdapterInfo()
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
