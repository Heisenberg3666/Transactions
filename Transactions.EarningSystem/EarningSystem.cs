using Exiled.API.Features;
using System;
using Transactions.EarningSystem.Events;

namespace Transactions.EarningSystem
{
    public class EarningSystem : Plugin<Config>
    {
        private PlayerEvents _playerEvents;

        public static EarningSystem Instance;

        public override string Name => "Transactions.EarningSystem";
        public override string Author => "Heisenberg3666";
        public override Version Version => new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion => new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Instance = this;

            _playerEvents = new PlayerEvents(Config);

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            _playerEvents = null;

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
