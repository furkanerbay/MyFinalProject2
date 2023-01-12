using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public Result Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult("Kategori eklendi.");
        }

        public Result Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult("Kategori silindi.");
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(),"Kategori listesinin tamami getirildi.");
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == id)) ;
        }

        public Result Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult("Guncelleme islemi basarili.");
        }
    }
}
