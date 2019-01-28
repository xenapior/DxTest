using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.Windows;

using con=System.Console;
using Device = SharpDX.Direct3D11.Device;

namespace TestDxConsole
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			con.WriteLine(GetAdapter0String());

			RenderForm fm=new RenderForm();
			
			SwapChainDescription swcDesc = new SwapChainDescription
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(fm.ClientSize.Width/2, fm.ClientSize.Height/2, new Rational(60, 1), Format.R8G8B8A8_UNorm),
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
			var fab = swapChain.GetParent<Factory>();
			fab.MakeWindowAssociation(fm.Handle,WindowAssociationFlags.IgnoreAll);


			con.ReadKey();
			dc.Flush(); 
			dc.Dispose();
			swapChain.Dispose();
			device.Dispose();
			fm.Dispose();
		}

		private static string GetAdapter0String()
		{
			Factory1 fab = new Factory1();
			Adapter1 adapter = fab.GetAdapter1(0);
			string description = adapter.Description1.Description;
			adapter.Dispose();
			fab.Dispose();
			return description;
		}
	}
}
