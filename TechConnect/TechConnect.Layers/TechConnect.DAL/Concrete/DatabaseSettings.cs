using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DAL.Concrete
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public IDictionary<string, string> CollectionNames { get; set; }

        public DatabaseSettings()
        {
            CollectionNames = new Dictionary<string, string>
            {
                { "About", "AboutCollectionName" },
                { "Testimonial", "TestimonialCollectionName" },
                { "Category", "CategoryCollectionName" },
                { "Product", "ProductCollectionName" },
                { "Color", "ColorCollectionName" },
                { "Tag", "TagCollectionName" },
                { "Contact", "ContactCollectionName" },
                { "Contact2", "Contact2CollectionName" },
                { "Compare", "CompareCollectionName" },
                { "Favourite", "FavouriteCollectionName" },
                { "Discount", "DiscountCollectionName" },
                { "BasketTotal", "BasketTotalCollectionName" },
                { "Address", "AddressCollectionName" },
                { "Ordering", "OrderingCollectionName" },
                { "Comment", "CommentCollectionName" },
                { "Slider", "SliderCollectionName" },
            };
        }

    }
}
