using ClassLibraryNetStandard20;
//using ClassLibraryNetStandard21;

namespace Tests_.NET
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void ClassLibraryNetStandard20_Class1_Test()
        {
            //ARRANGE
            var data = new Class20();

            //ACT
            var json = data.ToJson();

            //ASSERT
            Console.WriteLine(json);

        }


        [TestMethod]
        public void TestClassLibraryNetStandard21_Class1_TestMethod1()
        {
            ////ARRANGE
            //var data = new Class21();

            ////ACT
            //var json = data.ToJson();

            ////ASSERT
            //Console.WriteLine(json);
        }
    }
}
