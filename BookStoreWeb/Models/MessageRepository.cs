using Microsoft.Extensions.Options;

namespace BookStoreWeb.Models
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _newbookalertconfig;
        //private  NewBookAlertConfig _newbookalertconfig;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newbookalertconfig)
        {
            _newbookalertconfig = newbookalertconfig;
            //   _newbookalertconfig = newbookalertconfig.CurrentValue;
            //newbookalertconfig.OnChange(config =>
            //{
            //    _newbookalertconfig = config;
            //});
        }
        public string Name()
        {
            return _newbookalertconfig.CurrentValue.BookName;
        }
    }
}