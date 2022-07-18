using Exiled.API.Features;
using System.Collections.Generic;
using Transactions.API;
using Transactions.BountySystem.API.Entities;

namespace Transactions.BountySystem.API
{
    public static class BountySystemApi
    {
        private const int _duration = 10;

        public static List<Bounty> Bounties = new List<Bounty>();

        public static void CreateBounty(Bounty newBounty)
        {
            Player issuer = Player.Get(newBounty.IssuerId);
            Player target = Player.Get(newBounty.TargetId);

            string creationMessage = BountySystem.Instance.Config.BountyMessages.Creation
                .Replace("%issuer%", issuer.Nickname)
                .Replace("%target%", target.Nickname)
                .Replace("%points%", TransactionsApi.FormatPoints(newBounty.Reward))
                .Replace("%reason%", newBounty.Reason);

            Map.Broadcast(_duration, creationMessage);

            TransactionsApi.RemovePoints(issuer, newBounty.Reward);

            Bounties.Add(newBounty);
        }

        public static void CancelBounty(Bounty bounty)
        {
            Player issuer = Player.Get(bounty.IssuerId);
            Player target = Player.Get(bounty.TargetId);

            string cancellationMessage = BountySystem.Instance.Config.BountyMessages.Cancellation
                .Replace("%issuer%", issuer.Nickname)
                .Replace("%target%", target.Nickname)
                .Replace("%points%", TransactionsApi.FormatPoints(bounty.Reward))
                .Replace("%reason%", bounty.Reason);

            Map.Broadcast(_duration, cancellationMessage);

            TransactionsApi.AddPoints(issuer, bounty.Reward);

            Bounties.Remove(bounty);
        }

        public static void CompleteBounty(Bounty bounty, Player killer)
        {
            Player issuer = Player.Get(bounty.IssuerId);
            Player target = Player.Get(bounty.TargetId);

            string completionMessage = BountySystem.Instance.Config.BountyMessages.Completion
                .Replace("%issuer%", issuer.Nickname)
                .Replace("%target%", target.Nickname)
                .Replace("%killer%", killer.Nickname)
                .Replace("%points%", TransactionsApi.FormatPoints(bounty.Reward))
                .Replace("%reason%", bounty.Reason);

            string rewardMessage = BountySystem.Instance.Config.BountyMessages.Collection
                .Replace("%issuer%", issuer.Nickname)
                .Replace("%target%", target.Nickname)
                .Replace("%killer%", killer.Nickname)
                .Replace("%points%", TransactionsApi.FormatPoints(bounty.Reward))
                .Replace("%reason%", bounty.Reason);

            Map.Broadcast(_duration, completionMessage);
            killer.ShowHint(rewardMessage, _duration);

            TransactionsApi.AddPoints(killer, bounty.Reward);

            Bounties.Remove(bounty);
        }

        public static void FailBounty(Bounty bounty)
        {
            Player issuer = Player.Get(bounty.IssuerId);
            Player target = Player.Get(bounty.TargetId);

            string failureMessage = BountySystem.Instance.Config.BountyMessages.Failure
                .Replace("%issuer%", issuer.Nickname)
                .Replace("%target%", target.Nickname)
                .Replace("%points%", TransactionsApi.FormatPoints(bounty.Reward))
                .Replace("%reason%", bounty.Reason);

            Map.Broadcast(_duration, failureMessage);

            TransactionsApi.AddPoints(issuer, bounty.Reward);

            Bounties.Remove(bounty);
        }
    }
}
