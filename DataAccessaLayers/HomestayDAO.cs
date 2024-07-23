using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessaLayers
{
    public class HomestayDAO
    {
        private static HomestayDAO instance;
        private static readonly object locker = new object();

        public static HomestayDAO Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new HomestayDAO();
                    }
                    return instance;
                }
            }
        }

        private HomestayDAO() { }

        public List<Homestay> GetCustomers()
        {
            List<Homestay> homestays;
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    homestays = applicationDbContext.Homestays.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return homestays;
        }

        public void Update(Homestay homestay)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Entry<Homestay>(homestay).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }     
    }
}
