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

            var muddledWords = words.Select(x => {
				var muddled = _wordMuddler.MuddleWord(x);
				var lowercased = _capitalisationRemover.RemoveCapitalisation(muddled);
				return lowercased;
			});

            return string.Join(' ', muddledWords);
        }
    }
}
