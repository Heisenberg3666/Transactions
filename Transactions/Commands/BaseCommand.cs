using CommandSystem;
using System;
using System.Collections.Generic;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class BaseCommand : ParentCommand
    {
        internal static BaseCommand Instance;
        private static bool _alreadyCalled;

        internal List<IUsageCommand> Commands;

        public override string Command => nameof(Transactions).ToLower();
        public override string[] Aliases => new string[] { "trans" };
        public override string Description => "The base command for the Transactions plugin.";

        public BaseCommand()
        {
            // Class kept getting initialized (stops other plugins from registering subcommands).
            if (_alreadyCalled)
                return;

            _alreadyCalled = true;

            Instance = this;

            Commands = new List<IUsageCommand>()
            {
                new AddPoints(),
                new RemovePoints(),
                new GivePoints(),
                new SetPoints(),
                new GetPoints()
            };

            LoadGeneratedCommands();
        }

        public override void LoadGeneratedCommands()
        {
            foreach (IUsageCommand command in Commands)
                RegisterCommand(command);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "<color=red>Failed!</color> Available Subcommands:<color=yellow>";

            foreach (IUsageCommand command in Commands)
                response += $"\n{command.Command} {string.Join(" ", command.Usage)} | {command.Description} (Aliases: {string.Join(" ", command.Aliases)})";

            response += "</color>";

            return false;
        }
    }
}
