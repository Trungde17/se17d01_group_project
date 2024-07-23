using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public  interface IHomestayService
    {
        List<Homestay> GetHomestays();
       
        void UpdateHomestay(Homestay homestay);

    }
}
