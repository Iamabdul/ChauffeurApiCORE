using System.Threading.Tasks;

namespace ChauffeurApiCORE.Commands
{
	public class TestCommand : ITestCommand
	{
		public Task<string> Execute()
		{
			return Task.FromResult(nameof(TestCommand));
		}
	}

	public interface ITestCommand
	{
		Task<string> Execute();
	}
}
