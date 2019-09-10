using System;

namespace BlakeBot.Web.Api.Services
{
	public class SpaceMover : ISpaceMover
	{
		private const int Threshold = 60;

		private readonly IRandomiser _randomiser;

		public SpaceMover(IRandomiser randomiser)
		{
			_randomiser = randomiser;
		}

		public string MoveSpaces(string input)
		{
			var arr = input.ToCharArray();
			int currentIndex = 0;

			while (currentIndex < arr.Length - 1
			       && (currentIndex = Array.IndexOf(arr, ' ', currentIndex + 1)) != -1)
			{
				if (_randomiser.Next(100) > Threshold)
				{
					if (currentIndex > 0 && _randomiser.Next(100) < 50)
					{
						char a = arr[currentIndex];
						arr[currentIndex] = arr[currentIndex - 1];
						arr[currentIndex - 1] = a;
					}
					else
					{
						char a = arr[currentIndex];
						arr[currentIndex] = arr[currentIndex + 1];
						arr[currentIndex + 1] = a;
						currentIndex++;
					}
				}
			}

			return new string(arr);
		}
	}
}
