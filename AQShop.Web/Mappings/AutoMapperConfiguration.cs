using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

using AQShop.Web.Models;
using AQShop.Model.Models;
using AutoMapper;

namespace AQShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<PostTag, PostTagViewModel>();
            Mapper.CreateMap<Post, PostCategoryViewModel>();
            Mapper.CreateMap<Tag,TagViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<ProductViewModel, Product>();
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<ProductCategoryViewModel, ProductCategory>();
            Mapper.CreateMap<ProductTag, ProductTagViewModel>();
            Mapper.CreateMap<Footer, FooterViewModel>();
            Mapper.CreateMap<Slide, SlideViewModel>();
            Mapper.CreateMap<Page, PageViewModel>();
            Mapper.CreateMap<PageViewModel, Page>();
        }
    }
}
