using ConsentManager.API;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using System;
using Transactions.API;
using Transactions.Config;

namespace Transactions.Events
{
    internal class PlayerEvents
    {
        private BaseConfig _config;
        private Translation _translation;
        private Guid _apiKey;

        public PlayerEvents(BaseConfig config, Translation translation, Guid apiKey)
        {
            _config = config;
            _translation = translation;
            _apiKey = apiKey;
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
            if (!ConsentManagerApi.HasPlayerGivenConsent(e.Player, _apiKey))
            {
                if (!string.IsNullOrEmpty(_translation.DNTPlayerPrompt))
                    e.Player.OpenReportWindow(_translation.DNTPlayerPrompt);

                if (TransactionsApi.PlayerExists(e.Player))
                    TransactionsApi.RemovePlayer(e.Player);
            }
            else if (!TransactionsApi.PlayerExists(e.Player))
            {
                TransactionsApi.AddPlayer(e.Player);
            }
        }

        private void OnDying(DyingEventArgs e)
        {
            if (TransactionsApi.PlayerExists(e.Target))
                TransactionsApi.RemoveMoney(e.Target, _config.MoneyDropped);

            Transactions.Instance.Config.Coin.Spawn(e.Target, _config.MoneyDropped);
        }
    }
}
