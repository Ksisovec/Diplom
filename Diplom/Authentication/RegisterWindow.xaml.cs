using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Diplom.Authentication
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    using Models;
    using Repository.Interfaces;
    using Repository.Implementation;
    public partial class RegisterWindow : Window
    {
        private readonly IUserRepository userReository;
        private readonly IWorkerRepository workerRepository;
        public RegisterWindow()
        {
            InitializeComponent();

            workerRepository = new WorkerRepository(new ApplicationContext());
            userReository = new UserRepository(new ApplicationContext());
            

            surnameComboBox.ItemsSource = workerRepository.GetListFromCache().Select(p => p.Surname);
        }


        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (surnameComboBox.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            if (passwordBox.Password != passwordChekBox.Password &&
                passwordBox.Password.Length <= 4)
            {
                MessageBox.Show("error pass");
                return;
            }
            User user = new User();
            user.Password = passwordBox.Password;
            user.ID = workerRepository.GetListFromCache().Where(p => p.Surname == surnameComboBox.Text).First().ID;
            userReository.Insert(user);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
