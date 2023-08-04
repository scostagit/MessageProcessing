using JPMorgan.MessageProcessing.Dtos;
using JPMorgan.MessageProcessing.Enums;
using JPMorgan.MessageProcessing.Services;

namespace JPMorgan.MessageProcessing.UnitTest
{

    public class SalesProcessorServiceUnitTests
    {
        [Fact]
        public void SalesProcessorService_Process_ShouldBePausedAfter50Messages()
        {
            //Arrange 
            var messages = new List<MessageDto>();

            for (int i = 0; i < 51; i++)
            {
                messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test", Type = MessageType.Add });
            }

            var salesProcessorService = new SalesProcessorService();

            //Act
            salesProcessorService.Process(messages);

            //Assert
            Assert.True(salesProcessorService.IsPause);
        }


        [Fact]
        public void SalesProcessorService_Process_MessageTypeAdd()
        {
            //Arrange 
            var messages = new List<MessageDto>();
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 01", Type = MessageType.Add });
            messages.Add(new MessageDto { Price = 50, Quantity = 3, Product = "Test 01", Type = MessageType.Add });


            var salesProcessorService = new SalesProcessorService();

            //Act
            salesProcessorService.Process(messages);

            //Assert
            Assert.Equal(4, actual: salesProcessorService.Sales.First().Quantity);
            Assert.Equal(60, actual: salesProcessorService.Sales.First().Price);
            Assert.Equal(240, actual: salesProcessorService.Sales.First().Total);
        }

        
        [Fact]
        public void SalesProcessorService_Process_MessageTypeMultipley()
        {
            //Arrange 
            var messages = new List<MessageDto>();
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 01", Type = MessageType.Multiply });
            messages.Add(new MessageDto { Price = 50, Quantity = 3, Product = "Test 01", Type = MessageType.Multiply });


            var salesProcessorService = new SalesProcessorService();

            //Act
            salesProcessorService.Process(messages);

            //Assert
            Assert.Equal(4, actual: salesProcessorService.Sales.First().Quantity);
            Assert.Equal(500, actual: salesProcessorService.Sales.First().Price);
            Assert.Equal(2000, actual: salesProcessorService.Sales.First().Total);
        }

        [Fact]
        public void SalesProcessorService_Process_ManyMessageTypeS()
        {
            //Arrange 
            var messages = new List<MessageDto>();
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 01", Type = MessageType.Multiply });
            messages.Add(new MessageDto { Price = 50, Quantity = 3, Product = "Test 01", Type = MessageType.Substract });
            messages.Add(new MessageDto { Price = 100, Quantity = 7, Product = "Test 01", Type = MessageType.Add });


            var salesProcessorService = new SalesProcessorService();

            //Act
            salesProcessorService.Process(messages);

            //Assert
            Assert.Equal(11, actual: salesProcessorService.Sales.First().Quantity);
            Assert.Equal(60, actual: salesProcessorService.Sales.First().Price);
            Assert.Equal(660, actual: salesProcessorService.Sales.First().Total);
        }


        [Fact]
        public void SalesProcessorService_Process_ManyProducts()
        {
            //Arrange 
            var messages = new List<MessageDto>();
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 01", Type = MessageType.Add });
            messages.Add(new MessageDto { Price = 50, Quantity = 3, Product = "Test 02", Type = MessageType.Add });
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 01", Type = MessageType.Add });
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 01", Type = MessageType.Add });
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 01", Type = MessageType.Add });
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 01", Type = MessageType.Add });
            messages.Add(new MessageDto { Price = 10, Quantity = 1, Product = "Test 03", Type = MessageType.Add });


            var salesProcessorService = new SalesProcessorService();

            //Act
            salesProcessorService.Process(messages);

            //Assert
            Assert.Equal(3, actual: salesProcessorService.Sales.Count);         
        }
    }
}
