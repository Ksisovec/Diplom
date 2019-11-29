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
    using Authentication;

    /// <summary>
    /// Логика взаимодействия для WorkersWindow.xaml
    /// </summary>
    using Models;
    using Repository.Implementation;
    using Repository.Interfaces;
    public partial class WorkersWindow : Window
    {
        private struct Items
        {
            public Worker wrk { get; set; }
            public Departament dep { get; set; }
            public TimeSpan date { get; set; }
        }

        
        private readonly IWorkerRepository workerRepositoty;
        private readonly IDepartamentRepository departamentRepositoty;
        private readonly IContractRepository contractRepositoty;
        public WorkersWindow()
        {
            InitializeComponent();
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            departamentRepositoty = new DepartamentRepository(new ApplicationContext());
            contractRepositoty = new ContractRepository(new ApplicationContext());

            IEnumerable<Worker> workers;
            workers = workerRepositoty.GetListFromCache();
            

            foreach (Worker wr in workers)
            {
                DateTime d2 = contractRepositoty.Get(p => p.WorkerId == wr.ID).Last().EndDate;
                TimeSpan d3 = d2 - DateTime.Now;
                workerListView.Items.Add(new Items
                {
                    wrk = wr,
                    dep = departamentRepositoty.GetByIdFromCahce(wr.DepartamentID),
                    date = d3
                });
            }
        }




        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            workerRepositoty.ClearCache();
            LoginWindow mainWin = new LoginWindow();
            mainWin.Show();
            Close();
        }

        private void AddtButton_Click(object sender, RoutedEventArgs e)
        {
            CreateWorkerWindow crtWrkWin = new CreateWorkerWindow();
            crtWrkWin.Show();
            Close();
        }

        private void ChangeButtonn_Click(object sender, RoutedEventArgs e)
        {

            if (workerListView.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            Items it = (Items)workerListView.SelectedItem;
            ChangeWorkerWindow crtWrkWin = new ChangeWorkerWindow(it.wrk, it.dep);
            crtWrkWin.Show();
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (workerListView.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            Items it = (Items)workerListView.SelectedItem;
            workerRepositoty.Delete(it.wrk);
            workerRepositoty.DeleteFromCache(it.wrk.ID);

            IEnumerable<Worker> workers;
            workers = workerRepositoty.GetListFromCache();
            foreach (Worker wr in workers)
            {
                DateTime d2 = contractRepositoty.Get(p => p.WorkerId == wr.ID).Last().EndDate;
                TimeSpan d3 = d2 - DateTime.Now;
                workerListView.Items.Add(new Items
                {
                    wrk = wr,
                    dep = departamentRepositoty.GetByIdFromCahce(wr.DepartamentID),
                    date = d3
                });
            }
            workerListView.Items.Refresh();
        }
    }
}
