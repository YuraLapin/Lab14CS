using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14Main
{
    public class CollectionHandlerEventsArgs<T>: System.EventArgs
    {
        public string Name;
        public string Change;
        public T? Source;

        public CollectionHandlerEventsArgs(string _name, string _change, T? _source)
        {            
            Name = _name;
            Change = _change;
            Source = _source;
        }

        public override string ToString()
        {
            if (Source == null)
            {
                return "name - " + Name.ToString() + ", change - " + Change.ToString() + ", source - [ null ]";
            }
            return "name - " + Name.ToString() + ", change - " + Change.ToString() + ", source - [" + Source.ToString() + "]";
        }
    }
}