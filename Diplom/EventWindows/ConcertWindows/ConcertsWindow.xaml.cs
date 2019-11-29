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
    /// Логика взаимодействия для ConcertsWindow.xaml
    /// </summary>
    using Diplom.Models;
    using Diplom.Repository.Interfaces;
    using Diplom.Repository.Implementation;
    public partial class ConcertsWindow : Window
    {

        private readonly IConcertEventRepository concertRepositoty;
        public ConcertsWindow()
        {
            InitializeComponent();

            concertRepositoty = new ConcertEventRepository(new ApplicationContext());
            concertListView.ItemsSource = concertRepositoty.GetListFromCache();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddConcerWindow2 addConcert = new AddConcerWindow2();
            addConcert.Show();
            Close();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {

            if (concertListView.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            ConcertEvent Value = (ConcertEvent)concertListView.SelectedItem;
            ChangeConcertWindow changeConcert = new ChangeConcertWindow(Value.ID);
            changeConcert.Show();
            Close();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (concertListView.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }

            ConcertEvent Value = (ConcertEvent)concertListView.SelectedItem;
            concertRepositoty.Delete(Value);
            concertRepositoty.DeleteFromCache(Value.ID);

            concertListView.ItemsSource = concertRepositoty.GetListFromCache();
            concertListView.Items.Refresh();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseEvent choseWin = new ChooseEvent();
            choseWin.Show();
            Close();
        }
    }
}
