using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14Main
{
    internal class Journal<T> where T: INameable
    {
        public List<JournalEntry> list = new List<JournalEntry>();
        public void Add(CollectionHandlerEventsArgs<T> args)
        {                       
            if (args.Source == null)
            {
                list.Add(new JournalEntry(args.Name, args.Change, "null"));
            }
            else
            {
                list.Add(new JournalEntry(args.Name, args.Change, args.Source.Name));
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (JournalEntry je in list)
            {
                sb.Append(je.ToString());
            }
            if (list.Count == 0)
            {
                sb.Append("-");
            }
            sb.Append("]");
            return sb.ToString();
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }    
}