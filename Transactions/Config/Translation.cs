using Exiled.API.Interfaces;
using System.ComponentModel;

namespace Transactions.Config
{
    public class Translation : ITranslation
    {
        [Description("The message that will be used to prompt players with DNT enabled to disable it. Leave empty to disable.")]
        public string DNTPlayerPrompt { get; set; } = "You have DNT enabled! Disable DNT to use the Transactions plugin.";

        [Description("This is the format that players will see their money in.")]
        public string MoneyFormat { get; set; } = "£%money%";
    }
}
