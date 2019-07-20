using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDxConsole
{
	public class LogicBasic
	{
		public bool StateVar;

		private IUserInput inputSrc;

		public LogicBasic(IUserInput userInput)
		{
			inputSrc = userInput;
			inputSrc.OnRequestExit += RequestExit;
			inputSrc.OnChar += OnChar;
		}
		
		public bool RequestExit()
		{
			Debug.WriteLine(StateVar ? "Ready to quit" : "Not ready for quitting");
			return StateVar;
		}

		public void OnChar(char keyChar)
		{
			if (keyChar == 'a')
				StateVar = !StateVar;
		}
	}
}
