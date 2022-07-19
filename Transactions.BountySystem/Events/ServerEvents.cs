using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using System.Collections.Generic;
using Transactions.API;
using Transactions.API.Interfaces;
using Transactions.BountySystem.API;
using Transactions.BountySystem.API.Entities;
using Transactions.BountySystem.Commands;

namespace Transactions.BountySystem.Events
{
    internal class ServerEvents
    {
        public void RegisterEvents()
        {
            Server.WaitingForPlayers += OnWaitingForPlayers;
            Server.EndingRound += OnRoundEnded;
        }

        public void UnregisterEvents()
        {
            Server.WaitingForPlayers -= OnWaitingForPlayers;
            Server.EndingRound -= OnRoundEnded;
        }

        private void OnWaitingForPlayers()
        {
            TransactionsApi.RegisterSubcommands(new List<IUsageCommand>()
            {
                new CreateBounty(),
                new CancelBounty()
            });
        }

        private void OnRoundEnded(EndingRoundEventArgs e)
        {
            if (BountySystemApi.Bounties.Count > 0)
            {
                foreach (Bounty bounty in BountySystemApi.Bounties)
                    BountySystemApi.FailBounty(bounty);
            }
        }
    }
}
