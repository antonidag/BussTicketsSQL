using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BussAdmin
{
    public class BookManager
    {
        private int totalTickets = 0;
        private int ticketsLeft = 0;
        private Trip tripToBook;
        private int numberOfTickets;
        public BookManager()
        {
            tripToBook = null;
        }
        public void GetBookingTrip(Trip trip)
        {
            tripToBook = trip;

            // Specify connection options and open an connection
            NpgsqlConnection conn = new NpgsqlConnection("Server=pgserver.mah.se;User Id=ae4864;" + "Password=y8334zd4;Database=ae4864buss;");
            conn.Open();
            NpgsqlCommand cmd = null;
            // Define a query
            cmd = new NpgsqlCommand("select antalplatser from buss as b join tur as t on b.regnr = t.bussreg where t.id =" + tripToBook.GetID(), conn);

            // Execute a query
            NpgsqlDataReader dr = cmd.ExecuteReader();

            // Read all rows and output the first column in each row
            while (dr.Read())
            {
                totalTickets = int.Parse(dr[0].ToString());
            }
            // Close connection
            conn.Close();

        }
        public bool BookTrip(int numberOfTickets,string email)
        {
            this.numberOfTickets = numberOfTickets;
            if(numberOfTickets > 0)
            {
                //Säkerhets själ hämtar vi alltid hur många biljetter som är kvar från databasen.
                ticketsLeft = GetTicketsLeft();
                NpgsqlConnection conn = new NpgsqlConnection("Server=pgserver.mah.se;User Id=ae4864;" + "Password=y8334zd4;Database=ae4864buss;");
                conn.Open();
                NpgsqlCommand cmd = null;
                if ((ticketsLeft - numberOfTickets) > -1)
                {
                    cmd = new NpgsqlCommand("insert into biljett(resenarid,turid,antalbiljetter) values ('" + email + "'," + tripToBook.GetID() + "," + numberOfTickets + ")", conn);
                    cmd.ExecuteReader();
                    return true;
                }
                // Close connection
                conn.Close();
                return false;
            }
            else
            {
                return false;
            }
            
        }
        public int GetTicketsLeft()
        {
            int left = totalTickets;
            int temp = 0;
            NpgsqlConnection conn = new NpgsqlConnection("Server=pgserver.mah.se;User Id=ae4864;" + "Password=y8334zd4;Database=ae4864buss;");
            conn.Open();
            NpgsqlCommand cmd = null;
            // Define a query
            cmd = new NpgsqlCommand("select sum(antalbiljetter) from biljett where turid=" + tripToBook.GetID(), conn);
            // Execute a query
            NpgsqlDataReader dr = cmd.ExecuteReader();
            // Read all rows and output the first column in each row
            try
            {
                while (dr.Read())
                {
                    temp += int.Parse(dr[0].ToString());
                    left = totalTickets - temp;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + "  \nSql-satsen retunera inget.");
            }
            conn.Close();
            return left;
        }
        public override string ToString()
        {
            return tripToBook.ToString();
        }
        public int GetTotalTickets()
        {
            return totalTickets;
        }
    }
}
