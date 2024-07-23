using BusinessObjects.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace MyHomestayWpf
{
    public partial class UserList : Window
    {
        private readonly IUserService userService;

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
            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                User newUser = new User
                {
                    UserName = UserNameTextBox.Text,
                    PasswordHash = PasswordTextBox.Text,
                    FullName = FullNameTextBox.Text,
                    Email = EmailTextBox.Text,
                    Phone = PhoneTextBox.Text
                };

                userService.AddUser(newUser);
                LoadData();
                ClearTextBoxes();
                MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            try
            {
                if (UsersDataGrid.SelectedItem is User selectedUser)
                {
                    selectedUser.UserName = UserNameTextBox.Text;
                    selectedUser.PasswordHash = PasswordTextBox.Text;
                    selectedUser.FullName = FullNameTextBox.Text;
                    selectedUser.Email = EmailTextBox.Text;
                    selectedUser.Phone = PhoneTextBox.Text;

                    userService.UpdateUser(selectedUser);
                    LoadData();
                    ClearTextBoxes();
                    MessageBox.Show("User updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is int userId)
            {
                User user = userService.GetUserById(userId);
                if (user != null)
                {
                    UserNameTextBox.Text = user.UserName;
                    PasswordTextBox.Text = user.PasswordHash;
                    FullNameTextBox.Text = user.FullName;
                    EmailTextBox.Text = user.Email;
                    PhoneTextBox.Text = user.Phone;
                }
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is int userId)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        userService.DeleteUser(userId);
                        LoadData();
                        MessageBox.Show("User deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void SearchUser_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text.ToLower();
            List<User> filteredUsers = userService.GetUsers()
                .Where(user => user.FullName != null && user.FullName.ToLower().Contains(searchQuery))
                .ToList();
            UsersDataGrid.ItemsSource = filteredUsers;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            this.Close();
            adminPage.Show();
        }

        private void ClearTextBoxes()
        {
            UserNameTextBox.Clear();
            PasswordTextBox.Clear();
            FullNameTextBox.Clear();
            EmailTextBox.Clear();
            PhoneTextBox.Clear();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(UserNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordTextBox.Text) ||
                string.IsNullOrWhiteSpace(FullNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(FullNameTextBox.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Full Name can only contain letters and spaces.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(EmailTextBox.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email format is incorrect.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Regex.IsMatch(PhoneTextBox.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Phone number must be 10 digits.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
