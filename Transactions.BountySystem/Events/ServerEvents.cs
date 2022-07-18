using Exiled.Events.Handlers;
using System.Collections.Generic;
using Transactions.API;
using Transactions.API.Interfaces;
using Transactions.BountySystem.Commands;

namespace Transactions.BountySystem.Events
{
    internal class ServerEvents
    {
        public void RegisterEvents()
        {
            Server.WaitingForPlayers += OnWaitingForPlayers;
        }

        public void UnregisterEvents()
        {
            Server.WaitingForPlayers -= OnWaitingForPlayers;
        }

        private void OnWaitingForPlayers()
        {
            TransactionsApi.RegisterSubcommands(new List<IUsageCommand>()
            {
                new CreateBounty(),
                new CancelBounty()
            });
        }
    }
}
