using AQShop.Data.Infrastruture;
using AQShop.Data.Repositoties;
using AQShop.Model.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQShop.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        IDbFactory dbFactory;
        IUnitOfWork unitOfWork;
        IPostCategoryRepository postCategoryRepository;
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            postCategoryRepository = new PostCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repoistory_GetAll()
        {
            var list = postCategoryRepository.GetAll().ToList();
            Assert.AreEqual(1, list.Count());
        }

        [TestMethod]
        public void PostCategory_Repository_create()
        {
            PostCategory category = new PostCategory();

            category.Name = "test";
            category.Alias = "testalias";
            
            var result= postCategoryRepository.Add(category);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
