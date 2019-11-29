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
    /// Логика взаимодействия для ChangeWorkerWindow.xaml
    /// </summary>
    using Models;
    using Repository.Interfaces;
    using Repository.Implementation;
    public partial class ChangeWorkerWindow : Window
    {
        private struct Items
        {
            public Contract contr { get; set; }
            
        }
        Worker worker;
        private readonly IContractRepository contractRepositoty;
        private readonly IWorkerRepository workerRepositoty;
        private readonly IDepartamentRepository departamentRepositoty;
        private readonly IArtistCategoryRepository artistCategoryRepositoty;
        private readonly IContractTypeRepository contractTypeRepositoty;
        public ChangeWorkerWindow(Worker wr, Departament dp)
        {
            InitializeComponent();
            contractRepositoty = new ContractRepository(new ApplicationContext());
            workerRepositoty = new WorkerRepository(new ApplicationContext());
            departamentRepositoty = new DepartamentRepository(new ApplicationContext());
            artistCategoryRepositoty = new ArtistCategoryRepository(new ApplicationContext());
            contractTypeRepositoty = new ContractTypeRepository(new ApplicationContext());
            departament.ItemsSource = departamentRepositoty.GetListFromCache().Select(p => p.Name);

            worker = wr;

            this.name.Text = wr.Name;
            this.departament.Text = dp.Name;


            this.name.Text= worker.Name;

            this.departament.Text = departamentRepositoty.GetByIdFromCahce(wr.DepartamentID).Name;
            this.birthPlace.Text = worker.BirthPlace;
            surname.Text = worker.Surname;
            patronymic.Text = worker.Patronymic;
            registrationPlace.Text = worker.RegistrationPlace;
            birthPlace.Text = worker.BirthPlace;
            dateOfBirth.SelectedDate = worker.DateOfBirth;
            nationality.Text = worker.Nationality;
            education.Text = worker.Education;
            sex.Text = worker.Sex == true ? "мужской" : "женский";
            maritalStatus.Text = worker.MaritalStatus == true ? "холост" : "женат";
            phoneNum.Text = worker.PhoneNum;
            email.Text = worker.Email;


            IEnumerable<Contract> contracts;
            contracts = contractRepositoty.Get(p => p.WorkerId == wr.ID);
            dayLeft.Content = "Последний контракт истекает через: " + 
                (contracts.Last().EndDate - DateTime.Now).Days.ToString() + " дня";
            foreach (Contract cntr in contracts)
            {
                contractListView.Items.Add(new Items
                {     
                    contr = cntr
                });
            }

        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            WorkersWindow workWin = new WorkersWindow();
            workWin.Show();
            Close();
        }

        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan days = TimeSpan.Zero;
            if (contractListView.Items != null)
            {
                for (int i = contractListView.Items.Count-1; i >= 0; i-- )
                {
                    Items Value = (Items)contractListView.Items[i];
                    if (Value.contr.ContractTypeId == 2)
                    {
                        days += Value.contr.EndDate - Value.contr.BeginningDate;
                    }
                    else
                        break;
                }
            }
            AddContractWindow contractWind = null;
            if (days.Days > 1826)
            {
                contractWind = new AddContractWindow(false);
            }
            else
                contractWind = new AddContractWindow(true);
            //1826
            //var it = ;

            this.Visibility = Visibility.Collapsed;
            if (contractWind.ShowDialog() == false)
            {
                this.Visibility = Visibility.Visible;
                return;
            }
            if (contractWind.DialogResult.HasValue && contractWind.DialogResult.Value)
            {
                Contract contract = new Contract();
                contract.ContractTypeId = contractTypeRepositoty.Get(p => p.Name == contractWind.typeOfCon).First().ID; 
                contract.BeginningDate = (DateTime)contractWind.beginningDt;
                contract.EndDate = (DateTime)contractWind.endDt;
                contract.WorkerId = worker.ID;
                contract.OrderNum = contractWind.orderN;
                contract.ArtistCategoryId = artistCategoryRepositoty.Get(p => p.Name == contractWind.artistCat).First().ID;
                contract.Position = contractWind.posit;

                contractRepositoty.Insert(contract);

                contractListView.Items.Add(new Items
                {
                    contr = contract
                });
                
            }
            this.Visibility = Visibility.Visible;
            contractListView.Items.Refresh();

        }

        private void RemoveContractButton_Click(object sender, RoutedEventArgs e)
        {

            if (contractListView.SelectedItem != null)
            {
                Items item = (Items)contractListView.SelectedItem;
                contractRepositoty.Delete(item.contr);

                contractListView.Items.Remove(contractListView.SelectedItem);
                contractListView.Items.Refresh();
            }
            else
            {
                MessageBox.Show("errror");
                return;
            }
        }
        private void ChangeContractButton_Click(object sender, RoutedEventArgs e)
        {

            if (contractListView.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            Items item = (Items)contractListView.SelectedItem;

            AddContractWindow contractWind = new AddContractWindow(contractTypeRepositoty.FindByID(item.contr.ContractTypeId).Name,
                artistCategoryRepositoty.FindByID(item.contr.ArtistCategoryId).Name,
                item.contr.BeginningDate, item.contr.EndDate,
                 item.contr.OrderNum, item.contr.Position);
            this.Visibility = Visibility.Collapsed;
            if (contractWind.ShowDialog() == false)
            {
                this.Visibility = Visibility.Visible;
                return;
            }

            contractListView.Items.Remove(contractListView.SelectedItem);
            if (contractWind.DialogResult.HasValue && contractWind.DialogResult.Value)
            {
                Contract contract = item.contr;
                contract.ContractTypeId = contractTypeRepositoty.Get(p => p.Name == contractWind.typeOfCon).First().ID;
                contract.BeginningDate = (DateTime)contractWind.beginningDt;
                contract.EndDate = (DateTime)contractWind.endDt;
                contract.WorkerId = worker.ID;
                contract.OrderNum = contractWind.orderN;
                contract.ArtistCategoryId = artistCategoryRepositoty.Get(p => p.Name == contractWind.artistCat).First().ID;
                contract.Position = contractWind.posit;

                contractListView.Items.Add(new Items
                {
                    contr = contract
                });
            }

            this.Visibility = Visibility.Visible;

            contractListView.Items.Refresh();
        }

        private void SaveWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Worker workr = workerRepositoty.FindByID(this.worker.ID); ;

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


            workerRepositoty.Modified(workr);
            workerRepositoty.Update(workr);
            workerRepositoty.UpdateCache(worker, worker.ID);

            MessageBox.Show("changed");
            Close_Click(sender, e);
        }
    }
}
