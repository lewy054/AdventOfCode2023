namespace Helpers;

public interface IResolver
{
    protected IList<string> Input { get; init; }
    public int GetResult();
}