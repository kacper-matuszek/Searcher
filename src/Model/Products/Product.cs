using Searcher.Common.Application.Abstractions;
using Searcher.Model.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Searcher.Model.Products;

public sealed partial class Product : IKey<Guid>
{
    public Product(
        string name,
        IReadOnlySet<Tag> tags)
    {
        Key = Guid.NewGuid();
        Name = name;
        Tags = tags.ToHashSet();

        Validate();
    }

    private Product()
    { }

    public Guid Key { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public IReadOnlySet<Tag> Tags { get; private set; } = new HashSet<Tag>();
}