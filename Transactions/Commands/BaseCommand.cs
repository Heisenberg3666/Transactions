using CommandSystem;
using System;

namespace Transactions.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class BaseCommand : ParentCommand
    {
        public override string Command { get; } = nameof(Transactions).ToLower();
        public override string[] Aliases { get; } = Array.Empty<string>();
        public override string Description { get; } = "The base command for the Transactions plugin.";

        public BaseCommand()
        {
            LoadGeneratedCommands();
        }

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new AddPoints());
            RegisterCommand(new GetPoints());
            RegisterCommand(new GivePoints());
            RegisterCommand(new RemovePoints());
            RegisterCommand(new SetPoints());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Commands: addpoints | getpoints | givepoints | removepoints | setpoints";
            return false;
        }
    }
}
