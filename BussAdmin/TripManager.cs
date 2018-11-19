using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussAdmin
{
    class TripManager
    {
        private List<Trip> myList;
        object myLock = new object();
        public TripManager()
        {
            myList = new List<Trip>();
        }
        public bool Add(Trip trip)
        {
            try
            {
                myList.Add(trip);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Remove(int index)
        {
            try
            {
                myList.RemoveAt(index);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Trip GetAt(int index)
        {
            return myList[index];
        }
        public string GetStringAt(int index)
        {
            return myList[index].ToString();
        }
        public int Count
        {
            get
            {
                return myList.Count;
            }
        }
        public void DelateAll()
        {
            myList.Clear();
        }
    }
}
