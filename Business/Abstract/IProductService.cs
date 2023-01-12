using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<Product> GetById(int id);
        Result Add(Product product);
        Result Delete(Product product);
        Result Update(Product product);
        IDataResult<List<ProductDetailDto>> GetProductsDetails(ProductDetailDto productDetailDto);

        IResult AddTransactionalTest(Product product);
    }
}
