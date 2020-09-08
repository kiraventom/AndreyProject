using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AuthorizationModel;
using Microsoft.EntityFrameworkCore;

namespace AndreyProject
{
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
            authorizationContext = new AuthorizationContext();
            this.Closed += this.AdministratorWindow_Closed;
            this.Loaded += this.AdministratorWindow_Loaded;
            this.UsersDG.AutoGeneratingColumn += this.UsersDG_AutoGeneratingColumn;
            this.Closing += this.AdministratorWindow_Closing;
        }

        private readonly AuthorizationContext authorizationContext;

        private void AdministratorWindow_Closed(object sender, EventArgs e)
        {
            authorizationContext.Dispose();
        }

        private void AdministratorWindow_Loaded(object sender, RoutedEventArgs e) 
        {
            authorizationContext.Users.Load();
            this.UsersDG.ItemsSource = authorizationContext.Users.Local.ToObservableCollection();
        }

        private void UsersDG_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString().Equals("id", StringComparison.OrdinalIgnoreCase))
            {
                e.Column.IsReadOnly = true;
            }
        }

        private void AdministratorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dialogResult = MessageBox.Show("Save changes?", "Confirm", MessageBoxButton.YesNoCancel);
            switch (dialogResult)
            {
                case MessageBoxResult.Yes:
                    this.authorizationContext.SaveChanges();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    }
}
