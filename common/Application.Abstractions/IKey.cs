namespace Searcher.Application.Abstractions;

public interface IKey<out TKey> 
    where TKey : notnull
{
    public TKey Key { get; }
}