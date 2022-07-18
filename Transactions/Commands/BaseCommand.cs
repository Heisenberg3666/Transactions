using CommandSystem;
using System;
using System.Collections.Generic;

namespace Transactions.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class BaseCommand : ParentCommand, IUsageProvider
    {
        private IEnumerable<IUsageCommand> _commands;

        public override string Command => nameof(Transactions).ToLower();
        public override string[] Aliases => new string[] { "trans" };
        public override string Description => "The base command for the Transactions plugin.";
        public string[] Usage => new string[] { "Subcommand", "Arguments" };

        public BaseCommand()
        {
            _commands = new List<IUsageCommand>()
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
            foreach (IUsageCommand command in _commands)
                RegisterCommand(command);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "<color=red>Failed!</color> Available Subcommands:<color=yellow>";

            foreach (IUsageCommand command in _commands)
                response += $"\n{command.Command} {command.Usage} | {command.Description} (Aliases: {command.Aliases})";

            response += "</color>";

            return false;
        }
    }
}
