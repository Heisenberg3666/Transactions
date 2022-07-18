using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using Transactions.API;

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
            Player.Died += OnDied;
        }

        public void UnregisterEvents()
        {
            Player.Died -= OnDied;
        }

        private void OnDied(DiedEventArgs e)
        {
            if (TransactionsApi.PlayerExists(e.Target))
                TransactionsApi.RemovePoints(e.Target, _config.PointsLostOnDeath);

            if (e.Killer != null && TransactionsApi.PlayerExists(e.Killer))
                TransactionsApi.AddPoints(e.Killer, _config.PointsLostOnDeath);
        }
    }
}
