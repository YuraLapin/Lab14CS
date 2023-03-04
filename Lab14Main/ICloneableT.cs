using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14Main
{
    public interface ICloneable<T>
    {
        abstract public T Clone();
    }
}