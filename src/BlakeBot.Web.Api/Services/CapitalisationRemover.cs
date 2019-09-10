namespace BlakeBot.Web.Api.Services
{
	public class CapitalisationRemover : ICapitalisationRemover
	{
		private const int Threshold = 25;

		private readonly IRandomiser _randomiser;

		public CapitalisationRemover(IRandomiser randomiser)
		{
			_randomiser = randomiser;
		}

		public string RemoveCapitalisation(string input)
		{
			var num = _randomiser.Next(100);

			if (num > Threshold)
			{
				return input.ToLower();
			}

			return input;
		}
	}
}
