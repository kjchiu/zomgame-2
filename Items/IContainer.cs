using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zomgame.Items
{
    public interface IContainer : IEnumerable<Item>
    {
        Item this[int id]
        {
            get;
        }

        int Count { get; }
        void Add(Item item);
        void Remove(int id);
        void Give(int id, IContainer container);
    }
}
