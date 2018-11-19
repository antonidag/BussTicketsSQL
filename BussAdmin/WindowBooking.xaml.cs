using Npgsql;
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

namespace BussAdmin
{
    /// <summary>
    /// Interaction logic for WindowBooking.xaml
    /// </summary>
    public partial class WindowBooking : Window
    {
        BookManager bManager;
        public WindowBooking(BookManager bManager)
        {
            InitializeComponent();

            this.bManager = bManager;
            label.Content = bManager.ToString();
            label_totaltickets.Content = bManager.GetTotalTickets();
            UpdateLabelTicketsLeft();
            Console.WriteLine(bManager.ToString());

        }

        private void btn_book_Click(object sender, RoutedEventArgs e)
        {
            btn_book.IsEnabled = false;
            string numberOfTickets = textBox_numberOfTicktes.Text.ToString();
            string email = textBox_email.Text.ToString();
            //Ska man checka att det finns en användare här i if:en också?
            if (ValidateInt(numberOfTickets))
            {
                try
                {
                    if (bManager.BookTrip(int.Parse(numberOfTickets), email))
                    {
                        MessageBox.Show("Your tickets are now booked! \nA email was sent to as confirmation.");
                    }

                    else
                        MessageBox.Show("Your ticktes could not be booked!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                UpdateLabelTicketsLeft();
            }
            btn_book.IsEnabled = true;
        }
        private bool ValidateInt(string i)
        {
            try
            {
                int.Parse(i);
                return true;
            }
            catch
            {
                MessageBox.Show("Input integers only");
                return false;
            }

        }

        private void UpdateLabelTicketsLeft()
        {
            label_ticketsLeft.Content = bManager.GetTicketsLeft();
        }

    }
}
