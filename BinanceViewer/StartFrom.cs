using System;
using System.Linq;
using System.Windows.Forms;
using BinanceAcountViewer;
using BinanceAcountViewer.Properties;

namespace AcountViewer
{
    public partial class StartFrom : Form
    {

        public StartFrom()
        {
            InitializeComponent();
            Credentials.InitCredentials(Settings.Default.PathToFileWithKeys);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var credentials = Credentials.GetCredentials();
            var user1Form = new TradingInfo(credentials[0].Key, credentials[0].Value);
            user1Form.IsMdiContainer = true;
            user1Form.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var credentials = Credentials.GetCredentials();
            var user1Form = new BotViewer(credentials[0].Key, credentials[0].Value);
            user1Form.IsMdiContainer = true;
            user1Form.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            /* using (BinanceAcountViewer.ApplicationContext db = new BinanceAcountViewer.ApplicationContext())
             {
                 // создаем два объекта User
                 User user1 = new User { Name = "Tom", Age = 33 };
                 User user2 = new User { Name = "Alice", Age = 26 };
                 Warrior war = new Warrior { Nicname = "Aldboy", Age = 96 };

                 // добавляем их в бд
                 db.Users.AddRange(user1, user2);
                 db.SaveChanges();
             }
             // получение данных
             using (BinanceAcountViewer.ApplicationContext db = new BinanceAcountViewer.ApplicationContext())
             {
                 // получаем объекты из бд и выводим на консоль
                 var users = db.Users.ToList();
                 Console.WriteLine("Users list:");
                 foreach (User u in users)
                 {
                     Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                 }
             }

             var statisticForm = new PriceStatistic();
             statisticForm.IsMdiContainer = true;
             statisticForm.Show();*/
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var user1Form = new SetupTradingList();
            user1Form.IsMdiContainer = true;
            user1Form.Show();

        }
    }
}
