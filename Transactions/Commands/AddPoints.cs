﻿using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using Transactions.API;

namespace Transactions.Commands
{
    internal class AddPoints : ICommand
    {
        public string Command { get; } = nameof(AddPoints).ToLower();
        public string[] Aliases { get; } = new string[] { "add" };
        public string Description { get; } = "Add points to a player.";

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

            TransactionsApi.AddPoints(player, points);

            response = $"Added {points} points to {player.Nickname}'s total point count.";
            return true;
        }
    }
}