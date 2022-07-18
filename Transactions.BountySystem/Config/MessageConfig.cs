﻿using System.ComponentModel;

namespace Transactions.BountySystem.Config
{
    public class MessageConfig
    {
        [Description("This is the message that will be shown everyone when a bounty has been placed. Available variables: %points% %issuer% %target% %reason%")]
        public string Creation { get; set; } = "%issuer% has placed a bounty of %points% on %target%'s head (Reason: %reason%)";

        [Description("This is the message that will be shown everyone when a bounty has been cancelled. Available variables: %points% %issuer% %target% %reason%")]
        public string Cancellation { get; set; } = "%issuer% has cancelled their bounty on %target%.";

        [Description("This is the message that will be shown to the player who collect the bounty. Available variables: %points% %issuer% %killer% %target% %reason%")]
        public string Collection { get; set; } = "You have collected your bounty of %points%.";

        [Description("This is the message that will be shown to everyone when the bounty has been completed. Available variables: %points% %issuer% %killer% %target% %reason%")]
        public string Completion { get; set; } = "The bounty issued by %issuer% has been completed by %killer%";

        [Description("This is the message that will be shown to everyone when the bounty was not completed. Available variables: %points% %issuer% %target% %reason%")]
        public string Failure { get; set; } = "The bounty issued by %issuer% has failed.";
    }
}
