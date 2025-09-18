using SharedLibrary.Reflection.Assignables;

namespace QuickTests.PatternMatching;

[TestClass]
public class PatternMatchingTests
{
    private static readonly Dog Lassie = new("Lassie");
    private static readonly Cat Garfield = new("Garfield");
    private static readonly Animal LassieTheAnimal = Lassie;
    private static readonly Animal GarfieldTheAnimal = Garfield;
    private static readonly IAnimal LassieTheIAnimal = Lassie;
    private static readonly IAnimal GarfieldTheIAnimal = Garfield;

    [TestMethod]
    public void LassieTheAnimal_Is_Dog() => Assert.IsTrue(LassieTheAnimal is Dog);

    [TestMethod]
    public void LassieTheAnimal_Is_Cat() => Assert.IsFalse(LassieTheAnimal is Cat);

    [TestMethod]
    public void GarfieldTheAnimal_Is_Dog() => Assert.IsFalse(GarfieldTheAnimal is Dog);

    [TestMethod]
    public void GarfieldTheAnimal_Is_Cat() => Assert.IsTrue(GarfieldTheAnimal is Cat);


    [TestMethod]
    public void LassieTheIAnimal_Is_Dog() => Assert.IsTrue(LassieTheIAnimal is Dog);

    [TestMethod]
    public void LassieTheIAnimal_Is_Cat() => Assert.IsFalse(LassieTheIAnimal is Cat);

    [TestMethod]
    public void LassieTheIAnimal_Is_Animal() => Assert.IsTrue(LassieTheIAnimal is Animal);

    [TestMethod]
    public void GarfieldTheIAnimal_Is_Dog() => Assert.IsFalse(GarfieldTheIAnimal is Dog);

    [TestMethod]
    public void GarfieldTheIAnimal_Is_Cat() => Assert.IsTrue(GarfieldTheIAnimal is Cat);

    [TestMethod]
    public void GarfieldTheIAnimal_Is_Animal() => Assert.IsTrue(GarfieldTheIAnimal is Animal);


    [TestMethod]
    public void LassieTheAnimal_Is_Dog_Result()
    {
        if (LassieTheAnimal is not Dog dog)
            Assert.Fail();
        else
            Assert.IsNotNull(dog);
    }

    [TestMethod]
    public void GarfieldTheAnimal_Is_Cat_Result()
    {
        if (GarfieldTheAnimal is not Cat cat)
            Assert.Fail();
        else
            Assert.IsNotNull(cat);
    }

    [TestMethod]
    public void LassieTheIAnimal_Is_Dog_Result()
    {
        if (LassieTheIAnimal is not Dog dog)
            Assert.Fail();
        else
            Assert.IsNotNull(dog);
    }

    [TestMethod]
    public void LassieTheIAnimal_Is_Animal_Result()
    {
        if (LassieTheIAnimal is not Animal animal)
            Assert.Fail();
        else
            Assert.IsNotNull(animal);
    }

    [TestMethod]
    public void GarfieldTheIAnimal_Is_Cat_Result()
    {
        if (GarfieldTheIAnimal is not Cat cat)
            Assert.Fail();
        else
            Assert.IsNotNull(cat);
    }

    [TestMethod]
    public void GarfieldTheIAnimal_Is_Animal_Result()
    {
        if (GarfieldTheIAnimal is not Animal animal)
            Assert.Fail();
        else
            Assert.IsNotNull(animal);
    }
}