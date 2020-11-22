namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     syntax tree visitor
    /// </summary>
    public interface ISyntaxTreeVisitor {


    }

    /// <summary>
    ///     syntax tree visitor
    /// </summary>
    /// <typeparam name="T">type of nodes to visit</typeparam>
    public interface ISyntaxTreeStartVisitor<T> : ISyntaxTreeVisitor where T : ISyntaxTreeElement {

        /// <summary>
        ///     start visiting a node
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool StartVisit(T element);

    }

    /// <summary>
    ///     syntax tree end visitor
    /// </summary>
    /// <typeparam name="T">node type</typeparam>
    public interface ISyntaxTreeEndVisitor<T> : ISyntaxTreeVisitor where T : ISyntaxTreeElement {

        /// <summary>
        ///     end visiting a node
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool EndVisit(T element);

    }

    /// <summary>
    ///     helper class for visitors
    /// </summary>
    public static class VisitorHelper {

        /// <summary>
        ///     start visit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="visitor"></param>
        /// <param name="data"></param>
        public static bool StartVisitNode<T>(this ISyntaxTreeVisitor visitor, T data) where T : ISyntaxTreeElement {
            var v = visitor as ISyntaxTreeStartVisitor<T>;
            if (v != default)
                return v.StartVisit(data);
            else
                return true;
        }

        /// <summary>
        ///     end visit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="visitor"></param>
        /// <param name="data"></param>
        public static bool EndVisitNode<T>(this ISyntaxTreeVisitor visitor, T data) where T : ISyntaxTreeElement {
            var v = visitor as ISyntaxTreeEndVisitor<T>;

            if (v != default)
                return v.EndVisit(data);
            else
                return true;
        }


    }

}
