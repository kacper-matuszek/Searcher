using Searcher.Model.Products;
using Searcher.Model.Products.Exceptions;
using Searcher.Model.Shared.ValueObjects;
using System.Collections.Generic;
using Xunit;

namespace Searcher.Model.Tests.Products;

public class ProductTests
{
    [Fact]
    public void CreateInstance_By_CorrectCtorParameters_Should_Success()
    {
        var name = "name";
        var tags = new HashSet<Tag>(1) { new Tag("tag-name") };

        var product = new Product(name, tags);

        Assert.NotNull(product);
        Assert.Equal(name, product.Name);
        Assert.True(product.Tags.Count == 1);
    }

    [Fact]
    public void CreateInstance_By_EmptyName_Should_ThrowException()
    {
        var emptyName = string.Empty;
        var tags = new HashSet<Tag>(1) { new Tag("tag-name") };

        Assert.Throws<ProductNameEmptyException>(() => new Product(emptyName, tags));
    }

    [Fact]
    public void CreateInstance_By_EmptyTags_Should_ThrowException()
    {
        var name = "name";
        var tags = new HashSet<Tag>();

        Assert.Throws<ProductTagsEmptyException>(() => new Product(name, tags));
    }
}
