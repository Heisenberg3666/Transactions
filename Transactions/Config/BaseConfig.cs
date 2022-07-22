using Exiled.API.Features;
using Exiled.API.Interfaces;
using System.ComponentModel;
using System.IO;
using Transactions.CustomItems;

namespace Transactions.Config
{
    public class BaseConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("The path that leads to the database file.")]
        public string DatabasePath { get; set; } = Path.Combine(Paths.Configs, "Transactions.db");

        [Description("This is the amount of money that a new player will start with.")]
        public int StartingMoney { get; set; } = 100;

        [Description("This is the amount of money that will be dropped when a player dies.")]
        public int MoneyDropped { get; set; } = 25;

        public Coin Coin { get; set; } = new Coin();
    }
}
