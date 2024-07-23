using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HomestayService : IHomestayService
    {
        private readonly IHomestay iHomestayRepository;
        public HomestayService()
        {
            iHomestayRepository = new HomestayRepository();
        }
   

        public List<Homestay> GetHomestays()
        {
            return iHomestayRepository.GetHomestays();
        }

        public void UpdateHomestay(Homestay homestay)
        {
            iHomestayRepository.UpdateHomestay(homestay);
        }
    }
}

