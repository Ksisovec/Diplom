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

namespace Diplom.EventWindows
{
    /// <summary>
    /// Логика взаимодействия для InfoWorker.xaml
    /// </summary>
    using Diplom.Models;
    using Diplom.Repository.Interfaces;
    using Diplom.Repository.Implementation;
    public partial class InfoWorker : Window
    {
        private readonly ILessonEventRepository lessonRepository;
        private readonly ILessonMarksRepository lessonMarkRepository;
        private readonly IWorkerRepository workerRepository;
        private readonly IConcertEventRepository concertRepository;
        private readonly IConcertMarksRepository concertMarkRepository;
        public struct Items
        {
            public ConcertEvent concert { get; set; }
            public int marks { get; set; }
        }
        public InfoWorker(Worker worker)
        {
            int sum = 0;
            List<Items> items = new List<Items>();

            InitializeComponent();
            lessonRepository = new LessonEventRepository(new ApplicationContext());
            workerRepository = new WorkerRepository(new ApplicationContext());
            lessonMarkRepository = new LessonMarksRepository(new ApplicationContext());
            concertRepository = new ConcertEventRepository(new ApplicationContext());
            concertMarkRepository = new ConcertMarksRepository(new ApplicationContext());



            foreach (ConcertEvent con in concertRepository.Get(p => p.EndDate.Value.Month == DateTime.Now.Month))
            {
                if (concertMarkRepository.IsExist(p => p.ConcertEventID == con.ID &&
                        p.WorkerID == worker.ID))
                {
                    Items item = new Items()
                    {
                        concert = con,
                        marks = concertMarkRepository.Get(p => p.ConcertEventID == con.ID &&
                        p.WorkerID == worker.ID).First().NumOfMarks
                    };
                    sum += item.marks;
                    items.Add(item);
                }
            }
            concertListView.ItemsSource = items;

            this.Namelabel.Content = worker.Name;
            this.Surnamelabel.Content = worker.Surname;

            int lessonVisited = 0;
            foreach (LessonEvent lesson in lessonRepository.Get(p => p.Date.Month == DateTime.Now.Month))
            {
                if (lessonMarkRepository.IsExist(p => p.LessonEventID == lesson.ID && 
                p.WorkerID == worker.ID && p.IsVisited == true))
                {
                    lessonVisited++;
                }
            }
            sum += lessonVisited;
            this.numOfMakrslabel.Content = sum.ToString();
            this.lessonVisitedNumLabel.Content = lessonVisited.ToString();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            InfoEventWindow infWin = new InfoEventWindow();
            infWin.Show();
            Close();
        }
    }
}
