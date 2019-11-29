using Diplom.Repository.Implementation;
using Diplom.Repository.Interfaces;
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
    /// Логика взаимодействия для AddContractWindow.xaml
    /// </summary>
   
    public partial class AddContractWindow : Window
    {
        public struct Items
        {
            //public string itemName { get; set; }
            public string type { get; set; }

            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
        }

        private readonly IArtistCategoryRepository artistCategoryRepositoty;
        private readonly IContractTypeRepository contractTypeRepositoty;

        public AddContractWindow(bool type)
        {
            InitializeComponent();
            artistCategoryRepositoty = new ArtistCategoryRepository(new ApplicationContext());
            contractTypeRepositoty = new ContractTypeRepository(new ApplicationContext());

            //contractTypeRepositoty.getAll().Where(p => p.ID == 1).Select(p => p.Name)
            typeOfContract.ItemsSource = type ? contractTypeRepositoty.getAll().Select(p => p.Name) :
                contractTypeRepositoty.Get(p => p.ID == 1).Select(p => p.Name);
            artistCategory.ItemsSource = artistCategoryRepositoty.getAll().Select(p => p.Name); 
        }
        public AddContractWindow(string type, string placeType, DateTime beginDate, DateTime endDate, int orderN, string posit)
        {
            InitializeComponent();

            artistCategoryRepositoty = new ArtistCategoryRepository(new ApplicationContext());
            contractTypeRepositoty = new ContractTypeRepository(new ApplicationContext());

            typeOfContract.ItemsSource = contractTypeRepositoty.getAll().Select(p => p.Name);// add cache?
            artistCategory.ItemsSource = artistCategoryRepositoty.getAll().Select(p => p.Name); //???

            orderNum.Text = orderN.ToString();
            this.position.Text = posit;
            artistCategory.Text = placeType;
            typeOfContract.Text = type;
            beginningDate.SelectedDate = beginDate;
            this.endDate.SelectedDate = endDate;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public int orderN
        {
            get { return int.Parse(orderNum.Text); }
        }
        public string typeOfCon
        {
            get { return typeOfContract.Text; }
        }
        public DateTime? beginningDt
        {
            get { return beginningDate.SelectedDate; }
        }
        public DateTime? endDt
        {
            get { return endDate.SelectedDate; }
        }
        public string posit
        {
            get { return position.Text; }
        }
        public string artistCat
        {
            get { return artistCategory.Text; }
        }

    }
}
