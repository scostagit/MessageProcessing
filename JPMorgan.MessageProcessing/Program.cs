// See https://aka.ms/new-console-template for more information
using JPMorgan.MessageProcessing.Dtos;
using JPMorgan.MessageProcessing.Enums;
using JPMorgan.MessageProcessing.Services;
using JPMorgan.MessageProcessing.Services.Interfaces;

var messageList = new List<MessageDto>();
messageList.Add(new MessageDto { Product = "IPhone12", Quantity = 1, Price = 1000, Type = MessageType.Add });
messageList.Add(new MessageDto { Product = "IPhone12", Quantity = 1, Price = 1200, Type = MessageType.Add });
messageList.Add(new MessageDto { Product = "Macbook Pro", Quantity = 1, Price = 3000, Type = MessageType.Add });
messageList.Add(new MessageDto { Product = "Macbook Pro", Quantity = 1, Price = 3100, Type = MessageType.Multiply });
messageList.Add(new MessageDto { Product = "Macbook Pro 2", Quantity = 3, Price = 4000, Type = MessageType.Add });
messageList.Add(new MessageDto { Product = "IPhone12", Quantity = 7, Price = 7000, Type = MessageType.Add });
messageList.Add(new MessageDto { Product = "IPhone12", Quantity = 1, Price = 1200, Type = MessageType.Add });
messageList.Add(new MessageDto { Product = "IPhone12", Quantity = 1, Price = 1200, Type = MessageType.Add });
messageList.Add(new MessageDto { Product = "IPhone12", Quantity = 1, Price = 1200, Type = MessageType.Add });
messageList.Add(new MessageDto { Product = "IPhone12", Quantity = 1, Price = 1200, Type = MessageType.Add });

ISalesProcessorService salesProcessorSerivice = new SalesProcessorService();

foreach (var message in messageList)
{
    salesProcessorSerivice.Process(message);
}


Console.WriteLine("Hello, World!");
