/* remove unnecessary boilerplate usings 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

*/

namespace Core.Entities
{
    public class Product
    {
        // jpa by convention entity framework makes any property named id the primary key
        // all properties must be public for ef to gan access to the entity
        public int Id { get; set; }
        public string Name { get; set; }
    }
}