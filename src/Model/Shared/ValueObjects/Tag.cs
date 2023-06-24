using System;

namespace Searcher.Model.Shared.ValueObjects;

public readonly partial struct Tag : IEquatable<Tag>, IComparable<Tag>, IComparable
{
    public Tag(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;

    public Tag Add(Tag other) =>
        new Tag(Value + other.Value);

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty.");
    }
}