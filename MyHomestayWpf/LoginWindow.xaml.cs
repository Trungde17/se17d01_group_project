using BusinessObjects;
using BusinessObjects.Models;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace MyHomestayWpf
{
    public partial class LoginWindow : Window
    {
        private readonly IUserService userService;
        private readonly string adminEmail;
        private readonly string adminPassword;
        public List<User> Users { get; set; }

        public LoginWindow()
        {
            userService = new UserService();
            Users = userService.GetUsers();
            InitializeComponent();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            adminEmail = configuration["AdminAccount:Email"];
            adminPassword = configuration["AdminAccount:Password"];
        }

        private void Login_btn(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string error_text = "";

            if (!email.Equals("") && !password.Equals(""))
            {
                if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    if (email.Equals(adminEmail) && password.Equals(adminPassword))
                    {
                        AdminPage adminPage = new AdminPage();
                        this.Close();
                        adminPage.Show();
                    }
                    else
                    {
                        User user = Users.FirstOrDefault(u => u.Email.Equals(email.Trim()));
                        if (user != null)
                        {
                            if (user.PasswordHash.Equals(password))
                            {
                                MainWindow mainWindow = new MainWindow();
                                this.Close();
                                mainWindow.Show();
                            }
                            else error_text = "Incorrect password";
                        }
                        else error_text = "Account does not exist!";
                    }
                }
                else error_text = "It's not a valid email!";
            }
            else error_text = "Email and password cannot be blank!";
            Error.Text = error_text;
            Error.Visibility = Visibility.Visible;
        }

        private void Close_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
