using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2_Dz2
{
    internal class FailedAttemptEvent : IJournalEntry
    {   
        private string operation;
        private string shelf;
        private int slot;
        private string reason;



        public string Operation 
        { 
            get
            {
                return operation;
            }
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
        public string Reason 
        { 
            get
            { 
                return reason;
            }
        }

        public FailedAttemptEvent(string op, string shelf, int slot, string reason)
        {
            this.operation = op;
            this.shelf = shelf;
            this.slot = slot;
            this.reason = reason;
        }

        public string ToLogLine()
        {
            return $"{Operation}|{Shelf}|{Slot}|{Reason}";
        }

        public string ToScreenLine()
        {
            return $"Неудача | {Operation} | полка {Shelf} слот {Slot} | причина: {Reason}";
        }

        public static FailedAttemptEvent FromLogLine(string line)
        {
            string[] p = line.Split('|');
            return new FailedAttemptEvent(p[0],p[1],int.Parse(p[2]),p[3]);
        }
    }
}
