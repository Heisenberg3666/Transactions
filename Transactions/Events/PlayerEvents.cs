using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using Transactions.API;

namespace Transactions.Events
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
            Player.Verified += OnVerified;
            Player.Dying += OnDying;
        }

        public void UnregisterEvents()
        {
            Player.Verified -= OnVerified;
            Player.Dying -= OnDying;
        }

        private void OnVerified(VerifiedEventArgs e)
        {
            if (e.Player.DoNotTrack)
            {
                if (!string.IsNullOrEmpty(_config.DNTPlayerPrompt))
                    e.Player.OpenReportWindow(_config.DNTPlayerPrompt);

                if (TransactionsApi.PlayerExists(e.Player))
                    TransactionsApi.RemovePlayer(e.Player);

                return;
            }

            if (!TransactionsApi.PlayerExists(e.Player))
                TransactionsApi.AddPlayer(e.Player);
        }

        private void OnDying(DyingEventArgs e)
        {
            if (TransactionsApi.PlayerExists(e.Target))
                TransactionsApi.RemoveMoney(e.Target, _config.MoneyDropped);

            Transactions.Instance.Config.Coin.Spawn(e.Target, _config.MoneyDropped);
        }
    }
}
