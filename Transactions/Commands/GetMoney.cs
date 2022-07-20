using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using Transactions.API;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    internal class GetMoney : IUsageCommand
    {
        public string Command { get; } = nameof(GetMoney).ToLower();
        public string[] Aliases { get; } = new string[] { "get" };
        public string Description { get; } = "Get the money that a player has.";
        public string[] Usage { get; } = new string[] { "Name" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string permission = $"{nameof(Transactions)}.{Command}";

            if (!sender.CheckPermission(permission))
            {
                response = $"You do not have the permission to use this command ({permission}).";
                return false;
            }

            Player player;

            if (arguments.Count < 1)
                player = Player.Get(sender);
            else
                player = Player.Get(arguments.At(0));

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

            int money = TransactionsApi.GetMoney(player);
            response = TransactionsApi.FormatMoney(money);
            return true;
        }
    }
}
