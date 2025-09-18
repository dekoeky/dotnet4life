using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using PerformanceTests.Exceptions;
using SharedLibrary.Reflection.Assignables;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace PerformanceTests.Benchmarks.PatternMatching;

[ShortRunJob]
[MemoryDiagnoser]
public class PatternMatchingBenchmarks
{
    private readonly Consumer _consumer = new();

    private Dog _lassie;
    private Cat _garfield;
    private Animal _lassieTheAnimal;
    private Animal _garfieldTheAnimal;
    private IAnimal _lassieTheIAnimal;
    private IAnimal _garfieldTheIAnimal;

    [GlobalSetup]
    public void Setup()
    {
        _lassie = new Dog("Lassie");
        _garfield = new Cat("Garfield");
        _lassieTheAnimal = _lassie;
        _garfieldTheAnimal = _garfield;
        _lassieTheIAnimal = _lassie;
        _garfieldTheIAnimal = _garfield;
    }


    [Benchmark] public bool LassieTheAnimal_Is_Dog() => _lassieTheAnimal is Dog;
    [Benchmark] public bool LassieTheAnimal_Is_Cat() => _lassieTheAnimal is Cat;
    [Benchmark] public bool GarfieldTheAnimal_Is_Dog() => _garfieldTheAnimal is Dog;
    [Benchmark] public bool GarfieldTheAnimal_Is_Cat() => _garfieldTheAnimal is Cat;


    [Benchmark] public bool LassieTheIAnimal_Is_Dog() => _lassieTheIAnimal is Dog;
    [Benchmark] public bool LassieTheIAnimal_Is_Cat() => _lassieTheIAnimal is Cat;
    [Benchmark] public bool LassieTheIAnimal_Is_Animal() => _lassieTheIAnimal is Animal;
    [Benchmark] public bool GarfieldTheIAnimal_Is_Dog() => _garfieldTheIAnimal is Dog;
    [Benchmark] public bool GarfieldTheIAnimal_Is_Cat() => _garfieldTheIAnimal is Cat;
    [Benchmark] public bool GarfieldTheIAnimal_Is_Animal() => _garfieldTheIAnimal is Animal;


    [Benchmark]
    public bool LassieTheAnimal_Is_Dog_Result()
    {
        if (_lassieTheAnimal is not Dog dog)
            throw new ExpectedSuccessException();

        _consumer.Consume(dog);

        return true;
    }


    [Benchmark]
    public bool LassieTheAnimal_Is_Cat_Result()
    {
        if (_lassieTheAnimal is Cat cat)
            throw new ExpectedFailureException();

        return false;
    }


    [Benchmark]
    public bool GarfieldTheAnimal_Is_Dog_Result()
    {
        if (_garfieldTheAnimal is Dog dog)
            throw new ExpectedFailureException();

        return false;
    }

    [Benchmark]
    public bool GarfieldTheAnimal_Is_Cat_Result()
    {
        if (_garfieldTheAnimal is not Cat cat)
            throw new ExpectedSuccessException();

        _consumer.Consume(cat);

        return true;
    }


    [Benchmark]
    public bool LassieTheIAnimal_Is_Dog_Result()
    {
        if (_lassieTheIAnimal is not Dog dog)
            throw new ExpectedSuccessException();

        _consumer.Consume(dog);

        return true;
    }

    [Benchmark]
    public bool LassieTheIAnimal_Is_Cat_Result()
    {
        if (_lassieTheIAnimal is Cat cat)
            throw new ExpectedFailureException();

        return false;
    }

    [Benchmark]
    public bool LassieTheIAnimal_Is_Animal_Result()
    {
        if (_lassieTheIAnimal is not Animal animal)
            throw new ExpectedSuccessException();

        _consumer.Consume(animal);

        return true;
    }

    [Benchmark]
    public bool GarfieldTheIAnimal_Is_Dog_Result()
    {
        if (_garfieldTheIAnimal is Dog dog)
            throw new ExpectedFailureException();

        return false;
    }

    [Benchmark]
    public bool GarfieldTheIAnimal_Is_Cat_Result()
    {
        if (_garfieldTheIAnimal is not Cat cat)
            throw new ExpectedSuccessException();

        _consumer.Consume(cat);

        return true;
    }

    [Benchmark]
    public bool GarfieldTheIAnimal_Is_Animal_Result()
    {
        if (_garfieldTheIAnimal is not Animal animal)
            throw new ExpectedSuccessException();

        _consumer.Consume(animal);

        return true;
    }
}