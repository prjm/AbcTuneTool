using A = Xunit.Assert;

namespace AbcTuneToolTests {
    public static class Assert {

        public static void AreEqual<T>(T expected, T testResult)
            => A.Equal<T>(expected, testResult);

    }
}
