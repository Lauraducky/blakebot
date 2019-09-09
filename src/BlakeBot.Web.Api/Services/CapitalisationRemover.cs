using System;

namespace BlakeBot.Web.Api.Services {
    public class CapitalisationRemover : ICapitalisationRemover {
        public string RemoveCapitalisation(string input) {
            //todo selectively/randomly remove capitalisation
			return input.ToLower();
        }
    }
}
