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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    using Models;
    using Repository.Implementation;
    using Repository.Interfaces;
    using Authentication;

    public partial class MainWindow : Window
    {
        private readonly IWorkerRepository workerRepositoty;
        private readonly IDepartamentRepository departamentRepositoty;
        public MainWindow()
        {
            InitializeComponent();
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            departamentRepositoty = new DepartamentRepository(new ApplicationContext());
        }

        private void ContractsButton_Click(object sender, RoutedEventArgs e)
        {
            WorkersWindow wrkWindow = new WorkersWindow();
            wrkWindow.Show();
            Close();
        }

        private void MarksButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseEvent choseEv = new ChooseEvent();
            choseEv.Show();
            Close();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWin = new LoginWindow();
            loginWin.Show();
            Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
