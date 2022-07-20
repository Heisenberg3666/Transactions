using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using Transactions.API;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    internal class SetMoney : IUsageCommand
    {
        public string Command { get; } = nameof(SetMoney).ToLower();
        public string[] Aliases { get; } = new string[] { "set" };
        public string Description { get; } = "Set the amount of money that a player has.";
        public string[] Usage { get; } = new string[] { "Name", "Money" };

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

            int money = int.Parse(arguments.At(1));

            TransactionsApi.SetMoney(player, money);
            response = $"Set {player.UserId}'s balance to {TransactionsApi.FormatMoney(money)}";
            return true;
        }
    }
}
