using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using Transactions.API;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    internal class RemovePoints : IUsageCommand
    {
        public string Command { get; } = nameof(RemovePoints).ToLower();
        public string[] Aliases { get; } = new string[] { "remove" };
        public string Description { get; } = "Remove points from a player.";
        public string[] Usage { get; } = new string[] { "Id", "Points" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string permission = $"{nameof(Transactions)}.{Command}";

            if (!sender.CheckPermission(permission))
            {
                response = $"You do not have the permission to use this command ({permission}).";
                return false;
            }

            if (arguments.Count < 2)
            {
                response = $"Usage: {Command} 1 500";
                return false;
            }

            Player player = Player.Get(arguments.At(0));
            int points = int.Parse(arguments.At(1));

            if (!TransactionsApi.PlayerExists(player))
            {
                response = "Player does not exist in the database, they must have DNT enabled.";
                return false;
            }

            TransactionsApi.RemovePoints(player, points);

            response = $"Removed {points} points to {player.Nickname}'s total point count.";
            return true;
        }
    }
}
