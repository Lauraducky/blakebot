using System;
using System.Linq;

namespace BlakeBot.Web.Api.Services {
    public class PhraseMuddler : IPhraseMuddler {
        private readonly IWordMuddler _wordMuddler;
		private readonly ICapitalisationRemover _capitalisationRemover;

        public PhraseMuddler(IWordMuddler wordMuddler, ICapitalisationRemover capitalisationRemover) {
            _wordMuddler = wordMuddler;
			_capitalisationRemover = capitalisationRemover;
        }

        public string MuddlePhrase(string input) {
            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var muddled = words.Select(x => _wordMuddler.MuddleWord(x));

            var full = string.Join(' ', muddled);
			var lowercase = _capitalisationRemover.RemoveCapitalisation(full);
			return lowercase;
        }
    }
}
