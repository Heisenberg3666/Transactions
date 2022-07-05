using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using LiteDB;
using Transactions.API;
using Transactions.API.Entities;

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
        }

        public void UnregisterEvents()
        {
            Player.Verified -= OnVerified;
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
    }
}
