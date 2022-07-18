using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using Transactions.API;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    internal class SetPoints : IUsageCommand
    {
        public string Command { get; } = nameof(SetPoints).ToLower();
        public string[] Aliases { get; } = new string[] { "set" };
        public string Description { get; } = "Set the amount of points that a player has.";
        public string[] Usage { get; } = new string[] { "Name", "Points" };

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
                response = $"Usage: {Command} Heisenberg 500";
                return false;
            }

            Player player = Player.Get(arguments.At(0));

            if (!TransactionsApi.PlayerExists(player))
            {
                response = "Player does not exist in the database, they must have DNT enabled.";
                return false;
            }

            TransactionsApi.SetPoints(player, int.Parse(arguments.At(1)));
            response = $"\nUserId: {player.UserId}\nPoints: {arguments.At(1)}";
            return true;
        }
    }
}
