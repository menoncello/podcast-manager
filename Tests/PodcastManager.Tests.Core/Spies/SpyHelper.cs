using FluentAssertions;

namespace PodcastManager.Tests.Spies;

public class SpyHelper
{
    private int timesCalled;
    
    public void Call()
    {
        timesCalled++;
        Console.WriteLine(timesCalled);
    }

    public void ShouldBeCalledOnce()
    {
        timesCalled.Should().Be(1);
    }

    public void ShouldBeCalled(int times)
    {
        timesCalled.Should().Be(times);
    }
}

public class SpyHelper<T> : SpyHelper
{
    private readonly IList<T> parameters = new List<T>();

    public IEnumerable<T> Parameters => parameters;
    public T LastParameter => parameters[^1];

    public void Call(T parameter)
    {
        parameters.Add(parameter);
        base.Call();
    }
}
