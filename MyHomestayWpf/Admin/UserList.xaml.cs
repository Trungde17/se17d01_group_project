using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects.Models;
using Services;

namespace MyHomestayWpf
{
    public partial class UserList : Window
    {
        private readonly IUserService userService;
        private int editingUserId = -1;

        public UserList()
        {
            userService = new UserService();
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<User> users = userService.GetUsers();
            UsersDataGrid.ItemsSource = users;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                UserName = UserNameTextBox.Text,
                PasswordHash = PasswordTextBox.Text,
                FullName = FullNameTextBox.Text,
                Email = EmailTextBox.Text,
                Phone = PhoneTextBox.Text
            };

            userService.AddUser(user);
            MessageBox.Show("User added successfully.");
            ClearForm();
            LoadData();
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (editingUserId == -1)
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            var user = new User
            {
                UserId = editingUserId,
                UserName = UserNameTextBox.Text,
                PasswordHash = PasswordTextBox.Text,
                FullName = FullNameTextBox.Text,
                Email = EmailTextBox.Text,
                Phone = PhoneTextBox.Text
            };

            userService.UpdateUser(user);
            MessageBox.Show("User updated successfully.");
            ClearForm();
            LoadData();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            int userId = (int)((Button)sender).Tag;
            var user = userService.GetUserById(userId);
            if (user != null)
            {
                UserNameTextBox.Text = user.UserName;
                PasswordTextBox.Text = user.PasswordHash;
                FullNameTextBox.Text = user.FullName;
                EmailTextBox.Text = user.Email;
                PhoneTextBox.Text = user.Phone;
                editingUserId = user.UserId;
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            int userId = (int)((Button)sender).Tag;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                userService.DeleteUser(userId);
                LoadData();
                MessageBox.Show("User deleted successfully.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            this.Close();
            adminPage.Show();
        }

        private void ClearForm()
        {
            UserNameTextBox.Text = "";
            PasswordTextBox.Text = "";
            FullNameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            editingUserId = -1;
        }
    }
}
