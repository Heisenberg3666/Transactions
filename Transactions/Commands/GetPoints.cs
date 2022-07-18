using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using Transactions.API;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    internal class GetPoints : IUsageCommand
    {
        public string Command { get; } = nameof(GetPoints).ToLower();
        public string[] Aliases { get; } = new string[] { "get" };
        public string Description { get; } = "Get the points that a player has.";
        public string[] Usage { get; } = new string[] { "Id" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string permission = $"{nameof(Transactions)}.{Command}";

            if (!sender.CheckPermission(permission))
            {
                response = $"You do not have the permission to use this command ({permission}).";
                return false;
            }

            if (arguments.Count > 1)
            {
                response = $"Usage: {Command} 1";
                return false;
            }

            Player player = Player.Get(arguments.At(0));

            if (!TransactionsApi.PlayerExists(player))
            {
                response = "Player does not exist in the database, they must have DNT enabled.";
                return false;
            }

            int points = TransactionsApi.GetPoints(player);
            response = $"\nUserId: {player.UserId}\nPoints: {points}";
            return true;
        }
    }
}
