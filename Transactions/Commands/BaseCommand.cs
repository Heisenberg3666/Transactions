using CommandSystem;
using System;

namespace Transactions.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal class BaseCommand : ParentCommand
    {
        public override string Command { get; }
        public override string[] Aliases { get; }
        public override string Description { get; }

        public BaseCommand()
        {
            LoadGeneratedCommands();
        }

        public override void LoadGeneratedCommands()
        {
            throw new NotImplementedException();
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Commands: ";
            return false;
        }
    }
}
