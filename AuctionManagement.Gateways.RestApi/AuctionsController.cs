using System;
using AuctionManagement.Application.Contracts;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagement.Gateways.RestApi
{
    [Route("api/[controller]")]
    public class AuctionsController : Controller
    {
        private ICommandBus _bus;
        public AuctionsController(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public void Post([FromBody]OpenAuctionCommand command)
        {
            _bus.Dispatch(command);
        }
    }
}
