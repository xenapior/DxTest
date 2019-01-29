using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestDxConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDxConsole.Tests
{
	[TestClass()]
	public class ProgramTests
	{
		[TestMethod()]
		public void Return2Test()
		{
			Assert.AreEqual(2,Program.Return2());
		}
	}
}