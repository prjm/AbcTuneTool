using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using AbcTuneTool.Model;

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

    /// <summary>
    ///     list pool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListPool<T> : ObjectPool<List<T>> {

        /// <summary>
        ///     clear the list
        /// </summary>
        /// <param name="objectPoolItem"></param>
        public override void ClearItem(ObjectPoolItem<List<T>> objectPoolItem)
            => objectPoolItem.Item.Clear();
    }

    /// <summary>
    ///     list pools
    /// </summary>
    public class ListPools {

        /// <summary>
        ///     object list
        /// </summary>
        public ListPool<object> ObjectLists { get; }
            = new ListPool<object>();

        /// <summary>
        ///     token lists
        /// </summary>
        public ListPool<Token> TokenLists { get; }
            = new ListPool<Token>();

        /// <summary>
        ///     get an object list
        /// </summary>
        /// <returns></returns>
        public ObjectPoolItem<List<object>> GetObjectList()
            => ObjectLists.GetItem();


        /// <summary>
        ///     get an token list
        /// </summary>
        /// <returns></returns>
        public ObjectPoolItem<List<Token>> GetTokenList()
            => TokenLists.GetItem();
    }

    /// <summary>
    ///     helper class for object pools
    /// </summary>
    public static class ObjectPoolHelpers {

        /// <summary>
        ///     add an item to the list pool
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="item"></param>
        public static void Add(this ObjectPoolItem<List<object>> pool, object item)
            => pool.Item.Add(item);

        /// <summary>
        ///     add an item to the token list pool
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="item"></param>
        public static void Add(this ObjectPoolItem<List<Token>> pool, in Token item)
            => pool.Item.Add(item);

        /// <summary>
        ///     convert this list to an immutable array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static ImmutableArray<T> ToImmutableArray<T>(this ObjectPoolItem<List<object>> list) {

            var l = list.Item;

            return l.Count switch
            {
                0 => ImmutableArray<T>.Empty,
                1 => ImmutableArray.Create((T)l[0]),
                2 => ImmutableArray.Create((T)l[0], (T)l[1]),
                3 => ImmutableArray.Create((T)l[0], (T)l[1], (T)l[2]),
                4 => ImmutableArray.Create((T)l[0], (T)l[1], (T)l[2], (T)l[3]),
                5 => ImmutableArray.Create((T)l[0], (T)l[1], (T)l[2], (T)l[3], (T)l[4]),
                6 => ImmutableArray.Create((T)l[0], (T)l[1], (T)l[2], (T)l[3], (T)l[4], (T)l[5]),
                7 => ImmutableArray.Create((T)l[0], (T)l[1], (T)l[2], (T)l[3], (T)l[4], (T)l[5], (T)l[6]),
                _ => l.Cast<T>().ToImmutableArray()
            };
        }

        /// <summary>
        ///     convert this list to an immutable array
        /// </summary>
        /// <param name="list"></param>
        public static ImmutableArray<Token> ToImmutableArray(this ObjectPoolItem<List<Token>> list) {

            var l = list.Item;

            return l.Count switch
            {
                0 => ImmutableArray<Token>.Empty,
                1 => ImmutableArray.Create(l[0]),
                2 => ImmutableArray.Create(l[0], l[1]),
                3 => ImmutableArray.Create(l[0], l[1], l[2]),
                4 => ImmutableArray.Create(l[0], l[1], l[2], l[3]),
                5 => ImmutableArray.Create(l[0], l[1], l[2], l[3], l[4]),
                6 => ImmutableArray.Create(l[0], l[1], l[2], l[3], l[4], l[5]),
                7 => ImmutableArray.Create(l[0], l[1], l[2], l[3], l[4], l[5], l[6]),
                _ => l.ToImmutableArray()
            };
        }

    }
}
