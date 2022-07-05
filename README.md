# Transactions

![GitHub release (latest by date)](https://img.shields.io/github/downloads/Heisenberg3666/Transactions/total?style=for-the-badge)

Transactions is a plugin for SCP: SL using the Exiled framework. This plugin adds points in the game. The intended use is for other plugins to use this as a base for their points system.

## Default config:

```yaml
transactions:
  is_enabled: true
  # The path that leads to the database file.
  database_path: container/home/.config/EXILED/Configs/Transactions.db
  # The message that will be used to prompt players with DNT enabled to disable it. Leave empty to disable.
  d_n_t_player_prompt: You have DNT enabled! Disable DNT to use the Transactions plugin.
  # This is the amount of points that a new player will start with.
  starting_points: 100
```