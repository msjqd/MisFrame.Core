using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisFrame.Core.IService;
using MisFrame.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFrame.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        IAdvertisementService service;
        public AdvertisementController(IAdvertisementService service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<List<Advertisement>> Get(int id)
        {
            // IAdvertisementService service = new AdvertisementService();
            return await service.Query(d => d.Id == id);
        }

        [HttpGet()]
        public Advertisement Get()
        {
            return service.QueryTest(1);
        }

        [HttpGet("{id}/{y}")]
        public async Task Get(int id, int y)
        {
            await service.QueryTest1();
        }

    }
}
