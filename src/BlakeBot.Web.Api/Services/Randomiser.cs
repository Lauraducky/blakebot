using System;

namespace BlakeBot.Web.Api.Services
{
	public class Randomiser : IRandomiser
	{
		private readonly Random _random;

		public Randomiser()
		{
			_random = new Random();
		}

		public int Next(int max)
		{
			return _random.Next(max);
		}
	}
}
