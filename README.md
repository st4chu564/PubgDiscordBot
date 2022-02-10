# Pubg DiscordBot 
Pubg DiscrodBot is a simple bot for discord, with the ability to interact with Player Uknown: Battleground API and get data from it. It is still work in progress and right now, it allows to get lifetime stats for a player calling the command, selected player by adding his name and to get calling player last match with links to chickendinner.gg and pubglookup.com

## Installation![Badge](https://img.shields.io/badge/state-working-success)

To run this application, it is required to have [.Net 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed and `PubgApiKey` and `DiscordBotToken` for the bot to be filled in `appsettings.json`. `PubgApiKey` can be obtained by using [Pubg Apps page](https://developer.pubg.com/apps) and creating an account there. For the `DiscordBotToken`, it has to be generated per server per bot, .Net sample how to create one can be found [here](https://discordnet.dev/guides/getting_started/first-bot.html).

Then, when using Linux it can be run using and that's it. 

```bash
dotnet build
dotnet run
```

When using Windows and Visual Studio (or other IDE of your choice) it has to be run as a console application.

## Usage

Following commands are supported

```bash
import foobar

#Returns list of available commands and how to use them
/pubg help 

#Returns a short info about last match with links for more detailed insights
/pubg lastMatch

#Returns player selected lifetime statistics (list of statistics can be obtained by the help call)
/pubg stats {statName}

#Returns lifetime player stats
/pubg playerStats
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License
[MIT](https://choosealicense.com/licenses/mit/)
