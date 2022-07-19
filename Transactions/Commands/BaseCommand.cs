using CommandSystem;
using System;
using System.Collections.Generic;
using Transactions.API.Interfaces;

namespace Transactions.Commands
{
    public class BaseCommand : ParentCommand
    {
        internal static BaseCommand _instance;

        public override string Command => nameof(Transactions).ToLower();
        public override string[] Aliases => new string[] { "trans" };
        public override string Description => "The base command for the Transactions plugin.";

        public BaseCommand()
        {
            _instance = this;

            LoadGeneratedCommands();
        }

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new AddPoints());
            RegisterCommand(new RemovePoints());
            RegisterCommand(new GivePoints());
            RegisterCommand(new SetPoints());
            RegisterCommand(new GetPoints());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "<color=red>Failed!</color> Available Subcommands:<color=yellow>";

            foreach (IUsageCommand command in AllCommands)
                response += $"\n{command.Command} {string.Join(" ", command.Usage)} | {command.Description} (Aliases: {string.Join(" ", command.Aliases)})";

            response += "</color>";

            return false;
        }
    }
}
