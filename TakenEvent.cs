using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2_Dz2
{
    internal class TakenEvent : IJournalEntry
    {
        
        private string shelf;
        private int slot;
        private string item;
        public string Shelf 
        {
            get
            {
                return shelf;
            }
        }

        public int Slot 
        { 
            get
            {
                return slot;
            }
        }

        public string Item 
        { 
            get
            {
                return item;
            }
        }

        public TakenEvent(string shelf, int slot, string item)
        {
            this.shelf = shelf;
            this.slot = slot;
            this.item = item;
        }

        public string ToLogLine()
        {
            return $"{shelf}|{slot}|{item}";
        } 

        public string ToScreenLine()
        {
            return $"Изъятие | полка {shelf} | слот {slot} | товар «{item}»";
        }    

        public static TakenEvent FromLogLine(string line)
        {
            var p = line.Split('|');
            return new TakenEvent(p[0], int.Parse(p[1]), p[2]);
        }
    }
}
