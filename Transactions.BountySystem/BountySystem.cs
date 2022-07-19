﻿using Exiled.API.Features;
using Exiled.Loader;
using MEC;
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
        private CoroutineHandle _commandChecker;

        public static BountySystem Instance;

        public override string Name => "Transactions.BountySystem";
        public override string Author => "Heisenberg3666";
        public override Version Version => new Version(1, 1, 0, 0);
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

            _commandChecker = Timing.CallPeriodically(60f, 5f, () => 
            {
                if (Transactions.Instance != null)
                {
                    TransactionsApi.RegisterSubcommands(_commands);
                    _commandChecker.IsRunning = false;
                }
            });

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
