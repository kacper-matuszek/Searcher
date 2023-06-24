using System;

namespace Searcher.Model.Shared.ValueObjects;

public readonly partial struct Tag
{
    public override int GetHashCode() =>
        Value.GetHashCode();

    public override bool Equals(object? obj) =>
        obj is Tag t && Equals(t);

    public bool Equals(Tag other) =>
        Value == other.Value;

    public int CompareTo(object? obj) =>
        obj is Tag t ? CompareTo(t) : 1;

    public int CompareTo(Tag other) =>
        string.Compare(Value, other.Value, StringComparison.Ordinal);
}