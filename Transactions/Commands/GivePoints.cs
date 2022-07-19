using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using Transactions.API;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    internal class GivePoints : IUsageCommand
    {
        public string Command { get; } = nameof(GivePoints).ToLower();
        public string[] Aliases { get; } = new string[] { "give" };
        public string Description { get; } = "Transfer some of your points to another player.";
        public string[] Usage { get; } = new string[] { "Name", "Points" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string permission = $"{nameof(Transactions)}.{Command}";
            Player senderPlayer = Player.Get(sender);

            if (!sender.CheckPermission(permission))
            {
                response = $"You do not have the permission to use this command ({permission}).";
                return false;
            }

            if (arguments.Count < 2)
            {
                response = $"Usage: {Command} {string.Join(" ", Usage)}";
                return false;
            }

            Player player = Player.Get(arguments.At(0));

            if (player == null)
            {
                response = "You have not specified a valid player.";
                return false;
            }

            if (!TransactionsApi.PlayerExists(player))
            {
                response = "Player does not exist in the database, they must have DNT enabled.";
                return false;
            }

            if (TransactionsApi.GetPoints(senderPlayer) < int.Parse(arguments.At(1)))
            {
                response = "You do not have sufficient points!";
                return false;
            }

            int points = int.Parse(arguments.At(1));
            int currentBalance = TransactionsApi.GetPoints(senderPlayer);

            if (currentBalance < points)
            {
                response = "You cannot afford to give this amount of points to the player.";
                return false;
            }

            TransactionsApi.RemovePoints(senderPlayer, points);
            TransactionsApi.AddPoints(player, points);

            player.ShowHint($"{senderPlayer.Nickname} has just given you {points} points.");

            response = $"You have given {player.Nickname} {points} points.";
            return true;
        }
    }
}
