using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DGVTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void addRow_Click(object sender, EventArgs e)
		{
			dgv.ColumnCount = 10;
			for (int i = 0; i < dgv.ColumnCount; i++)
			{
				dgv.Columns[i].Name = "Col "+i;
			}

			dgv.Rows.Add(1,2,3,"s",5);
			dgv.Rows[0].HeaderCell.Value = "Row1";
			
		}

		private void dgv_MouseEnter(object sender, EventArgs e)
		{
			tTip.Active = true;
		}

		private void dgv_MouseMove(object sender, MouseEventArgs e)
		{
			tTip.Show("In DGV",dgv,e.X/10*10,e.Y/10*10);
		}

		private void dgv_MouseLeave(object sender, EventArgs e)
		{
			tTip.Hide(dgv);
		}
	}
}
