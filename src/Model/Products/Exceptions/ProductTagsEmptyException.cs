using System;

namespace Searcher.Model.Products.Exceptions;

public sealed class ProductTagsEmptyException : Exception
{
    public ProductTagsEmptyException()
        : base("Product must have at least one tag.")
    { }
}