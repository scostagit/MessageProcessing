using JPMorgan.MessageProcessing.Domain;
using JPMorgan.MessageProcessing.Dtos;
using JPMorgan.MessageProcessing.Enums;

namespace JPMorgan.MessageProcessing.Services
{
    public sealed class SalesProcessorService
    {
        private readonly int LOG_SALES_DETAILS_INDEX = 10;
        private readonly int PAUSE_APPLICATION_INDEX = 50;
        private List<Sale> _salesList = new List<Sale>();
        private int _counter = 1;
        private bool _isPaused = false;

        public bool IsPause => _isPaused;
        public List<Sale> Sales => _salesList;

        public void Process(List<MessageDto> messages)
        {
            messages.ForEach(message => this.Process(message));
        }

        public void Process(MessageDto messageDto)
        {
            if (!this._isPaused)
            {
                // Sale1: Rigester the sale if 
                var existingProduct = this._salesList.FirstOrDefault(_ => _.Product.Contains(messageDto.Product));

                if (existingProduct is null)
                {
                    _salesList.Add(new Sale(messageDto.Product, messageDto.Price, messageDto.Quantity));
                }
                else
                {
                    this.AdjustmentSales(existingProduct, messageDto);
                }

                if (_counter == LOG_SALES_DETAILS_INDEX)
                {
                    //After every 10th message received your application should log a report detailing the number
                    //of sales of each product and their total value
                    Console.WriteLine("10th message received");
                    this.GenerateReport();
                }
                else if (_counter >= PAUSE_APPLICATION_INDEX)
                {

                    //After 50 messages your application should log that it is pausing, stop accepting new
                    //messages and log a report of the adjustments that have been made to each sale type while
                    //the application was running.

                    //Pause the Processig                    
                    this._isPaused = true;

                    Console.WriteLine("50th message received");
                    this.GenerateReport();
                }

                _counter++;
            }
            else
            {
                Console.WriteLine("The JP Morgan Message Process Applicaiton is Paused");
            }
        }

        public void Restart()
        {
            this._isPaused = false;
            this._counter = 1;
        }

        private void AdjustmentSales(Sale sale, MessageDto messageDto)
        {
            switch (messageDto.Type)
            {
                case MessageType.Add:
                    sale.Adjustment((sale.Price + messageDto.Price), (sale.Quantity + messageDto.Quantity));
                    break;
                case MessageType.Substract:
                    sale.Adjustment((sale.Price - messageDto.Price), (sale.Quantity + messageDto.Quantity));
                    break;
                case MessageType.Multiply:
                    sale.Adjustment((sale.Price * messageDto.Price), (sale.Quantity + messageDto.Quantity));
                    break;
                default:
                    Console.WriteLine("AdjustmentSales Failed");
                    break;
            }
        }

        private void GenerateReport()
        {
            Console.WriteLine("Start Report ------------------------------------------------------------------------");

            foreach (var sale in _salesList)
            {
                Console.WriteLine($"Product Name: {sale.Product}, Total Quantity: {sale.Quantity}, Total Price: {String.Format("{0:C}", sale.Total)}");
            }

            var totalSales = _salesList.Sum(_ => _.Total);
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"Total Sales: {String.Format("{0:C}", totalSales)}");
        }
    }
}
