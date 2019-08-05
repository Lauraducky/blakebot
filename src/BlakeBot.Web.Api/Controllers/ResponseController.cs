using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlakeBot.Web.Api.Configuration;
using BlakeBot.Web.Api.Services;
using GlobalX.ChatBots.Core;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.WebexTeams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlakeBot.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IPhraseMuddler _phraseMuddler;
        private readonly IChatHelper _chatHelper;
        private readonly IWebhookHelper _webhookHelper;
        private readonly BotSettings _settings;

        public ResponseController(IPhraseMuddler phraseMuddler, IChatHelper chatHelper, IWebhookHelper webhookHelper, IOptions<BotSettings> settings)
        {
            _phraseMuddler = phraseMuddler;
            _chatHelper = chatHelper;
            _webhookHelper = webhookHelper;
            _settings = settings.Value;
        }

        // POST api/response/room
        [HttpPost("room")]
        public async Task<ActionResult<string>> Room()
        {
            string rawData;
            using (var reader = new StreamReader(Request.Body))
            {
                rawData = reader.ReadToEnd();
            }

            var message = await _webhookHelper.Webhooks.ProcessMessageWebhookCallbackAsync(rawData);

            if (message.MessageParts[0].MessageType != MessageType.PersonMention ||
                message.MessageParts[0].UserId != _settings.BotId)
            {
                return string.Empty;
            }

            var text = string.Join(null, message.MessageParts.Skip(1).Select(x => x.Text).ToArray()).Trim();
            var muddled = _phraseMuddler.MuddlePhrase(text);

            await _chatHelper.Messages.SendMessageAsync(new Message
            {
                Text = muddled,
                RoomId = message.RoomId
            });
            return muddled;
        }

        // POST api/response/direct
        [HttpPost("direct")]
        public async Task<ActionResult<string>> Direct()
        {
            string rawData;
            using (var reader = new StreamReader(Request.Body))
            {
                rawData = reader.ReadToEnd();
            }

            var message = await _webhookHelper.Webhooks.ProcessMessageWebhookCallbackAsync(rawData);
            var muddled = _phraseMuddler.MuddlePhrase(message.Text);
            await _chatHelper.Messages.SendMessageAsync(new Message
            {
                Text = muddled,
                RoomId = message.RoomId
            });
            return muddled;
        }

        // GET api/response/muddle/{word}
        [HttpGet("muddle/{phrase}")]
        public ActionResult<string> Get(string phrase)
        {
            return _phraseMuddler.MuddlePhrase(phrase);
        }
    }
}
