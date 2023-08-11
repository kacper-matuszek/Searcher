using Searcher.Model.Shared.ValueObjects;
using System;
using Xunit;

namespace Searcher.Model.Tests.Shared.ValueObjects;

public partial class TagTests
{
    [Fact]
    public void TagEqualOperator_Should_ReturnTrue_WithSameValue()
    {
        const string tagValue = "value";

        var tag = new Tag(tagValue);
        var anotherTag = new Tag(tagValue);

        var isEqual = tag == anotherTag;

        Assert.True(isEqual);
    }

    [Fact]
    public void TagEqualOperator_Should_ReturnFalse_WithoutSameValue()
    {
        var tag = new Tag("tag");
        var anotherTag = new Tag("another-tag");

        var isEqual = tag == anotherTag;

        Assert.False(isEqual);
    }

    [Fact]
    public void TagNotEqualOperator_Should_ReturnFalse_WithSameValue()
    {
        const string tagValue = "value";

        var tag = new Tag(tagValue);
        var anotherTag = new Tag(tagValue);

        var isEqual = tag != anotherTag;

        Assert.False(isEqual);
    }

    [Fact]
    public void TagNotEqualOperator_Should_ReturnTrue_WithoutSameValue()
    {
        var tag = new Tag("tag");
        var anotherTag = new Tag("another-tag");

        var isEqual = tag != anotherTag;

        Assert.True(isEqual);
    }

    [Fact]  
    public void Tag_Should_CastToString()
    {
        var tag = new Tag("tag");

        var value = (string)tag;

        Assert.Equal(value, tag.ToString());
    }

    [Fact]
    public void CorrectStringValue_Should_CastToTag()
    {
        var value = "tag";

        var tag = (Tag)value;

        Assert.Equal(value, tag.ToString());
    }

    [Fact]
    public void InCorrectStringValue_Should_ThrowArgumentException()
    {
        var value = string.Empty;

        Assert.Throws<ArgumentException>(() => (Tag)value);
    }
}
