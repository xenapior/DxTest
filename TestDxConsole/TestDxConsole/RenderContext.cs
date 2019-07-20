using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;

namespace TestDxConsole
{
	/// <summary>
	/// DX resource creation
	/// </summary>
	public partial class RenderContext : IDisposable
	{
		private readonly DisposeCollector AutoCollector = new DisposeCollector();
		private readonly LogicBasic logicData;	//todo: replace to interface with neccessary properties

		private SwapChain swapChain;
		private Device device;
		private DeviceContext dc;

		public RenderContext(Control window, LogicBasic logicModule)
		{
			logicData = logicModule;
			InitDevice(window);
			PrepareResources();
		}
		
		private void InitDevice(Control window)
		{
			SwapChainDescription swcDesc = new SwapChainDescription
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(window.ClientSize.Width, window.ClientSize.Height, new Rational(60, 1),
					Format.R8G8B8A8_UNorm),
				IsWindowed = true,
				OutputHandle = window.Handle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Discard,
				Usage = Usage.RenderTargetOutput
			};
			Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swcDesc, out device, out swapChain);
			AutoCollector.Collect(device);
			AutoCollector.Collect(swapChain);
			dc = device.ImmediateContext;
			AutoCollector.Collect(dc);
			var factory = swapChain.GetParent<Factory>();
			factory.MakeWindowAssociation(window.Handle, WindowAssociationFlags.IgnoreAll);
			factory.Dispose();
		}

		public void Dispose()
		{
			AutoCollector.DisposeAndClear();
		}
	}
}
