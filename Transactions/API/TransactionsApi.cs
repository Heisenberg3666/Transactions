using Exiled.API.Features;
using Exiled.API.Features.Items;
using Transactions.API.Entities;
using Transactions.API.Interfaces;
using Transactions.Commands;
using Transactions.Config;

namespace Transactions.API
{
    public static class TransactionsApi
    {
        /// <summary>
        /// Checks if a player exists in the database.
        /// </summary>
        /// <param name="player"></param>
        /// <returns><see cref="bool"/></returns>
        public static bool PlayerExists(Player player) => Transactions.Instance.Database.GetCollection<PlayerData>().Exists(x => x.UserId == player.UserId);

        /// <summary>
        /// Spawns a coin that gives player money when picked up.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public static Pickup SpawnCoin(Player position, int money)
        {
            return Transactions.Instance.Config.Coin.Spawn(position, money);
        }

        /// <summary>
        /// Register a command to the <see cref="BaseCommand"/>.
        /// </summary>
        /// <param name="command"></param>
        public static void RegisterSubcommand(IUsageCommand command)
        {
            BaseCommand._instance.RegisterCommand(command);
        }

        /// <summary>
        /// Unregister a command from the <see cref="BaseCommand"/>.
        /// </summary>
        /// <param name="command"></param>
        public static void UnregisterSubcommand(IUsageCommand command)
        {
            BaseCommand._instance.UnregisterCommand(command);
        }

        /// <summary>
        /// Formats the integer input into a string customised in the <see cref="BaseConfig"/>.
        /// </summary>
        /// <param name="money"></param>
        /// <returns><see cref="string"/></returns>
        public static string FormatMoney(int money)
        {
            return Transactions.Instance.Translation.MoneyFormat
                .Replace("%money%", money.ToString());
        }

        /// <summary>
        /// Gets the amount of money that a player has.
        /// </summary>
        /// <param name="player"></param>
        /// <returns><see cref="int"/></returns>
        public static int GetMoney(Player player)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);

            return playerData.Money;
        }

        /// <summary>
        /// Sets the amount of money that a player has.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="money"></param>
        public static void SetMoney(Player player, int money)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Money = money;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);
        }

        /// <summary>
        /// Adds an amount of money to a player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="money"></param>
        public static void AddMoney(Player player, int money)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Money += money;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);
        }

        /// <summary>
        /// Removes money from a player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="money"></param>
        public static void RemoveMoney(Player player, int money)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Money -= money;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);
        }

        /// <summary>
        /// Adds a player to the database.
        /// </summary>
        /// <param name="player"></param>
        public static void AddPlayer(Player player)
        {
            PlayerData playerData = new PlayerData()
            {
                UserId = player.UserId,
                Money = Transactions.Instance.Config.StartingMoney
            };

            Transactions.Instance.Database.GetCollection<PlayerData>().Insert(playerData);
        }

        /// <summary>
        /// Removes a player from the database.
        /// </summary>
        /// <param name="player"></param>
        public static void RemovePlayer(Player player)
        {
            Transactions.Instance.Database.GetCollection<PlayerData>().Delete(player.UserId);
        }
    }
}
