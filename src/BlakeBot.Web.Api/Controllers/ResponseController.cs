using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using BlakeBot.Web.Api.Services;

namespace BlakeBot.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IPhraseMuddler _phraseMuddler;

        public ResponseController(IPhraseMuddler phraseMuddler) {
            _phraseMuddler = phraseMuddler;
        }

        // GET api/response/room
        [HttpGet("room")]
        public ActionResult<string> Get()
        {
            return "room";
        }

        // GET api/response/direct
        [HttpGet("direct")]
        public ActionResult<string> Get(int id)
        {
            return "direct";
        }
    }
}
