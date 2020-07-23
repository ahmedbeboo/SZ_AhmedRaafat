using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Net_AhmedRaafat.BL;

namespace Net_AhmedRaafat.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;
        private IHttpContextAccessor _httpContextAccessor;

        public MessageController(IHubContext<NotifyHub, ITypedHubClient> hubContext, IHttpContextAccessor httpContextAccessor)
        {
            _hubContext = hubContext;
            _httpContextAccessor = httpContextAccessor;
        }

        

        [HttpPost]
        public string Post([FromBody]Message msg)
        {
            string retMessage;
            try
            {
                var user= _httpContextAccessor.HttpContext.Request.HttpContext.User.Identity.Name;


                _hubContext.Clients.All.BroadcastMessage(msg.Type, msg.Payload);


                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }
            return retMessage;
        }
    }
}
