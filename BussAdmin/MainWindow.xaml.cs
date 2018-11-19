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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using System.Threading;
using System.Net.Mail;
using System.Net;

namespace BussAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TripManager tripManager;
        BookManager bookManager;
        object myLock;
        public MainWindow()
        {
            InitializeComponent();

            tripManager = new TripManager();
            bookManager = new BookManager();
            myLock = new object();

        }
        public void UpdateListBox()
        {
            listBox.Dispatcher.Invoke(new Action(() => listBox.Items.Clear()));
            for (int i = 0; i < tripManager.Count; i++)
            {
                listBox.Dispatcher.Invoke(new Action(() => listBox.Items.Add(tripManager.GetStringAt(i))));
            }
        }
        public void GetTrips(string inputFrom,string inputTo)
        {
            try
            {
                lock (myLock)
                {

                    tripManager.DelateAll();
                    // Specify connection options and open an connection
                    NpgsqlConnection conn = new NpgsqlConnection("Server=pgserver.mah.se;User Id=ae4864;" +"Password=y8334zd4;Database=ae4864buss;");
                    conn.Open();
                    NpgsqlCommand cmd = null;
                    // Define a query
                    if (inputFrom.Length >= 0 && inputTo.Length == 0)
                    {
                        cmd = new NpgsqlCommand("select f.namn,t.namn,datum,avgang,ankomst,pris,tur.id from tur join stad as f on tur.fstad = f.id join stad as t on tur.tstad = t.id where f.namn like '" + inputFrom + "%'", conn);
                    }
                    else if(inputTo.Length >= 0 && inputFrom.Length == 0)
                    {
                        cmd = new NpgsqlCommand("select f.namn,t.namn,datum,avgang,ankomst,pris,tur.id from tur join stad as f on tur.fstad = f.id join stad as t on tur.tstad = t.id where t.namn like '" + inputTo + "%'", conn);
                    }
                    else if(inputTo.Length >= 0 && inputFrom.Length >= 0)
                    {
                        cmd = new NpgsqlCommand("select f.namn,t.namn,datum,avgang,ankomst,pris,tur.id from tur join stad as f on tur.fstad = f.id join stad as t on tur.tstad = t.id where f.namn like '" + inputFrom + "%' and t.namn like '" + inputTo +"%'", conn);
                    }
                    // Execute a query
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    // Read all rows and output the first column in each row
                   while (dr.Read())
                    {
                        int price = int.Parse(dr[5].ToString());
                        int id = int.Parse(dr[6].ToString());
                        tripManager.Add(new Trip(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), price, id));
                    }
                    // Close connection
                    conn.Close();
                    UpdateListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
        /// <summary>
        /// Everytime input is changed in the from-textbox, start a thread and runs GetTrips method.
        /// </summary>
        private void tbox_InputFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strInputFrom = tbox_InputFrom.Text.ToString();
            string strInputTo = tbox_InputTo.Text.ToString();
            Task tFillListbox = new Task(new Action(() => GetTrips(strInputFrom, strInputTo)));
            tFillListbox.Start();
        }
        /// <summary>
        /// Everytime input is changed in the to-textbox, start a thread and runs GetTrips method.
        /// </summary>
        private void tbox_InputTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strInputFrom = tbox_InputFrom.Text.ToString();
            string strInputTo = tbox_InputTo.Text.ToString();
            Task tFillListbox = new Task(new Action(() => GetTrips(strInputFrom, strInputTo)));
            tFillListbox.Start();
        }

        private void mitem_user_Create_Click(object sender, RoutedEventArgs e)
        {
            WindowCreateUser wCreateUser = new WindowCreateUser();
            wCreateUser.Show();
        }

        private void listBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = listBox.SelectedIndex;
            if(index > -1)
            {
                bookManager.GetBookingTrip(tripManager.GetAt(index));
                WindowBooking wBooking = new WindowBooking(bookManager);
                wBooking.Show();
            }
        }

    }
}
