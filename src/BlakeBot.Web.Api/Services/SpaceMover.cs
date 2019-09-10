using System;
using BlakeBot.Web.Api.Configuration;
using Microsoft.Extensions.Options;

namespace BlakeBot.Web.Api.Services
{
	public class SpaceMover : ISpaceMover
	{
		private readonly IRandomiser _randomiser;
		private readonly Thresholds _thresholds;

		public SpaceMover(IRandomiser randomiser, IOptions<Thresholds> thresholds)
		{
			_randomiser = randomiser;
			_thresholds = thresholds.Value;
		}

		public string MoveSpaces(string input)
		{
			var arr = input.ToCharArray();
			int currentIndex = 0;

			while (currentIndex < arr.Length - 1
			       && (currentIndex = Array.IndexOf(arr, ' ', currentIndex + 1)) != -1)
			{
				if (_randomiser.Next(100) <= _thresholds.ChanceToMoveSpaces)
				{
					if ((char.IsLetterOrDigit(arr[currentIndex - 1]) || char.IsPunctuation(arr[currentIndex - 1]))
					    && currentIndex > 0 && _randomiser.Next(100) <= _thresholds.ChanceToMoveSpacesLeft)
					{
						var a = arr[currentIndex];
						arr[currentIndex] = arr[currentIndex - 1];
						arr[currentIndex - 1] = a;
					}
					else if (char.IsLetterOrDigit(arr[currentIndex + 1]) || char.IsPunctuation(arr[currentIndex + 1]))
					{
						var a = arr[currentIndex];
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
