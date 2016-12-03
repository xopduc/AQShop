using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AQShop.Data.Repositoties;
using AQShop.Data.Infrastruture;
using AQShop.Service;
using AQShop.Model.Models;

namespace AQShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _categoryRepository;
        private Mock<IUnitOfWork> _unitOfWork ;
        private PostCategoryService _categoryService;
        private List<PostCategory> _postCategoryList;
        [TestInitialize]
        public void Initialize()
        {
            _categoryRepository = new Mock<IPostCategoryRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_categoryRepository.Object, _unitOfWork.Object);
            _postCategoryList = new List<PostCategory>()
            {
                new PostCategory() {ID =1, Name= "MD1" },
                new PostCategory() {ID =2, Name= "MD2" },
                new PostCategory() {ID =3, Name= "MD3" },
            };
        }

        [TestMethod]
        public void PostCategoryService__GetAll()
        {
            //setup method
            _categoryRepository.Setup(m => m.GetAll(null)).Returns(_postCategoryList);

            //TEST Method 
            var result = _categoryService.GetAll() as List<PostCategory>;

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void PostCategory_Create()
        {
            PostCategory category = new PostCategory();
            int id = 1;
            category.Name = "Test2";
            category.Alias = "TEst22";

            //setup
            _categoryRepository.Setup(m => m.Add(category)).Returns((PostCategory p)
                  =>
            {
                p.ID = 1;
                return p;
            });
            var result = _categoryService.Add(category);

            Assert.AreEqual(1, result.ID);
        }
    }
}
