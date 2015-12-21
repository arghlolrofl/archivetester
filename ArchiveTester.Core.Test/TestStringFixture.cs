using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchiveTester.Core.Test
{
  [TestClass]
  public class TestStringFixture
  {
    [TestMethod]
    public void TestIncrement1()
    {
      Ascii.SetStringMode(StringMode.Lower);
      TestString str = new TestString(4, "aa");

      str.Increment();

      Assert.AreEqual("ab", str.Value);
    }

    [TestMethod]
    public void TestIncrement2()
    {
      Ascii.SetStringMode(StringMode.Lower);
      TestString str = new TestString(4, "a");

      str.Increment();

      Assert.AreEqual("b", str.Value);
    }

    [TestMethod]
    public void TestIncrement3()
    {
      Ascii.SetStringMode(StringMode.Lower | StringMode.Upper | StringMode.Numbers);
      TestString str = new TestString(4, "a9");

      str.Increment(3);

      Assert.AreEqual("bc", str.Value);
    }

    [TestMethod]
    public void TestIncrement4()
    {
      Ascii.SetStringMode(StringMode.Lower | StringMode.Upper | StringMode.Numbers);
      TestString str = new TestString(4, "9");

      str.Increment();

      Assert.AreEqual("aa", str.Value);
    }

    [TestMethod]
    public void TestOverflowLastIndex()
    {
      Ascii.SetStringMode(StringMode.Lower);
      TestString str = new TestString(4, "aaz");

      str.Increment();

      Assert.AreEqual("aba", str.Value);
    }

    [TestMethod]
    public void TestOverflowTwoModes()
    {
      Ascii.SetStringMode(StringMode.Lower | StringMode.Upper);
      TestString str = new TestString(4, "aBZ");

      str.Increment();

      Assert.AreEqual("aCa", str.Value);
    }

    [TestMethod]
    public void TestOverflowThreeModes()
    {
      Ascii.SetStringMode(StringMode.Lower | StringMode.Upper | StringMode.Numbers);
      TestString str = new TestString(4, "a39");

      str.Increment();

      Assert.AreEqual("a4a", str.Value);
    }

    [TestMethod]
    public void TestLastStringPermutation()
    {
      Ascii.SetStringMode(StringMode.Lower | StringMode.Upper | StringMode.Numbers);
      TestString str = new TestString(3, "999");

      bool hasFinished = !str.Increment();

      Assert.IsTrue(hasFinished);
    }

    //[TestMethod]
    //public void TestStringIncrementPerformance() {
    //    Ascii.SetStringMode(StringMode.Lower | StringMode.Upper | StringMode.Numbers);
    //    TestString str = new TestString(3, "aaa");

    //    int distance = 0;

    //    Stopwatch sw = new Stopwatch();
    //    sw.Start();

    //    while (str.Increment())
    //        distance++;

    //    sw.Stop();

    //    Assert.IsTrue(sw.ElapsedMilliseconds < 100);
    //    Assert.AreEqual(238327, distance);
    //}
  }
}
