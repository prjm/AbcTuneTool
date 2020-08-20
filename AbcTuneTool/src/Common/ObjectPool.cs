using System;
using System.Collections.Generic;
using System.Text;

namespace AbcTuneTool.Common {

    /// <summary>
    ///     object pool item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPoolItem<T> : IDisposable where T : new() {

        /// <summary>
        ///     create a new item
        /// </summary>
        public ObjectPoolItem(ObjectPool<T> parent) {
            Item = new T();
            Parent = parent;
        }

        /// <summary>
        ///     get the item
        /// </summary>
        public T Item { get; }

        /// <summary>
        ///     parent pool
        /// </summary>
        public ObjectPool<T> Parent { get; }

        /// <summary>
        ///     dispose this item
        /// </summary>
        public void Dispose()
            => Parent.ReturnItem(this);
    }

    /// <summary>
    ///     pool of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObjectPool<T> where T : new() {

        private readonly Queue<ObjectPoolItem<T>> poolItems = new Queue<ObjectPoolItem<T>>();

        /// <summary>
        ///     get an item from the object pool
        /// </summary>
        /// <returns></returns>
        public ObjectPoolItem<T> GetItem() {
            if (poolItems.Count > 0)
                return poolItems.Dequeue();

            var item = new ObjectPoolItem<T>(this);
            return item;
        }

        /// <summary>
        ///     return this item
        /// </summary>
        /// <param name="objectPoolItem"></param>
        public void ReturnItem(ObjectPoolItem<T> objectPoolItem) {
            ClearItem(objectPoolItem);
            if (!poolItems.Contains(objectPoolItem))
                poolItems.Enqueue(objectPoolItem);
        }

        /// <summary>
        ///     clear an item
        /// </summary>
        /// <param name="objectPoolItem"></param>
        public abstract void ClearItem(ObjectPoolItem<T> objectPoolItem);

    }


    /// <summary>
    ///     string builder pool
    /// </summary>
    public class StringBuilderPool : ObjectPool<StringBuilder> {

        /// <summary>
        ///     clear an item
        /// </summary>
        /// <param name="objectPoolItem"></param>
        public override void ClearItem(ObjectPoolItem<StringBuilder> objectPoolItem)
            => objectPoolItem.Item.Clear();
    }
}
