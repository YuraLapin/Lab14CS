using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14Main
{
    internal class JournalEntry
    {
        public string Name;
        public string ChangeType;
        public string Changed;

        public JournalEntry(string _name, string _changeType, string _changed)
        {
            Name = _name;
            ChangeType = _changeType;
            Changed = _changed;
        }

        public override string ToString()
        {
            return "name - " + Name + ", changeType - " + ChangeType + ", changed - " + Changed;
        }
    }
}