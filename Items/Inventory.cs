using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Zomgame.Items
{
    public class Inventory : IContainer
    {
        private Dictionary<int, Item> items;

        public int Count
        {
            get { return items.Count; }
        }

        public Item this[int id]
        {
            get
            {
                return items.ContainsKey(id) ? items[id] : null;
            }
        }

        public Inventory()
        {
            items = new Dictionary<int,Item>();
        }

        /// <summary>
        /// Add item to container
        /// </summary>
        /// <param name="item"></param>
        public void Add(Item item)
        {
            items[item.ID] = item;
        }

        /// <summary>
        /// Remove item and return
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Remove(int id)
        {
            if (!items.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            else
            {
                items.Remove(id);
            }
        }

        public void Give(int id, IContainer container)
        {
            if (!items.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            var item = items[id];
            this.Remove(id);
            container.Add(item);
        }



        public IEnumerator<Item> GetEnumerator()
        {
            foreach (var item in items.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
