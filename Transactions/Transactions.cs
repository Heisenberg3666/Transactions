using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using LiteDB;
using RemoteAdmin;
using System;
using Transactions.Commands;
using Transactions.Events;

namespace Transactions
{
    public class Transactions : Plugin<Config>
    {
        private PlayerEvents _playerEvents;

        public static Transactions Instance;
        public LiteDatabase Database;
        public BaseCommand ParentCommand;

        public override string Name { get; } = "Transactions";
        public override string Author { get; } = "Heisenberg3666";
        public override Version Version { get; } = new Version(2, 0, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Instance = this;
            Database = new LiteDatabase(Config.DatabasePath);

            _playerEvents = new PlayerEvents(Config);

            RegisterEvents();

            CustomItem.RegisterItems();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomItem.UnregisterItems();

            UnregisterEvents();

            _playerEvents = null;

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
