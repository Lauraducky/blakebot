using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlakeBot.Web.Api.Configuration
{
	public class Thresholds
	{
		public int ChanceToSwapCharacters { get; set; }
		public int ChanceToRemoveCapitals { get; set; }
		public int ChanceToMoveSpaces { get; set; }
		public int ChanceToMoveSpacesLeft { get; set; }
		public int ChanceToRemovePunctuation { get; set; }
	}
}
