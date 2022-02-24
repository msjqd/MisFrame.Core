using MisFrame.Core.IRepository;
using MisFrame.Core.IService;
using MisFrame.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisFrame.Core.Service
{
    public class AdvertisementService:BaseServices<Advertisement>, IAdvertisementService
    {
        IAdvertisementRepo dal;
        public AdvertisementService(IAdvertisementRepo repo)
        {
            dal = repo;
            baseDal = repo;
        }
    }
}
