using Exiled.API.Features;
using Exiled.API.Features.Items;
using System.Collections.Generic;
using Transactions.API.Entities;
using Transactions.API.Interfaces;
using Transactions.Commands;

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
        /// Spawns a coin that gives player points when picked up.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="coinValue"></param>
        /// <returns></returns>
        public static Pickup SpawnCoin(Player position, int coinValue)
        {
            return Transactions.Instance.Config.Coin.Spawn(position, coinValue);
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
        /// Register commands to the <see cref="BaseCommand"/>.
        /// </summary>
        /// <param name="commands"></param>
        public static void RegisterSubcommands(IEnumerable<IUsageCommand> commands)
        {
            foreach (IUsageCommand command in commands)
                RegisterSubcommand(command);
        }

        /// <summary>
        /// Unegister a command from the <see cref="BaseCommand"/>.
        /// </summary>
        /// <param name="command"></param>
        public static void UnregisterSubcommand(IUsageCommand command)
        {
            BaseCommand._instance.UnregisterCommand(command);
        }

        /// <summary>
        /// Unegister commands from the <see cref="BaseCommand"/>.
        /// </summary>
        /// <param name="commands"></param>
        public static void UnregisterSubcommands(IEnumerable<IUsageCommand> commands)
        {
            foreach (IUsageCommand command in commands)
                UnregisterSubcommand(command);
        }

        /// <summary>
        /// Formats the integer input into a string customised in the <see cref="Config"/>.
        /// </summary>
        /// <param name="points"></param>
        /// <returns><see cref="string"/></returns>
        public static string FormatPoints(int points)
        {
            return Transactions.Instance.Config.PointsFormat
                .Replace("%points%", points.ToString());
        }

        /// <summary>
        /// Gets the amount of points that a player has.
        /// </summary>
        /// <param name="player"></param>
        /// <returns><see cref="int"/></returns>
        public static int GetPoints(Player player)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);

            return playerData.Points;
        }

        /// <summary>
        /// Sets the amount of points that a player has.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="points"></param>
        public static void SetPoints(Player player, int points)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Points = points;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);
        }

        /// <summary>
        /// Adds an amount of points to a player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="points"></param>
        public static void AddPoints(Player player, int points)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Points += points;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);

            player.ShowHint($"<color=green>+{FormatPoints(points)}</color>", 10);
        }

        /// <summary>
        /// Removes points from a player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="points"></param>
        public static void RemovePoints(Player player, int points)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Points -= points;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);

            player.ShowHint($"<color=red>-{FormatPoints(points)}</color>", 10);
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
                Points = Transactions.Instance.Config.StartingPoints
            };

            Transactions.Instance.Database.GetCollection<PlayerData>().Insert(playerData);

            player.ShowHint($"Welcome to the server, enjoy your free points!\n<color=green>+{FormatPoints(playerData.Points)}</color>");
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
