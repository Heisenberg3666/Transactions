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
            RegisterCommand(new AddMoney());
            RegisterCommand(new RemoveMoney());
            RegisterCommand(new GiveMoney());
            RegisterCommand(new SetMoney());
            RegisterCommand(new GetMoney());
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
