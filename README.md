# Transactions

![GitHub release (latest by date)](https://img.shields.io/github/downloads/Heisenberg3666/Transactions/total?style=for-the-badge)
[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/)

Transactions is an SCP: SL plugin using the Exiled framework. The plugin is supposed to be used as a base for other developers to build ontop of.

## Authors

- [@Heisenberg3666](https://github.com/Heisenberg3666)

## Installation

Download Transactions.dll from the latest release and place inside of the Plugins folder.
You will also need to download LiteDB.dll aswell so that the plugin works.

## Support

For support, please create an issue on GitHub or message me on Discord (Heisenberg#3666).

## Features

- Provides a base for other plugins to use to create an economy.
- Can be used to reduce the amount of databases.

## Developers

### API Examples

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

## License

[GPLv3](https://choosealicense.com/licenses/gpl-3.0/)
