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
    /// Логика взаимодействия для CreateWorkerWindow.xaml
    /// </summary>
    using Models;
    using Repository.Interfaces;
    using Repository.Implementation;
    public partial class CreateWorkerWindow : Window
    {
        private struct Items
        {
            //public string itemName { get; set; }
            public string typeName { get; set; }

            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public int OrderNum { get; set; }
            public string Position { get; set; }
            public string artistCategory { get; set; }
        }

        private readonly IContractRepository contractRepositoty;
        private readonly IWorkerRepository workerRepositoty;
        private readonly IDepartamentRepository departamentRepositoty;
        private readonly IArtistCategoryRepository artistCategoryRepositoty;
        private readonly IContractTypeRepository contractTypeRepositoty;
        public CreateWorkerWindow()
        {
            InitializeComponent();
            contractRepositoty = new ContractRepository(new ApplicationContext());
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            departamentRepositoty = new DepartamentRepository(new ApplicationContext());
            artistCategoryRepositoty = new ArtistCategoryRepository(new ApplicationContext());
            contractTypeRepositoty = new ContractTypeRepository(new ApplicationContext());

            departament.ItemsSource = departamentRepositoty.GetListFromCache().Select(p => p.Name);
            //departament.ItemsSource = departamentRepositoty.getAll().Select(p => p.Name);
        }
        


        private void AddWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Worker worker = new Worker();
            worker.Name = this.name.Text;
            worker.DepartamentID = departamentRepositoty.Get(p => p.Name == this.departament.Text).Select(p => p.ID).First();
            worker.BirthPlace = this.birthPlace.Text;
            worker.Surname = surname.Text;
            worker.Patronymic = patronymic.Text;
            worker.RegistrationPlace = registrationPlace.Text;
            worker.BirthPlace = birthPlace.Text;
            worker.DateOfBirth = (DateTime)dateOfBirth.SelectedDate;
            worker.Nationality = nationality.Text;
            worker.Education = education.Text;
            worker.Sex = sex.Text == "мужской" ? true : false;
            worker.MaritalStatus = maritalStatus.Text == "холост" ? true : false;
            worker.PhoneNum = phoneNum.Text;
            worker.Email = email.Text;

            workerRepositoty.Insert(worker);
            workerRepositoty.AddToCache(worker, worker.ID);

            if (contractListView.Items != null)
            {
                for (int i = 0; i < contractListView.Items.Count; i++)
                {
                    Items Value = (Items)contractListView.Items[i];
                    Contract cnt = new Contract();

                    cnt.ContractTypeId = contractTypeRepositoty.Get(p => p.Name == Value.typeName).First().ID;
                    cnt.BeginningDate = Value.beginDate;
                    cnt.EndDate = Value.endDate;
                    cnt.WorkerId = worker.ID;
                    cnt.OrderNum = Value.OrderNum;
                    cnt.ArtistCategoryId = artistCategoryRepositoty.Get(p => p.Name == Value.artistCategory).First().ID;
                    cnt.Position = Value.Position;

                    contractRepositoty.Insert(cnt);
                }
            }

            MessageBox.Show("добавлен");
            Close_Click(sender, e);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            WorkersWindow workWin = new WorkersWindow();
            workWin.Show();
            Close();
        }

        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {
            AddContractWindow contractWind = new AddContractWindow(false);
            if (contractWind.ShowDialog() == false)
            {
                this.Visibility = Visibility.Visible;
                return;
            }

            contractListView.Items.Add(new Items
            {
                typeName = contractWind.typeOfCon,
                beginDate = (DateTime)contractWind.beginningDt,
                endDate = (DateTime)contractWind.endDt,
                OrderNum = contractWind.orderN,
                artistCategory = contractWind.artistCat,
                Position = contractWind.posit
            });
            contractListView.Items.Refresh();



        }

        private void RemoveContractButton_Click(object sender, RoutedEventArgs e)
        {
            contractListView.Items.Remove(contractListView.SelectedItem);
            contractListView.Items.Refresh();
        }

        private void ChangeContractButton_Click(object sender, RoutedEventArgs e)
        {
            if (contractListView.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            Items item = (Items)contractListView.SelectedItem;
            AddContractWindow contractWind = new AddContractWindow(item.typeName, item.artistCategory, item.beginDate, item.endDate,
                item.OrderNum, item.Position);
            if (contractWind.ShowDialog() == false)
            {
                this.Visibility = Visibility.Visible;
                return;
            }
            contractListView.Items.Remove(contractListView.SelectedItem);
            contractListView.Items.Add(new Items
            {
                typeName = contractWind.typeOfCon,
                beginDate = (DateTime)contractWind.beginningDt,
                endDate = (DateTime)contractWind.endDt,
                OrderNum = contractWind.orderN,
                artistCategory = contractWind.artistCat,
                Position = contractWind.posit
            });
            contractListView.Items.Refresh();
        }
    }
}
