using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisFrame.Core.IService;
using MisFrame.Core.Model;
using MisFrame.Core.Service;
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

        [HttpGet]
        public async Task<List<Advertisement>> Get(int id)
        {
            IAdvertisementService service = new AdvertisementService();
            return await service.Query(d => d.Id == id);
        }
    }
}
