using MongoDB.Driver;
using Searcher.Application.Abstractions.Products;
using Searcher.Common.Persistence.Mongo.Repositories;
using Searcher.Model.Products;
using System;

namespace Searcher.Persistence.Mongo.Products;

public sealed class ProductRepository : MongoRepository<Product, Guid>, IProductRepository
{
    public ProductRepository(IMongoDatabase database) 
        : base(database)
    {
    }

    protected override string CollectionName => "Products";
}
