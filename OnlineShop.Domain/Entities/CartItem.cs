using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public  Guid ProductId {  get; set; }
        public int Quantity {  get; set; }
    }
}
