using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XPCTView
{
	public static class MatlabInterface
	{
		private static volatile MainForm form;

		/// <summary>
		/// Start a Winform window in a new thread.
		/// </summary>
		public static void InitMainWindow()
		{
			Thread t=new Thread(uiThread);
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
			while (true)
			{
				if (form != null)
					break;
				Thread.Sleep(0);
			}
		}

		private static void uiThread()
		{
			// check duplicate main window
			if (form != null)
				return;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			form=new MainForm();
			Application.Run(form);
		}

		/// <summary>
		/// Query ready-state of all settings for next steps.
		/// If ready, a subsequent call to GetSetting() will retrieve parameters.
		/// </summary>
		public static bool IsSettingReady()
		{
			return form.SettingReady;
		}

		public static Settings GetSetting()
		{
			return form.Setting;
		}
	}
}
