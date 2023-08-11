using Searcher.Model.Shared.ValueObjects;
using System;
using Xunit;

namespace Searcher.Model.Tests;

public partial class TagTests
{
    [Fact]
    public void CorrectValue_Should_CreateInstanceOfTag()
    {
        var value = "value";

        var tag = new Tag(value);

        Assert.Equal(value, tag.Value);
    }

    [Fact]
    public void EmptyValue_Should_ThrowArgumentException()
    {
        var value = string.Empty;

        Assert.Throws<ArgumentException>(() => new Tag(value));
    }
}
