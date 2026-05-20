using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2_Dz2
{
    internal class ExistEvent : IJournalEntry
    {
        private string shelf;
        private int slot;
        private string item;

        public ExistEvent(string shelf, int slot, string item)
        {
            this.shelf = shelf;
            this.slot = slot;
            this.item = item;
        }

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
        public string ToLogLine()
        {
            return shelf + "|" + slot + "|" + item;
        }
        public string ToScreenLine()
        {
            return "Расположение | полка " + shelf + " | слот " + slot + " | товар " + item;
        }
        public static ExistEvent FromLogLine(string line)
        {
            string[] p = line.Split('|');
            return new ExistEvent(p[0], int.Parse(p[1]), p[2]);
        }
    }
}
