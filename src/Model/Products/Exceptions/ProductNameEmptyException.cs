using System;

namespace Searcher.Model.Products.Exceptions;

public sealed class ProductNameEmptyException : Exception
{
    public ProductNameEmptyException()
        : base("Product name cannot be empty.")
    {
    }
}