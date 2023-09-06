using Searcher.Common.Application.Abstractions;
using Searcher.Model.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Searcher.Model.Product;

public sealed partial class Product : IKey<Guid>
{
    private readonly HashSet<Tag> _tags;
    
    public Product(
        string name,
        IReadOnlyCollection<Tag> tags)
    {
        Key = Guid.NewGuid();
        Name = name;
        _tags = tags.ToHashSet();

        Validate();
    }

    public Guid Key { get; private set; }
    public string Name { get; private set; }
    public IReadOnlySet<Tag> Tags => _tags;
}