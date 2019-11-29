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
    using EventWindows;

    /// <summary>
    /// Логика взаимодействия для InfoEventWindow.xaml
    /// </summary>
    using Models;
    using Repository.Implementation;
    using Repository.Interfaces;

    public partial class InfoEventWindow : Window
    {
        private readonly ILessonEventRepository lessonRepository;
        private readonly ILessonMarksRepository lessonMarkRepository;
        private readonly IWorkerRepository workerRepository;
        private readonly IConcertEventRepository concertRepository;
        private readonly IConcertMarksRepository concertMarkRepository;
        public struct Items
        {
            public Worker wrk { get; set; }
        }
        public InfoEventWindow()
        {

            List<Items> items = new List<Items>();

            InitializeComponent();
            lessonRepository = new LessonEventRepository(new ApplicationContext());
            workerRepository = new WorkerRepository(new ApplicationContext());
            lessonMarkRepository = new LessonMarksRepository(new ApplicationContext());
            concertRepository = new ConcertEventRepository(new ApplicationContext());
            concertMarkRepository = new ConcertMarksRepository(new ApplicationContext());
            foreach (Worker wr in workerRepository.GetAllFromCache(1))
            {
                Items item = new Items() { wrk = wr };
                //wrk = wr;
                items.Add(item);
            }

            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            {
                DataGridTextColumn chekColumn = new DataGridTextColumn();
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

        }

        public void GridSum(List<Items> wrList)
        {

            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dt2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            IEnumerable<ConcertEvent> concert = concertRepository.Get(p => p.EndDate <= dt2 && p.EndDate >= dt);
            //p.BeginningDate
            //(p => dt2 >= p.EndDate)
            for (int row = 0; row < workerRepository.GetAllFromCache().Count(); row++)
            {
                int sum = 0;
                int wrId = wrList[row].wrk.ID;

                foreach (ConcertEvent cncrt in concert)
                {
                    if(concertMarkRepository.IsExist(p => p.ConcertEventID == cncrt.ID && p.WorkerID == wrId))
                        sum += concertMarkRepository.Get(p => p.ConcertEventID == cncrt.ID && p.WorkerID == wrId).First().NumOfMarks;
                }

                sum += lessonMarkRepository.Get(p => p.WorkerID == wrId && p.IsVisited == true).Count();
                DataGridRow r = lessonGrid.GetRow(row);
                TextBlock s = (TextBlock)lessonGrid.GetCell(r, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + 1).Content;
                s.Text = sum.ToString();
            }

        }

        public void SetValue()
        {
            List<Items> wrList = (List<Items>)lessonGrid.ItemsSource;
            for (int column = 1; column < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + 1; column++)
            {


                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, column);
                LessonEvent lsnEvent = new LessonEvent();
                if (lessonRepository.IsExist(p => p.Date == dt))
                    lsnEvent = lessonRepository.GetAllFromCache().Where(d => d.Date == dt).First();
                //lsnEvent = lessonRepository.Get(d => d.Date == dt).First();

                ConcertEvent concEvent = new ConcertEvent();
                if (concertRepository.IsExist(p => dt >= p.BeginningDate && dt <= p.EndDate))
                {
                    concEvent = concertRepository.GetListFromCache().Where(p => dt >= p.BeginningDate && dt <= p.EndDate).First();
                    //concEvent = concertRepository.Get(p => dt >= p.BeginningDate && dt <= p.EndDate).First();
                }
                for (int row = 0; row < workerRepository.GetAllFromCache(1).Count(); row++)
                {
                    int wrId = wrList[row].wrk.ID;
                    DataGridRow r = lessonGrid.GetRow(row);

                    if (lessonMarkRepository.IsExist(p => p.LessonEventID == lsnEvent.ID && p.IsVisited == true && p.WorkerID == wrId))
                    {
                        lessonGrid.GetCell(r, column).Background = new SolidColorBrush(Colors.Red);
                    }
                    if (concertMarkRepository.IsExist(p => p.ConcertEventID == concEvent.ID && p.WorkerID == wrId))
                    {
                        lessonGrid.GetCell(r, column).Background = new SolidColorBrush(Colors.Green);
                    }
                }

                
            }

            GridSum(wrList);



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetValue();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            
            if(lessonGrid.SelectedItem == null)
            {
                MessageBox.Show("errror");
                return;
            }
            Items Value = (Items)lessonGrid.SelectedItem;
            InfoWorker infoWorker = new InfoWorker(Value.wrk);
            infoWorker.Show();
            Close();
        }

        private void SaveExelButton_Click(object sender, RoutedEventArgs e)
        {

            List<Items> wrList = (List<Items>)lessonGrid.ItemsSource;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int j = 0; j < lessonGrid.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = lessonGrid.Columns[j].Header;
            }
            for (int i = 0; i < lessonGrid.Columns.Count; i++)
            {
                DateTime dt;
                ConcertEvent concEvent = new ConcertEvent();
                LessonEvent lsnEvent = new LessonEvent();
                if (i != 0 && i != lessonGrid.Columns.Count && i != lessonGrid.Columns.Count - 1)
                {
                    dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);

                    concEvent = new ConcertEvent();
                    if (concertRepository.IsExist(p => dt >= p.BeginningDate && dt <= p.EndDate))
                        concEvent = concertRepository.GetListFromCache().Where(p => dt >= p.BeginningDate && dt <= p.EndDate).First();

                    lsnEvent = lessonRepository.GetAllFromCache().Where(d => d.Date == dt).First(); 
                }
                for (int j = 0; j < lessonGrid.Items.Count; j++)
                {

                    int wrId = wrList[j].wrk.ID;
                    DataGridRow r = lessonGrid.GetRow(j);
                    TextBlock b = lessonGrid.Columns[i].GetCellContent(lessonGrid.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];

                    if (lessonMarkRepository.IsExist(p => p.LessonEventID == lsnEvent.ID && p.IsVisited == true && p.WorkerID == wrId))
                    {
                        lessonGrid.GetCell(r, i).Background = new SolidColorBrush(Colors.Red);
                        myRange.Value2 = 1.ToString();
                    }
                    else
                    if (concertMarkRepository.IsExist(p => p.ConcertEventID == concEvent.ID && p.WorkerID == wrId))
                    {
                        lessonGrid.GetCell(r, i).Background = new SolidColorBrush(Colors.Green);
                        double mark = concertMarkRepository.Get(p => p.ConcertEventID == concEvent.ID && p.WorkerID == wrId).First().NumOfMarks;
                        TimeSpan d1 = concEvent.EndDate.Value - concEvent.BeginningDate.Value;
                        myRange.Value2 = "(концерт)" + Math.Round((mark / (double)d1.Days), 2).ToString();
                    }
                    else
                        myRange.Value2 = b.Text;
                }
            }
            workbook.SaveAs("отчёт за " + DateTime.Now.Month + " месяц");
            excel.Quit();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseEvent choseEv = new ChooseEvent();
            choseEv.Show();
            Close();
        }
    }
}
