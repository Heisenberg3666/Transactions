using Exiled.API.Features;
using Exiled.API.Interfaces;
using System.ComponentModel;
using System.IO;
using Transactions.CustomItems;

namespace Transactions
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("The path that leads to the database file.")]
        public string DatabasePath { get; set; } = Path.Combine(Paths.Configs, "Transactions.db");

        [Description("The message that will be used to prompt players with DNT enabled to disable it. Leave empty to disable.")]
        public string DNTPlayerPrompt { get; set; } = "You have DNT enabled! Disable DNT to use the Transactions plugin.";

        [Description("This is the amount of points that a new player will start with.")]
        public int StartingPoints { get; set; } = 100;

        [Description("This is the format that players will see their money in.")]
        public string PointsFormat { get; set; } = "£%points%";

        public Coin Coin { get; set; } = new Coin();
    }
}
