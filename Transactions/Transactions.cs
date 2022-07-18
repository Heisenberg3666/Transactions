using Exiled.API.Features;
using LiteDB;
using System;
using Transactions.Events;

namespace Transactions
{
    public class Transactions : Plugin<Config>
    {
        private PlayerEvents _playerEvents;

        public static Transactions Instance;
        public LiteDatabase Database;

        public override string Name => "Transactions";
        public override string Author => "Heisenberg3666";
        public override Version Version => new Version(1, 0, 1, 0);
        public override Version RequiredExiledVersion => new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Instance = this;
            Database = new LiteDatabase(Config.DatabasePath);
            _playerEvents = new PlayerEvents(Config);

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            _playerEvents = null;

            Database.Dispose();
            Database = null;

            Instance = null;

            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            _playerEvents.RegisterEvents();
        }

        public void UnregisterEvents()
        {
            _playerEvents.UnregisterEvents();
        }
    }
}
