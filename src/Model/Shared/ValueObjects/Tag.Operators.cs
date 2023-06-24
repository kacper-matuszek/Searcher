namespace Searcher.Model.Shared.ValueObjects;

public readonly partial struct Tag
{
    public static bool operator ==(Tag left, Tag right) =>
        left.Equals(right);

    public static bool operator !=(Tag left, Tag right) =>
        !left.Equals(right);

    public static explicit operator string(Tag t) =>
        t.Value;

    public static explicit operator Tag(string value) =>
        new Tag(value);
}