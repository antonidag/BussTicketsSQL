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
    /// Interaction logic for WindowCreateUser.xaml
    /// </summary>
    public partial class WindowCreateUser : Window
    {
        public WindowCreateUser()
        {
            InitializeComponent();
        }

        private void btn_Create_Click(object sender, RoutedEventArgs e)
        {
            CreateUser();
        }
        public void CreateUser()
        {
            string email = textBox.Text.ToString();
            string name = textBox_Name.Text.ToString();
            string telefon = textBox_Telefon.Text.ToString();
            string adress = textBox_Adress.Text.ToString();


            if (ValidateEmail(email) && ValidateTextBox(name) && ValidateTextBox(telefon) && ValidateTextBox(adress))
            {
                try
                {
                    // Specify connection options and open an connection
                    NpgsqlConnection conn = new NpgsqlConnection("Server=pgserver.mah.se;User Id=ae4864;" + "Password=y8334zd4;Database=ae4864buss;");

                    conn.Open();

                    NpgsqlCommand cmd = null;

                    cmd = new NpgsqlCommand("insert into resenar(email,namn,telenr,adress) values ('" + email + "','" + name + "','" + telefon + "','" + adress + "')", conn);

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    conn.Close();

                    MessageBox.Show("User created!");
                    ClearTextBoxes();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Email is allready used.");
                    Console.WriteLine(ex.Message.ToString());
                }
            }
        }
        private bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                MessageBox.Show("Insert correct emailadress. \n" + email);
                return false;
            }
        }
        private bool ValidateTextBox(string input)
        {
            if(input.Length > 0)
                return true;
            MessageBox.Show("Check your input.");
            return false;
        }
        private void ClearTextBoxes()
        {
            textBox.Text = string.Empty;
            textBox_Adress.Text = string.Empty;
            textBox_Name.Text = string.Empty;
            textBox_Telefon.Text = string.Empty;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
