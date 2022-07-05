using Exiled.API.Features;
using Transactions.API.Entities;

namespace Transactions.API
{
    public static class TransactionsApi
    {
        public static bool PlayerExists(Player player) => Transactions.Instance.Database.GetCollection<PlayerData>().Exists(x => x.UserId == player.UserId);

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
        }

        public static void RemovePoints(Player player, int points)
        {
            PlayerData playerData = Transactions.Instance.Database.GetCollection<PlayerData>().FindById(player.UserId);
            playerData.Points -= points;

            Transactions.Instance.Database.GetCollection<PlayerData>().Update(playerData);
        }

        public static void AddPlayer(Player player)
        {
            PlayerData playerData = new PlayerData()
            {
                UserId = player.UserId,
                Points = Transactions.Instance.Config.StartingPoints
            };

            Transactions.Instance.Database.GetCollection<PlayerData>().Insert(playerData);
        }

        public static void RemovePlayer(Player player)
        {
            Transactions.Instance.Database.GetCollection<PlayerData>().Delete(player.UserId);
        }
    }
}
