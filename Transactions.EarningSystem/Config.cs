using Exiled.API.Interfaces;
using System.ComponentModel;

namespace Transactions.EarningSystem
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("This is the amount of points that will be lost on death.")]
        public int PointsLostOnDeath { get; set; } = 50;
    }
}
