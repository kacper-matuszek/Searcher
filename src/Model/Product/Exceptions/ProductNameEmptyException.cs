using System;

namespace Searcher.Model.Product.Exceptions;

public sealed class ProductNameEmptyException : Exception
{
    public ProductNameEmptyException()
        : base("Product name cannot be empty.")
    {
    }
}