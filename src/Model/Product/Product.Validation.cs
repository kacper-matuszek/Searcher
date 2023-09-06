﻿using Searcher.Model.Product.Exceptions;

namespace Searcher.Model.Product;

public sealed partial class Product
{
    private void Validate()
    {
        ValidateName();
        ValidateTags();
    }

    private void ValidateName()
    {
        if (string.IsNullOrEmpty(Name))
            throw new ProductNameEmptyException();
    }

    private void ValidateTags()
    {
        if (_tags.Count == 0)
            throw new ProductTagsEmptyException();
    }
}