using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        //[ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("product.add,admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public Result Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductsCountOfCategoryId(product.CategoryId),
                CheckIfCategoryLimitExcepded());
            if(result!=null)
            {
                return new ErrorResult();
            }

            _productDal.Add(product);
            return new SuccessResult("Ekleme basarili");
        }
        [CacheRemoveAspect("IProductService.Get")]
        public Result Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult("Silme basarili");
        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),"Urunler donduruldu.");
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == id));
        }

        public IDataResult<List<ProductDetailDto>> GetProductsDetails(ProductDetailDto productDetailDto)
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),"Urunlerin detaylari ektedir.");
        }

        [CacheRemoveAspect("IProductService.Get")]
        public Result Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult("Guncelleme basarili.");
        }

        private IResult CheckIfProductsCountOfCategoryId(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            
            if(result >= 15)
            {
                return new ErrorResult("Kategorideki Max Ürün Sayisina Ulasildi.");
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();

            if(result)
            {
                return new ErrorResult("Ayni isimde urun var.");
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExcepded()
        {
            var result = _categoryService.GetAll();

            if(result.Data.Count > 15)
            {
                return new ErrorResult("Kategori limitine ulasildi.");
            }

            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
