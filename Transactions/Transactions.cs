using ConsentManager.API;
using ConsentManager.API.Entities;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using LiteDB;
using RemoteAdmin;
using System;
using Transactions.Commands;
using Transactions.Config;
using Transactions.Events;

namespace Transactions
{
    public class Transactions : Plugin<BaseConfig, Translation>
    {
        private PlayerEvents _playerEvents;

        internal Guid _apiKey;

        public static Transactions Instance;
        public LiteDatabase Database;
        public BaseCommand ParentCommand;

        public override string Name { get; } = "Transactions";
        public override string Author { get; } = "Heisenberg3666";
        public override Version Version { get; } = new Version(2, 1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Instance = this;
            Database = new LiteDatabase(Config.DatabasePath);

            _apiKey = PluginRegistration.Register(new PluginUsage()
            {
                Name = Name,
                Version = Version,
                DataUsage = "Transactions will store the amount of in-game money that a player has and a reference to the player (so the in-game money can be found).",
                WhoCanSeeData = "Transactions plugin will see the data, people who you give in-game money to will also see how much you have given them."
            });

            _playerEvents = new PlayerEvents(Config, Translation, _apiKey);

            RegisterEvents();

            CustomItem.RegisterItems();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomItem.UnregisterItems();

            UnregisterEvents();

            _playerEvents = null;

            PluginRegistration.Unregister(_apiKey);
            _apiKey = Guid.Empty;

            Database.Dispose();
            Database = null;

            Instance = null;

            base.OnDisabled();
        }

        public override void OnRegisteringCommands()
        {
            ParentCommand = new BaseCommand();

            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(ParentCommand);
            GameCore.Console.singleton.ConsoleCommandHandler.RegisterCommand(ParentCommand);
            QueryProcessor.DotCommandHandler.RegisterCommand(ParentCommand);
        }

        public override void OnUnregisteringCommands()
        {
            CommandProcessor.RemoteAdminCommandHandler.UnregisterCommand(ParentCommand);
            GameCore.Console.singleton.ConsoleCommandHandler.UnregisterCommand(ParentCommand);
            QueryProcessor.DotCommandHandler.UnregisterCommand(ParentCommand);

            ParentCommand = null;
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
