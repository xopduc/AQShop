using AQShop.Data.Infrastruture;
using AQShop.Data.Repositoties;
using AQShop.Model.Models;
using System.Collections.Generic;

namespace AQShop.Service
{
    public interface IPostSevice
    {
        void Add(Post post);

        void Update(Post post);

        void Delete(int id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetPaging(int page, int size, out int totalRow);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int size, out int totalRow);

        Post GetById(int id);

        void Commit();
    }

    public class PostService : IPostSevice
    {
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepoitory, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepoitory;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Post post)
        {
            _postRepository.Add(post);
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new[] { "PostCategory" });
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public IEnumerable<Post> GetPaging(int page, int size, out int totalRow)
        {
            return _postRepository.GetMultiPaging(null, out totalRow, page, size);
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int size, out int totalRow)
        {
            return _postRepository.GetAllByTag(tag, page, size,out totalRow);
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }
    }
}