using Exiled.API.Features;
using System.Collections.Generic;
using Transactions.API.Entities;
using Transactions.API.Interfaces;
using Transactions.Commands;

namespace Transactions.API
{
    public static class TransactionsApi
    {
        public static bool PlayerExists(Player player) => Transactions.Instance.Database.GetCollection<PlayerData>().Exists(x => x.UserId == player.UserId);

        public static void RegisterSubcommands(IEnumerable<IUsageCommand> commands)
        {
            BaseCommand.Instance.Commands.AddRange(commands);

            foreach (IUsageCommand command in commands)
                BaseCommand.Instance.RegisterCommand(command);
        }

        public static string FormatPoints(int points)
        {
            return Transactions.Instance.Config.PointsFormat
                .Replace("%points%", points.ToString());
        }

        public static int GetPoints(Player player)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);

            return playerData.Points;
        }

        public static void SetPoints(Player player, int points)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Points = points;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);
        }

        public static void AddPoints(Player player, int points)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Points += points;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);

            player.ShowHint($"<color=green>+{FormatPoints(points)}</color>", 10);
        }

        public static void RemovePoints(Player player, int points)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Points -= points;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);

            player.ShowHint($"<color=red>-{FormatPoints(points)}</color>", 10);
        }

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

        public static void RemovePlayer(Player player)
        {
            Transactions.Instance.Database.GetCollection<PlayerData>().Delete(player.UserId);
        }
    }
}
