using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Transactions.API;
using Transactions.API.Interfaces;
using Transactions.BountySystem.API;
using Transactions.BountySystem.API.Entities;

namespace Transactions.BountySystem.Commands
{
    internal class CreateBounty : IUsageCommand
    {
        public string Command { get; } = nameof(CreateBounty).ToLower();
        public string[] Aliases { get; } = new string[] { "cb" };
        public string Description { get; } = "Use this command to place a bounty on players.";
        public string[] Usage { get; } = new string[] { "Name", "Points", "Reason" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string permission = $"{nameof(Transactions)}.{nameof(BountySystem)}.{Command}";

            if (!sender.CheckPermission(permission))
            {
                response = $"You do not have the correct permission to run this command ({permission}).";
                return false;
            }

            if (arguments.Count < 3)
            {
                response = $"Usage: {Command} Heisenberg 500 Called me a name";
                return false;
            }

            if (!TransactionsApi.PlayerExists(Player.Get(sender)))
            {
                response = "You are not registered in the database, you must have DNT enabled.";
                return false;
            }

            Player player = Player.Get(arguments.At(0));

            if (player == null)
            {
                response = "You have not specified a valid player.";
                return false;
            }

            string reason = string.Join(" ", arguments.Skip(2));

            Bounty bounty = new Bounty()
            {
                IssuerId = Player.Get(sender).Id,
                TargetId = player.Id,
                Reward = int.Parse(arguments.At(1)),
                Reason = reason
            };

            BountySystemApi.CreateBounty(bounty);

            response = $"You have sucessfully placed a bounty on {player.Nickname}";
            return true;
        }
    }
}
