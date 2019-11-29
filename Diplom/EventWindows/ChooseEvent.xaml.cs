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

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для ChooseEvent.xaml
    /// </summary>
    using Diplom.Models;
    using Diplom.Repository.Interfaces;
    using Diplom.Repository.Implementation;
    using Authentication;

    public partial class ChooseEvent : Window
    {
        private readonly IConcertEventRepository concertRepositoty;
        private readonly IWorkerRepository workerRepositoty;
        public ChooseEvent()
        {
            InitializeComponent();
            concertRepositoty = new ConcertEventRepository(new ApplicationContext());
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            concertListView.ItemsSource = concertRepositoty.GetListFromCache().OrderByDescending(x => x.EndDate).Take(10);
        }

        private void ConcertEventButton_Click(object sender, RoutedEventArgs e)
        {
            ConcertsWindow concert = new ConcertsWindow();
            concert.Show();
            this.Close();
        }

        private void LessonEventButton_Click(object sender, RoutedEventArgs e)
        {
            LessonWindow lesson = new LessonWindow();
            lesson.Show();
            this.Close();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            InfoEventWindow infiWin = new InfoEventWindow();
            infiWin.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            workerRepositoty.ClearCache();
            LoginWindow mainWin = new LoginWindow();
            mainWin.Show();
            Close();
        }
    }
}
