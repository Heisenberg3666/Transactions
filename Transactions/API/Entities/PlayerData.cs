using LiteDB;

namespace Transactions.API.Entities
{
    public class PlayerData
    {
        [BsonId]
        public string UserId { get; set; }

        public int Points { get; set; }
    }
}
