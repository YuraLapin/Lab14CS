using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14Main
{
    public class NewCycledList<T>: CycledList<T>, INameable where T: ICloneable<T>, new()
    {
        public string Name
        {
            get;
            set;
        }

        public delegate void CollectionHandler(CollectionHandlerEventsArgs<T> args);
        public event CollectionHandler? CollectionCountChanged;
        public event CollectionHandler? CollectionReferenceChanged;

        public NewCycledList(): base()
        {
            Name = "";
        }

        public NewCycledList(string _name)
        {
            Name = _name;
        }

        public virtual void OnCollectionCountChanged(CollectionHandlerEventsArgs<T> args)
        {
            if (CollectionCountChanged != null)
            {
                CollectionCountChanged(args);
            }
        }

        public virtual void OnCollectionReferenceChanged(CollectionHandlerEventsArgs<T> args)
        {
            if (CollectionReferenceChanged != null)
            {
                CollectionReferenceChanged(args);
            }
        }

        public override bool Remove(int index)
        {
            OnCollectionCountChanged(new CollectionHandlerEventsArgs<T>(Name, "remove", this[index]));
            return base.Remove(index);
        }

        public override void Add(T? element)
        {            
            OnCollectionCountChanged(new CollectionHandlerEventsArgs<T>(Name, "add", element));
            base.Add(element);
        }

        public override T this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                OnCollectionReferenceChanged(new CollectionHandlerEventsArgs<T>(Name, "changed", value));
                base[index] = value;
            }
        }        
    }
}