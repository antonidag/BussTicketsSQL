using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussAdmin
{
    public class Trip
    {
        string fCity, tCity, date, depature, incomming;
        int price,id;
        public Trip(string fCity,string tCity,string date,string depature,string incomming,int price,int id)
        {
            this.id = id;

            this.fCity = fCity;
            this.tCity = tCity;

            this.date = date;
            this.depature = depature;
            this.incomming = incomming;
            this.price = price;
        }
        public int GetID()
        {
            return id;
        }
        public override string ToString()
        {
            return fCity +" - " +tCity + ". Date: " + date.Remove(10) + ".\nDepature at:" + depature.Remove(0, 11) + ". Arrival at:" + incomming.Remove(0, 11) + ".\nPrice: " + price + " kr";
        }
    }
}
