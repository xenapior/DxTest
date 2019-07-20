using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XPCTView
{
	internal partial class MainForm : Form
	{
		public volatile bool SettingReady;
		public Settings Setting = new Settings();

		public MainForm()
		{
			InitializeComponent();
		}

		private void btn1_Click(object sender, EventArgs e)
		{
			Setting.str = textBox1.Text;
			SettingReady = true;
		}
	}
}
