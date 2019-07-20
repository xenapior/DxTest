namespace TestDxConsole
{
	public delegate bool UInputVV();
	public delegate void UInputVC(char Character);

	public interface IUserInput
	{
		event UInputVV OnRequestExit;
		event UInputVC OnChar;
	}
}