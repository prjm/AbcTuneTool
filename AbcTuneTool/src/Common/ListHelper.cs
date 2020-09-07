using System.Collections.Generic;
using System.Linq;

namespace AbcTuneTool.Common {

    /// <summary>
    ///     list helper methods
    /// </summary>
    public static class ListHelper {

        /// <summary>
        ///     rotate a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static List<T> Rotate<T>(this List<T> list, int offset)
            => list.Skip(offset).Concat(list.Take(offset)).ToList();
    }
}
