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
    /// Логика взаимодействия для LessonWindow.xaml
    /// </summary>
    using Models;
    using Repository.Implementation;
    using Repository.Interfaces;
    using System.Windows.Controls.Primitives;

    static class GettingCell
    {

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static DataGridRow GetSelectedRow(this DataGrid grid)
        {
            return (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem);
        }
        public static DataGridRow GetRow(this DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }
    }


    public partial class LessonWindow : Window
    {
        private readonly ILessonEventRepository lessonRepository;
        private readonly ILessonMarksRepository lessonMarkRepository;
        private readonly IWorkerRepository workerRepository;
        private struct Items
        {
            public Worker wrk { get; set; }
        }
        public LessonWindow()
        {
            InitializeComponent();

            List<Items> items = new List<Items>();

            lessonRepository = new LessonEventRepository(new ApplicationContext());
            workerRepository = new WorkerRepository(new ApplicationContext());
            lessonMarkRepository = new LessonMarksRepository(new ApplicationContext());
            
            foreach (Worker wr in workerRepository.GetAllFromCache(1))
            {
                Items item = new Items() { wrk = wr };
                items.Add(item);
            }

            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                DataGridCheckBoxColumn chekColumn = new DataGridCheckBoxColumn();
                chekColumn.Header = i.ToString();
                chekColumn.Binding = new Binding(i.ToString());
                chekColumn.IsReadOnly = true;

                chekColumn.CellStyle = new Style(typeof(DataGridCell));
                chekColumn.CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.LightGray)));

                lessonGrid.Columns.Add(chekColumn);
                
            }
            DataGridTextColumn num = new DataGridTextColumn();
            num.Header = "Sum";
            num.Binding = new Binding("Sum");
            
            num.CellStyle = new Style(typeof(DataGridCell));
            num.CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.LightGray)));
            lessonGrid.Columns.Add(num);

            lessonGrid.ItemsSource = items;


            //SetValue();

        }
        public void GridCount()
        {
            for (int row = 0; row < workerRepository.GetAllFromCache(1).Count(); row++)
            {
                int num = 0;
                DataGridRow r = lessonGrid.GetRow(row);
                for (int column = 1; column < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + 1; column++)
                {
                    var sd = lessonGrid.GetCell(r, column).Content;
                    CheckBox ch = (CheckBox)sd;
                    if (ch.IsChecked == true)
                    {
                        num++;
                    }
                }
                TextBlock s = (TextBlock)lessonGrid.GetCell(r, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + 1).Content;
                s.Text = num.ToString();
            }
        }

        public void SetValue(bool clear = true)
        {
            List<Items> wrList = (List<Items>)lessonGrid.ItemsSource;
            for (int column = 1; column < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)+1; column++)
            {
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, column);
                LessonEvent lsnEvent = new LessonEvent();
                if (lessonRepository.IsExist(p => p.Date == dt))
                    lsnEvent = lessonRepository.GetAllFromCache().Where(d => d.Date == dt).First();
                else
                {
                    lsnEvent.Date = dt;
                    lessonRepository.Insert(lsnEvent);
                    lessonRepository.AddToCache(lsnEvent, lsnEvent.ID);
                }
                for (int row = 0; row < workerRepository.GetAllFromCache(1).Count(); row++)
                {
                    Worker wr = wrList[row].wrk;
                    DataGridRow r = lessonGrid.GetRow(row);
                    var s = lessonGrid.GetCell(r, column).Content;
                    CheckBox ch = (CheckBox)s;
                    if(lessonMarkRepository.IsExist(p => p.LessonEventID == lsnEvent.ID && p.WorkerID == wr.ID))
                    //if (lessonRepository.IsExist(p => p.Date == dt))
                        ch.IsChecked = clear ? lessonMarkRepository.Get(p => p.WorkerID == wr.ID && p.LessonEventID == lsnEvent.ID).First().IsVisited : false;
                    else
                        ch.IsChecked = false;
                    s = ch;
                    lessonGrid.GetCell(r, column).Content = ch;
                }
            }
            GridCount();



        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            List<Items> wrList = (List<Items>)lessonGrid.ItemsSource;

            for (int column = 1; column < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)+1; column++)
            {

                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, column);
                LessonEvent lsnEvent = new LessonEvent();
                if (!lessonRepository.IsExist(p => p.Date == dt))
                {
                    lsnEvent.Date = dt;
                    lessonRepository.Insert(lsnEvent);
                    lessonRepository.AddToCache(lsnEvent, lsnEvent.ID);
                }
                else
                    lsnEvent = lessonRepository.GetAllFromCache().Where(d => d.Date == dt).First();


                for (int row = 0; row < workerRepository.GetAllFromCache(1).Count(); row++)
                {
                    Worker wr = wrList[row].wrk;
                    DataGridRow r = lessonGrid.GetRow(row);
                    CheckBox ch = (CheckBox)lessonGrid.GetCell(r, column).Content;

                    if (!lessonMarkRepository.IsExist(p => p.WorkerID == wr.ID && p.LessonEventID == lsnEvent.ID))
                    {

                        LessonMarks lsnMark = new LessonMarks();
                        lsnMark.IsVisited = ch.IsChecked.Value;
                        lsnMark.WorkerID = wr.ID;
                        lsnMark.LessonEventID = lsnEvent.ID;
                        lessonMarkRepository.Insert(lsnMark);
                    }
                    else
                    {
                        LessonMarks lsnMark = lessonMarkRepository.Get(p => p.WorkerID == wr.ID && p.LessonEventID == lsnEvent.ID).First();
                        lsnMark.IsVisited = ch.IsChecked.Value;
                        lessonMarkRepository.Update(lsnMark);
                    }
                }

                SetCellColor(false);
                SetChanged(false);
                //lessonRepository.ClearAll();

            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            SetChanged();
            SetCellColor();
        }

        private void SetChanged(bool change = true)
        {
            for (int column = 1; column < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + 1; column++)
            {
                lessonGrid.Columns.ElementAt(column).IsReadOnly = change ? false : true;
            }
        }
        private void SetCellColor(bool color = true)
        {

            for (int column = 1; column < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + 1; column++)
            {


                for (int row = 0; row < workerRepository.GetAllFromCache(1).Count(); row++)
                {
                    DataGridRow r = lessonGrid.GetRow(row);
                    lessonGrid.GetCell(r, column).Background = color ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.LightGray);

                }
            }
        }
        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            SetValue(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetValue();
        }

        private void lessonGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridCount();
        }

        private void ChangedSell(object sender, SelectionChangedEventArgs e)
        {
            GridCount();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseEvent chooseWin = new ChooseEvent();
            chooseWin.Show();
            Close();
        }

        private void ChangedSell(object sender, EventArgs e)
        {

        }
    }
}
