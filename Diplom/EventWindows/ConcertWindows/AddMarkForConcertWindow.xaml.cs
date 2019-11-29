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
    /// Логика взаимодействия для AddMarkForConcertWindow.xaml
    /// </summary>
    public partial class AddMarkForConcertWindow : Window
    {
        public AddMarkForConcertWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

            //if (int.TryParse(marksBox.Text, Mark))
            //{
            //    MessageBox.Show("errror");
            //    return;
            //}
            this.DialogResult = true;
        }

        public int Mark
        {
            get { return int.Parse(marksBox.Text); }
        }
    }
}
