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
    /// Логика взаимодействия для ChangeConcertWindow.xaml
    /// </summary>
    using Diplom.Models;
    using Diplom.Repository.Interfaces;
    using Diplom.Repository.Implementation;
    public partial class ChangeConcertWindow : Window
    {
        private int id;
        private struct Items
        {
            //public string itemName { get; set; }
            public Worker wrk { get; set; }
            public int mark { get; set; }
        }

        private readonly IConcertEventRepository concertRepositoty;
        private readonly IWorkerRepository workerRepositoty;
        private readonly IConcertMarksRepository concertMarksRepositoty;

        private readonly IConcertTypeRepository concertTypeRepositoty;
        private readonly IConcertPlaceTypeRepository concertPlaceTypeRepository;
        public ChangeConcertWindow()
        {
            InitializeComponent();
            concertRepositoty = new ConcertEventRepository(new ApplicationContext());
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            concertMarksRepositoty = new ConcertMarksRepository(new ApplicationContext());
            concertTypeRepositoty = new ConcertTypeRepository(new ApplicationContext());
            concertPlaceTypeRepository = new ConcertPlaceTypeRepository(new ApplicationContext());

        }
        public ChangeConcertWindow(int id)
        {
            this.id = id;
            InitializeComponent();
            concertRepositoty = new ConcertEventRepository(new ApplicationContext());
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            concertMarksRepositoty = new ConcertMarksRepository(new ApplicationContext());
            concertTypeRepositoty = new ConcertTypeRepository(new ApplicationContext());
            concertPlaceTypeRepository = new ConcertPlaceTypeRepository(new ApplicationContext());

            ConcertEvent concert = concertRepositoty.GetByIdFromCahce(id);

            this.BeginingDate.SelectedDate = concert.BeginningDate;
            this.EndDate.SelectedDate = concert.EndDate;
            this.CountryTextBox.Text = concert.Country;
            this.CitytextBox.Text = concert.City;
            this.AddresTextBox.Text = concert.Address;
            this.TypeComboBox.SelectedItem = concertTypeRepositoty.FindByID(concert.ConcertTypeId).Name;
            this.TypePlaceComboBox.SelectedItem = concertPlaceTypeRepository.FindByID(concert.ConcertPlaceTypeId).Name;
            this.DescriptionTextBox.Text = concert.Description;


            TypeComboBox.ItemsSource = concertTypeRepositoty.getAll().Select(p => p.Name);
            TypePlaceComboBox.ItemsSource = concertPlaceTypeRepository.getAll().Select(p => p.Name);

            IEnumerable<ConcertMarks> concertMarks;
            concertMarks = concertMarksRepositoty.getAll().Where(p => p.ConcertEventID == id);

            foreach (ConcertMarks conMark in concertMarks)
            {
                workerListView.Items.Add(new Items
                {
                    wrk = workerRepositoty.GetByIdFromCahce(conMark.WorkerID),
                    mark = conMark.NumOfMarks
                });
            }
        }

        private void AddWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            List<Worker> wrList = new List<Worker>();
            if (workerListView.Items != null)
            {
                for (int i = 0; i < workerListView.Items.Count; i++)
                {
                    Items Value = (Items)workerListView.Items[i];
                    wrList.Add(Value.wrk);
                }
            }


            
            AddWorkersToConcertWindow addWorker = new AddWorkersToConcertWindow(wrList);
            this.Visibility = Visibility.Collapsed;
            addWorker.ShowDialog();
            if (addWorker.DialogResult.HasValue && addWorker.DialogResult.Value)
            {
                workerListView.Items.Add(new Items
                {
                    wrk = addWorker.Worker,
                    mark = addWorker.Mark
                });

                ConcertEvent concert = concertRepositoty.GetByIdFromCahce(id);

                ConcertMarks concertMark = new ConcertMarks();
                concertMark.NumOfMarks = addWorker.Mark;
                concertMark.ConcertEventID = concert.ID;
                concertMark.WorkerID = addWorker.Worker.ID;

                concertMarksRepositoty.Insert(concertMark);
            }
            this.Visibility = Visibility.Visible;

            workerListView.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ConcertEvent concert = concertRepositoty.FindByID(id);

            concert.BeginningDate = this.BeginingDate.SelectedDate;
            concert.EndDate = this.EndDate.SelectedDate;
            concert.Country = this.CountryTextBox.Text;
            concert.City = this.CitytextBox.Text;
            concert.Address = this.AddresTextBox.Text;
            concert.ConcertTypeId = concertTypeRepositoty.Get(p => p.Name == this.TypeComboBox.Text).First().ID;
            concert.ConcertPlaceTypeId = concertPlaceTypeRepository.Get(p => p.Name == this.TypePlaceComboBox.Text).First().ID;
            concert.Description = this.DescriptionTextBox.Text;


            concertRepositoty.Modified(concert);
            concertRepositoty.Update(concert);
            concertRepositoty.UpdateCache(concert, concert.ID);

            MessageBox.Show("changed");
            Close_Click(sender, e);
        }

        private void RemoveWorkerButton_Click(object sender, RoutedEventArgs e)
        {

            Items Value = (Items)workerListView.SelectedItem;
            ConcertMarks conMark = concertMarksRepositoty.Get(p => p.WorkerID == Value.wrk.ID && p.ConcertEventID == id).First();
            concertMarksRepositoty.Delete(conMark);


            workerListView.Items.Remove(workerListView.SelectedItem);
            workerListView.Items.Refresh();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ConcertsWindow conWin = new ConcertsWindow();
            conWin.Show();
            Close();
        }
    }
}
