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
    using Diplom.Models;
    using Diplom.Repository.Interfaces;
    using Diplom.Repository.Implementation;
    /// <summary>
    /// Логика взаимодействия для AddConcerWindow2.xaml
    /// </summary>
    public partial class AddConcerWindow2 : Window
    {
        private struct Items
        {
            //public string itemName { get; set; }
            public Worker wrk { get; set; }
            public int mark { get; set; }
        }

        private readonly IConcertEventRepository concertRepositoty;
        private readonly IConcertMarksRepository concertMarksRepositoty;
        private readonly IConcertTypeRepository concertTypeRepositoty;
        private readonly IConcertPlaceTypeRepository concertPlaceTypeRepository;
        //private List<Worker> workers;
        public AddConcerWindow2()
        {
            InitializeComponent();
            concertRepositoty = new ConcertEventRepository(new ApplicationContext());
            concertMarksRepositoty = new ConcertMarksRepository(new ApplicationContext());
            concertTypeRepositoty = new ConcertTypeRepository(new ApplicationContext());
            concertPlaceTypeRepository = new ConcertPlaceTypeRepository(new ApplicationContext());

            TypeComboBox.ItemsSource = concertTypeRepositoty.getAll().Select(p => p.Name);
            TypePlaceComboBox.ItemsSource = concertPlaceTypeRepository.getAll().Select(p => p.Name);
            //workerListView.ItemsSource = concertMarksRepositoty.getAll();
            //workers = new List<Worker>();
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            ConcertEvent concert = new ConcertEvent();
            
            concert.BeginningDate = this.BeginingDate.SelectedDate;
            concert.EndDate = this.EndDate.SelectedDate;
            concert.Country = this.CountryTextBox.Text;
            concert.City = this.CitytextBox.Text;
            concert.Address = this.AddresTextBox.Text;
            concert.ConcertTypeId = concertTypeRepositoty.Get(p => p.Name == this.TypeComboBox.Text).First().ID;
            concert.ConcertPlaceTypeId = concertPlaceTypeRepository.Get(p => p.Name == this.TypePlaceComboBox.Text).First().ID;
            concert.Description = this.DescriptionTextBox.Text;


            concertRepositoty.Insert(concert);

            if (workerListView.Items != null)
            {
                concert.ConcertMarks = null;
                //ICollection<ConcertMarks> concertMarks = new System.Collections.ObjectModel.Collection<ConcertMarks>();
                for (int i = 0; i < workerListView.Items.Count; i++)
                {

                        Items Value = (Items)workerListView.Items[i];

                        ConcertMarks concertMark = new ConcertMarks();
                        concertMark.NumOfMarks = Value.mark;
                        concertMark.ConcertEventID = concert.ID;
                        concertMark.WorkerID = Value.wrk.ID;


                        concertMarksRepositoty.Insert(concertMark);

                }
                concertRepositoty.Update(concert);
            }

            concertRepositoty.UpdateCache(concert, concert.ID);


            MessageBox.Show("добавлен");
            Close_Click(sender, e);
            //this.btnReload_Click(sender, e);
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
                workerListView.Items.Refresh();
            }
           this.Visibility = Visibility.Visible;

            //workers.Add(addWorker.Worker);


            //var c = workerListView.Items.GetItemAt(0);

        }

        private void RemoveWorkerButton_Click(object sender, RoutedEventArgs e)
        {
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
