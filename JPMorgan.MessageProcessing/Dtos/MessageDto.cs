using JPMorgan.MessageProcessing.Enums;

namespace JPMorgan.MessageProcessing.Dtos
{
    public class MessageDto
    {
        public MessageType Type { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
