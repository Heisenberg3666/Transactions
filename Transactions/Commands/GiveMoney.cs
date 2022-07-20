using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using Transactions.API;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    internal class GiveMoney : IUsageCommand
    {
        public string Command { get; } = nameof(GiveMoney).ToLower();
        public string[] Aliases { get; } = new string[] { "give" };
        public string Description { get; } = "Transfer some of your money to another player.";
        public string[] Usage { get; } = new string[] { "Name", "Money" };

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

            if (TransactionsApi.GetMoney(senderPlayer) < int.Parse(arguments.At(1)))
            {
                response = "You do not have sufficient money!";
                return false;
            }

            int money = int.Parse(arguments.At(1));
            int currentBalance = TransactionsApi.GetMoney(senderPlayer);

            if (money < 1 || currentBalance < money)
            {
                response = "You cannot give this amount of money to the player.";
                return false;
            }

            TransactionsApi.RemoveMoney(senderPlayer, money);
            TransactionsApi.AddMoney(player, money);

            player.ShowHint($"{senderPlayer.Nickname} has just given you {TransactionsApi.FormatMoney(money)}.");

            response = $"You have given {player.Nickname} {TransactionsApi.FormatMoney(money)}.";
            return true;
        }
    }
}
