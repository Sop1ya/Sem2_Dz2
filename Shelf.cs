using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2_Dz2
{
    internal class Shelf
    {
        private string[] slots;

        public Shelf(int size)
        {
            slots = new string[size];
        }

        public string Get(int index)
        {
            return slots[index];
        }

        public void Put(int index, string item)
        {
            slots[index] = item;
        }

        public string Take(int index)
        {
            string item = slots[index];
            slots[index] = null;
            return item;
        }

        

        public void Print(string name)
        {
            Console.Write($"Полка {name}: ");

            for (int i = 0; i < slots.Length; i++)
            {
                string value = slots[i];
                if (value == null)
                {
                    value = "пусто";
                }

                Console.Write($"[{i + 1}] {value}  ");
            }

            Console.WriteLine();
        }
    }
}
