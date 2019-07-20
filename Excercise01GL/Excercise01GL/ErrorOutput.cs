using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excercise01GL
{
	static class ErrorOutput
	{
		public static void Error(string text)
		{
			MessageBox.Show(text);
		}
	}
}
