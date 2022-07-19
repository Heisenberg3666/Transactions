﻿using Exiled.API.Features;
using System;
using System.Collections.Generic;
using Transactions.API;
using Transactions.API.Interfaces;
using Transactions.BountySystem.Commands;
using Transactions.BountySystem.Config;
using Transactions.BountySystem.Events;

namespace Transactions.BountySystem
{
    public class BountySystem : Plugin<BaseConfig>
    {
        private PlayerEvents _playerEvents;
        private ServerEvents _serverEvents;
        private List<IUsageCommand> _commands;

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

            _commands = new List<IUsageCommand>()
            {
                new CreateBounty(),
                new CancelBounty()
            };

            TransactionsApi.RegisterSubcommands(_commands);

            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            TransactionsApi.UnregisterSubcommands(_commands);

            _commands = null;

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
