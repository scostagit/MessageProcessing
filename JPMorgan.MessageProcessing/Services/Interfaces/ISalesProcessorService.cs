using JPMorgan.MessageProcessing.Domain;
using JPMorgan.MessageProcessing.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPMorgan.MessageProcessing.Services.Interfaces
{
    public interface ISalesProcessorService
    {
        public void Process(List<MessageDto> messages);
        public void Process(MessageDto messageDto);
        public void Restart();
        public void GenerateReport();
    }
}
