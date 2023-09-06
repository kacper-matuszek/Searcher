using Searcher.Common.Application.Abstractions;
using Searcher.Model.Products;
using System;

namespace Searcher.Application.Abstractions.Products;

public interface IProductRepository : IRepository<Product, Guid>
{
}
