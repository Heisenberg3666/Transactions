using Exiled.API.Interfaces;

namespace Transactions.BountySystem.Config
{
    public class BaseConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public MessageConfig BountyMessages { get; set; } = new MessageConfig();
    }
}
