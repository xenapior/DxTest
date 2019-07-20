using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Windows;
using System.Windows.Forms;

namespace TestDxConsole
{
	public class RenderformInput : IUserInput,IDisposable
	{
		public event UInputVV OnRequestExit;
		public event UInputVC OnChar;
		private RenderForm form;

		public RenderformInput(RenderForm form)
		{
			this.form = form;
			form.FormClosing += Form_FormClosing;
			form.KeyPress += Form_KeyPress;
		}

		private void Form_KeyPress(object sender, KeyPressEventArgs e)
		{
			OnChar?.Invoke(e.KeyChar);
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				Debug.WriteLine("User is closing RenderForm");
				if (OnRequestExit != null) e.Cancel = !OnRequestExit();
			}
		}

		public void Dispose()
		{
			form.FormClosing -= Form_FormClosing;
			form.KeyPress -= Form_KeyPress;
		}
	}
}
