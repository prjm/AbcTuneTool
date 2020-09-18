namespace AbcTuneTool.Model {

    /// <summary>
    ///     part repetition
    /// </summary>
    public class PartRepeat : PartItem {

        /// <summary>
        ///     create a new part repeat
        /// </summary>
        /// <param name="repeat"></param>
        public PartRepeat(int repeat)
            => Repeat = repeat;

        /// <summary>
        ///     number of repetitions
        /// </summary>
        public int Repeat { get; }
    }
}
