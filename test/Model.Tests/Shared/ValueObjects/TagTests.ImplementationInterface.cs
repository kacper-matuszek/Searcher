using Searcher.Model.Shared.ValueObjects;
using Xunit;

namespace Searcher.Model.Tests.Shared.ValueObjects;

public partial class TagTests
{
    [Fact]
    public void Tag_Should_Equals_AnoterTag_WithSameValue()
    {
        const string tagValue = "value";

        var tag = new Tag(tagValue);
        var anotherTag = new Tag(tagValue);

        var isEqual = tag.Equals(anotherTag);

        Assert.True(isEqual);
    }

    [Fact]
    public void Tag_Should_NotEquals_AnotherTag_WithoutSameValue()
    {
        var tag = new Tag("tag");
        var anotherTag = new Tag("another-tag");

        var isEqual = tag.Equals(anotherTag);

        Assert.False(isEqual);
    }

    [Fact]
    public void TagCompare_Should_EqualsZero()
    {
        const string tagValue = "tag";

        var tag = new Tag(tagValue);
        var anotherTag = new Tag(tagValue);

        var compareResult = tag.CompareTo(anotherTag);

        Assert.True(compareResult == 0);
    }

    [Fact]  
    public void TagCompare_Should_ReturnPositiveValue()
    {
        var tag = new Tag("value");
        var anotherTag = new Tag("another-tag");

        var compareResult = tag.CompareTo(anotherTag);

        Assert.True(compareResult > 0);
    }

    [Fact]
    public void TagCompare_Should_ReturnNegativeValue()
    {
        var tag = new Tag("value");
        var anotherTag = new Tag("another-tag");

        var compareResult = anotherTag.CompareTo(tag);

        Assert.True(compareResult < 0);
    }
}
