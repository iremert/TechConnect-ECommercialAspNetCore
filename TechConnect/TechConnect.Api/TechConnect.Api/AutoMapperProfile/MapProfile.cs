

using AutoMapper;
using TechConnect.Dto.AboutDtos;
using TechConnect.Dto.CategoryDtos;
using TechConnect.Dto.ColorDtos;
using TechConnect.Dto.CompareDtos;
using TechConnect.Dto.Contact2Dtos;
using TechConnect.Dto.ContactDtos;
using TechConnect.Dto.DiscountDtos;
using TechConnect.Dto.FavouriteDtos;
using TechConnect.Dto.ProductDtos;
using TechConnect.Dto.TagDtos;
using TechConnect.Dto.TestimonialDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.WebUI.AutoMapperProfile
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<ResultAboutDto,About>().ReverseMap();
            CreateMap<GetByIdAboutDto,About>().ReverseMap();
            CreateMap<UpdateAboutDto,About>().ReverseMap();
            CreateMap<CreateAboutDto,About>().ReverseMap();

            CreateMap<ResultTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<GetByIdTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<UpdateTestimonialDto, Testimonial>().ReverseMap();
            CreateMap<CreateTestimonialDto, Testimonial>().ReverseMap();

            CreateMap<ResultCategoryDto, Category>().ReverseMap();
            CreateMap<GetByIdCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();

            CreateMap<ResultProductDto, Product>().ReverseMap();
            CreateMap<GetByIdProductDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();

            CreateMap<ResultColorDto, Color>().ReverseMap();
            CreateMap<GetByIdColorDto, Color>().ReverseMap();
            CreateMap<UpdateColorDto, Color>().ReverseMap();
            CreateMap<CreateColorDto, Color>().ReverseMap();

            CreateMap<ResultTagDto, Tag>().ReverseMap();
            CreateMap<GetByIdTagDto, Tag>().ReverseMap();
            CreateMap<UpdateTagDto, Tag>().ReverseMap();
            CreateMap<CreateTagDto, Tag>().ReverseMap();


            CreateMap<ResultContactDto, Contact>().ReverseMap();
            CreateMap<GetByIdContactDto, Contact>().ReverseMap();
            CreateMap<UpdateContactDto, Contact>().ReverseMap();
            CreateMap<CreateContactDto, Contact>().ReverseMap();


            CreateMap<ResultContact2Dto, Contact2>().ReverseMap();
            CreateMap<GetByIdContact2Dto, Contact2>().ReverseMap();
            CreateMap<UpdateContact2Dto, Contact2>().ReverseMap();
            CreateMap<CreateContact2Dto, Contact2>().ReverseMap();


            CreateMap<ResultFavouriteDto, Favourite>().ReverseMap();
            CreateMap<GetByIdFavouriteDto, Favourite>().ReverseMap();
            CreateMap<UpdateFavouriteDto, Favourite>().ReverseMap();
            CreateMap<CreateFavouriteDto, Favourite>().ReverseMap();


            CreateMap<ResultCompareDto, Compare>().ReverseMap();
            CreateMap<GetByIdCompareDto, Compare>().ReverseMap();
            CreateMap<UpdateCompareDto, Compare>().ReverseMap();
            CreateMap<CreateCompareDto, Compare>().ReverseMap();


            CreateMap<ResultDiscountDto, Discount>().ReverseMap();
            CreateMap<GetByIdDiscountDto, Discount>().ReverseMap();
            CreateMap<UpdateDiscountDto, Discount>().ReverseMap();
            CreateMap<CreateDiscountDto, Discount>().ReverseMap();
        }
    }
}
