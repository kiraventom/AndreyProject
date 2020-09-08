using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AndreyProject
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            this.LoginBt.Click += this.LoginBt_Click;
        }

        private void LoginBt_Click(object sender, RoutedEventArgs e)
        {
            string login = this.LoginTB.Text;
            string password = this.PasswordPB.Password;
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return; // error handling
            }
            else
            {
                using var authContext = new AuthorizationModel.AuthorizationContext();
                authContext.Users.Load();
                var user = authContext.Users.Local.ToList()
                    .FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
                if (user is null)
                {
                    return; // error handling
                }
                else
                {
                    this.Hide();
                    switch (user.Level)
                    {
                        case AuthorizationModel.User.UserLevel.User:
                            // open user form
                            break;

                        case AuthorizationModel.User.UserLevel.Admin:
                            var adminWindow = new AdministratorWindow();
                            adminWindow.ShowDialog();
                            break;

                        default:
                            // error;
                            break;
                    }
                    this.Show();
                }
            }
        }
    }
}
