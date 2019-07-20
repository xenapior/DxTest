using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDxConsole
{
	public class ConsoleInput : IUserInput
	{
		public event UInputVV OnRequestExit;
		public event UInputVC OnChar;

		
	}
}
