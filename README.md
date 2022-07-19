# Transactions

![GitHub release (latest by date)](https://img.shields.io/github/downloads/Heisenberg3666/Transactions/total?style=for-the-badge)
[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/)

Transactions is a plugin for the game SCP: SL using the Exiled framework. The core plugin acts as a base for other developers to build ontop of while reducing the potential amount of databases created for each plugin.
## Authors

- [@Heisenberg3666](https://github.com/Heisenberg3666)
## Installation

### Transactions

Download Transactions.dll from the latest release and place inside of the Plugins folder.
You will also need to download LiteDB.dll aswell so that the plugin works.
If you want to use any other plugins within this repository, you will need to install this plugin for them to work.

### Transactions.BountySystem

Download Transactions.BountySystem.dll from the latest release and place inside of the Plugins folder.
## Support

For support, please create an issue on GitHub or message me on Discord (Heisenberg#3666).
## Features

### Transactions

- Provides a base for other plugins to use to create an economy.
- Can be used to reduce the amount of databases.

### Transactions.BountySystem

- Provides players a way to spend their money.
- Adds difficulty to the gameplay.
## Developers

### Transactions API

```csharp
using Transactions.API;
...
Player player = Player.Get("Heisenberg");
TransactionsApi.SpawnCoin(player, 100);
...
if (TransactionsApi.PlayerExists(player)) // Checks if a player exists within the database
{
    Log.Debug(TransactionsApi.GetPoints(player)); // Gets the amount of points that a player has
    TransactionsApi.AddPoints(player, 100); // Give points to a player
    Log.Debug(TransactionsApi.FormatPoints(100)); // Formats the integer into a string customised in the Config
}
else
{
    TransactionsApi.AddPlayer(player); // Registers a player in the database if they are not in there
    TransactionsApi.SetPoints(player, 100); // Sets the amount of points a player has
}
...
TransactionsApi.RemovePoints(player, 100); // Removes points from a player
TransactionsApi.RemovePlayer(player); // Removes a player from the database.
```

### Transactions.BountySystem API

```csharp
using Transactions.BountySystem.API;
...
Player issuer = Player.Get("Heisenberg");
Player target = Player.Get("Gabe Newell");
Player killer = Player.Get("Hubert Moszka");

Bounty bounty = new Bounty()
{
    IssuerId = issuer.Id,
    TargetId = target.Id,
    Reward = 100,
    Reason = "Because he owns Valve and I don't."
};

BountySystemApi.CreateBounty(bounty);
Bounty myBounty = BountySystemApi.Bounties.FirstOrDefault(x => x.IssuerId == issuer.Id);
...

// There are many ways to end a bounty, here are some:
BountySystemApi.CancelBounty(bounty);
BountySystemApi.CompleteBounty(bounty);
BountySystemApi.FailBounty(bounty);
```
## License

[GNU](https://choosealicense.com/licenses/gpl-3.0/)
