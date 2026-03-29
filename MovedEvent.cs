using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2_Dz2
{
    internal class MovedEvent : IJournalEntry
    {   

        private string fromShelf;
        private int fromSlot;
        private string toShelf;
        private int toSlot;
        private string item;



        public string FromShelf 
        { 
            get
            {
                return fromShelf;
            }
        }
        public int FromSlot 
        {
            get
            {
                return fromSlot;
            }
        }
        public string ToShelf 
        { 
            get
            {
                return toShelf;
            }
        }
        public int ToSlot 
        { 
            get
            {
                return toSlot;
            }
        }
        public string Item 
        { 
            get
            {
                return item;
            }
        }

        public MovedEvent(string fs, int fslot, string ts, int tslot, string item)
        {
            this.fromShelf = fs;
            this.fromSlot = fslot;
            this.toShelf = ts;
            this.toSlot = tslot;
            this.item = item;
        }

        public string ToLogLine()
        {
            return $"{fromShelf}|{fromSlot}|{toShelf}|{toSlot}|{item}";
        }
            

        public string ToScreenLine()
        {
            return $"Перенос | с {fromShelf}:{fromSlot} на {toShelf}:{toSlot} | товар «{item}»";
        } 

        public static MovedEvent FromLogLine(string line)
        {
            string[] p = line.Split('|');

            return new MovedEvent(p[0],int.Parse(p[1]),p[2],int.Parse(p[3]),p[4]);
        }
    }
}
