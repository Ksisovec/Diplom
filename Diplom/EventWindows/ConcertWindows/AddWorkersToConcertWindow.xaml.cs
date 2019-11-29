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
    /// Логика взаимодействия для AddWorkersToConcertWindow.xaml
    /// </summary>
    using Diplom.Models;
    using Repository.Implementation;
    using Repository.Interfaces;

    public partial class AddWorkersToConcertWindow : Window
    {
        private readonly IWorkerRepository workerRepositoty;
        private Worker worker;
        private int mark;
        public AddWorkersToConcertWindow()
        {
            InitializeComponent();
            worker = new Worker();
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            listView.ItemsSource = workerRepositoty.GetAllFromCache();
        }

        public AddWorkersToConcertWindow(List<Worker> wrList)
        {
            InitializeComponent();
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            List<Worker> wrk = workerRepositoty.getAll().Where(p => p.DepartamentID == 1).ToList();
            //Worker www = workerRepositoty.FindByID(1);
            //wrk.Remove(www);
            if (wrList != null)
                for (int i = 0; i < wrList.Count; i++)
                {
                    Worker www = workerRepositoty.FindByID(wrList[i].ID);
                    
                    wrk.Remove(www);
                }
            listView.ItemsSource = wrk;


        }

        public Worker Worker
        {
            get { return worker; }
        }

        public int Mark
        {
            get { return mark; }
        }

        private void AddWorkersButton_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            AddMarkForConcertWindow addMark = new AddMarkForConcertWindow();
            addMark.ShowDialog();
            //ConcertMarks concertMarks = new ConcertMarks();
            mark = addMark.Mark;
            worker = (Worker)listView.SelectedItem;
            this.DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
