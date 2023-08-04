using JPMorgan.MessageProcessing.Domain.Core;

namespace JPMorgan.MessageProcessing.Domain
{
    public sealed class Sale : Entity
    {
        public Sale(string product, decimal price, int quantity)
        {
            this.Product = product;
            this.Price = price;
            this.Quantity = quantity;
        }

        public string Product { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public decimal Total
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }

        public void Adjustment(decimal price, int quantity)
        {
            //TODO : Validation price and quantity

            this.Price = price;
            this.Quantity = quantity;
        }
    }
}
