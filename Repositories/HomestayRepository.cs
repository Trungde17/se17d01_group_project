using BusinessObjects.Models;
using DataAccessaLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public  class HomestayRepository: IHomestay
    {


     

      







        public void UpdateHomestay(Homestay homestay) => HomestayDAO.Instance.Update(homestay);





        public List<Homestay> GetHomestays() => HomestayDAO.Instance.GetCustomers();


    }
}
