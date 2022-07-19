using Exiled.API.Features;
using System;
using Transactions.BountySystem.Config;
using Transactions.BountySystem.Events;

namespace Transactions.BountySystem
{
    public class BountySystem : Plugin<BaseConfig>
    {
        private PlayerEvents _playerEvents;
        private ServerEvents _serverEvents;

        public static BountySystem Instance;

        public override string Name => "Transactions.BountySystem";
        public override string Author => "Heisenberg3666";
        public override Version Version => new Version(1, 0, 1, 0);
        public override Version RequiredExiledVersion => new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Instance = this;

            _playerEvents = new PlayerEvents();
            _serverEvents = new ServerEvents();

            RegisterEvents();

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnregisterEvents();

            _serverEvents = null;
            _playerEvents = null;

            Instance = null;

            base.OnDisabled();
        }
        
        public void RegisterEvents()
        {
            _playerEvents.RegisterEvents();
            _serverEvents.RegisterEvents();
        }

        public void UnregisterEvents()
        {
            _playerEvents.UnregisterEvents();
            _serverEvents.UnregisterEvents();
        }
    }
}
