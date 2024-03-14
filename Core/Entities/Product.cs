/* remove unnecessary boilerplate usings 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

*/

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        // jpa by convention entity framework makes any property named id the primary key
        // all properties must be public for ef to gan access to the entity
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}