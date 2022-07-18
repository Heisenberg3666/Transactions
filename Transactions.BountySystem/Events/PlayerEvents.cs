﻿using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using System.Collections.Generic;
using System.Linq;
using Transactions.BountySystem.API;
using Transactions.BountySystem.API.Entities;

namespace Transactions.BountySystem.Events
{
    internal class PlayerEvents
    {
        public void RegisterEvents()
        {
            Player.Dying += OnDying;
        }

        public void UnregisterEvents()
        {
            Player.Dying -= OnDying;
        }

        private void OnDying(DyingEventArgs e)
        {
            Bounty bounty = BountySystemApi.Bounties.FirstOrDefault(x => x.TargetId == e.Target.Id);
            IEnumerable<Bounty> issuedBounties = BountySystemApi.Bounties.Where(x => x.IssuerId == e.Target.Id);

            if (bounty != null)
            {
                if (e.Killer != null && !e.Killer.DoNotTrack)
                    BountySystemApi.CompleteBounty(bounty, e.Killer);
                else
                    BountySystemApi.FailBounty(bounty);
            }

            if (issuedBounties != null)
            {
                foreach (Bounty issuedBounty in issuedBounties)
                    BountySystemApi.CancelBounty(bounty);
            }
        }
    }
}