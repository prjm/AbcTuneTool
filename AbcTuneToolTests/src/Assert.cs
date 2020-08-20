using System.Diagnostics.CodeAnalysis;
using A = Xunit.Assert;

namespace AbcTuneToolTests {
    public static class Assert {

        public static void AreEqual<T>(T expected, T testResult)
            => A.Equal<T>(expected, testResult);

        public static void NotNull<T>([NotNull] T? value) where T : class
#pragma warning disable CS8777 // Parameter must have a non-null value when exiting.
            => A.NotNull(value);
#pragma warning restore CS8777 // Parameter must have a non-null value when exiting.

    }
}
