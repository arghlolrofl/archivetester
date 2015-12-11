using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArchiveTester.Core.Test
{
  [TestClass]
  public class TestStringFixture
  {
    [TestMethod]
    public void TestLowerCaseStringIncrementsWithOverflow()
    {
      Ascii.SetStringMode(StringMode.Lower);
      TestString str = new TestString(3, "aaz");

      str.Increment();

      Assert.AreEqual("aba", str.Value);
    }

    [TestMethod]
    public void TestLowerAndUpperCaseStringIncrementsWithOverflow()
    {
      Ascii.SetStringMode(StringMode.Lower | StringMode.Upper);
      TestString str = new TestString(3, "aBZ");

      str.Increment();

      Assert.AreEqual("aCa", str.Value);
    }

    [TestMethod]
    public void TestNumberAndLowerAndUpperCaseStringIncrementsWithOverflow()
    {
      Ascii.SetStringMode(StringMode.Lower | StringMode.Upper | StringMode.Numbers);
      TestString str = new TestString(3, "a39");

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

    [TestMethod]
    public void TestStringIncrementPerformance()
    {
      Ascii.SetStringMode(StringMode.Lower | StringMode.Upper | StringMode.Numbers);
      TestString str = new TestString(3, "aaa");

      int distance = 0;

      Stopwatch sw = new Stopwatch();
      sw.Start();

      while (str.Increment())
        distance++;

      sw.Stop();

      Assert.IsTrue(sw.ElapsedMilliseconds < 100);
      Assert.AreEqual(238327, distance);
    }
  }
}
