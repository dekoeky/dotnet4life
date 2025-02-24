//using ClassLibraryNetStandard21;

using ClassLibraryNetStandard20;

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
    }
}