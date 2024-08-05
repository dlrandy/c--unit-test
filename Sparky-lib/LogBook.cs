using System;
namespace Sparky_lib
{
	public interface ILogBook
	{
		void Message(string message);
	}

	public class LogBook : ILogBook
	{

        public void Message(string message)
        {
			Console.WriteLine(message);
        }
    }
}

