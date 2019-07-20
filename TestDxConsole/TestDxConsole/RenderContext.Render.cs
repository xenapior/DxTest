using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace TestDxConsole
{
	public partial class RenderContext
	{
		private RenderTargetView backView;

		public void PrepareResources()
		{
			InitBackViews();
		}

		public void RenderProc()
		{
			dc.ClearRenderTargetView(backView, logicData.StateVar ? Color.Green : Color.Red);
			swapChain.Present(0, PresentFlags.None);
		}

		private void InitBackViews()
		{
			Texture2D backBuffer = swapChain.GetBackBuffer<Texture2D>(0);
			backView = new RenderTargetView(device, backBuffer);
			AutoCollector.Collect(backView);
			backBuffer.Dispose();
		}
	}
}