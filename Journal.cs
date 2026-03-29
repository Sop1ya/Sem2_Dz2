using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2_Dz2
{
    internal class Journal<T> where T : IJournalEntry
    {
        private List<T> entries = new List<T>();

        public void Add(T entry)
        {
            entries.Add(entry);
        }

        public List<T> GetAll()
        {
            return entries;
        }

        public void SaveToFile(string fileName)
        {
            List<string> lines = new List<string>();

            foreach (T e in entries)
                lines.Add(e.ToLogLine());

            File.WriteAllLines(fileName, lines);
        }
    }
}
