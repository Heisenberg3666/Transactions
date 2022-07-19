using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using Transactions.API;
using Transactions.CustomItems;

namespace Transactions.EarningSystem.Events
{
    internal class PlayerEvents
    {
        private Config _config;

        public PlayerEvents(Config config)
        {
            _config = config;
        }

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
            if (TransactionsApi.PlayerExists(e.Target))
                TransactionsApi.RemovePoints(e.Target, _config.PointsLostOnDeath);

            Transactions.Instance.Config.Coin.Spawn(e.Target, _config.PointsLostOnDeath);
        }
    }
}
