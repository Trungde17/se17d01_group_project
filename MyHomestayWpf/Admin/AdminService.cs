using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHomestayWpf
{
    public class AdminService
    {
        private readonly string adminEmail;
        private readonly string adminPassword;

        public AdminService()
        {
            adminEmail = ConfigurationManager.AppSettings["AdminAccount:Email"];
            adminPassword = ConfigurationManager.AppSettings["AdminAccount:Password"];
        }

        public bool Login(string email, string password)
        {
            return email == adminEmail && password == adminPassword;
        }
    }
}
