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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    using Models;
    using Repository.Interfaces;
    using Repository.Implementation;
    public partial class LoginWindow : Window
    {
        private readonly IUserRepository userReository;
        private readonly IWorkerRepository workerRepository;
        private readonly IDepartamentRepository departamentRepository;
        public LoginWindow()
        {
            InitializeComponent();
            workerRepository = new WorkerRepository(new ApplicationContext());
            userReository = new UserRepository(new ApplicationContext());
            departamentRepository = new DepartamentRepository(new ApplicationContext());

            //var x = departamentRepository.GetListFromCache().Where(p => p.Name == "Балет");
            //var y = departamentRepository.GetAllFromCache().Where(p => p.Name == "Балет");

            surnameComboBox.ItemsSource = workerRepository.GetListFromCache().Select(p => p.Surname);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow newWindow = new RegisterWindow();
            newWindow.ShowDialog();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (surnameComboBox.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            Worker worker = workerRepository.GetListFromCache().Where(p => p.Surname == surnameComboBox.Text).First();
            //Worker worker2 = workerRepository.Get(p => p.Surname == surnameComboBox.Text).First();
            User user = userReository.FindByID(worker.ID);
            if (user == null)
            {
                MessageBox.Show("error pass");
                return;
            }
            if (passwordBox.Password != user.Password)
            {
                MessageBox.Show("error pass");
                return;
            }

            Window mainWin = new Window();
            if (worker.DepartamentID == departamentRepository.GetListFromCache().Where(p => p.Name == "Балет").Select(p => p.ID).First())
                mainWin = new ChooseEvent();

            if (worker.DepartamentID == departamentRepository.GetListFromCache().Where(p => p.Name == "Администрация").Select(p => p.ID).First())
                mainWin = new WorkersWindow();
            if (mainWin.IsInitialized == false)
            {
                MessageBox.Show("Недостаточно прав доступа");
                return;
            }

            mainWin.Show();

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           
            Close();
        }
    }
}
