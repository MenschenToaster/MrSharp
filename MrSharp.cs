﻿using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using MrSharp.Commands;
using MrSharp.Config;

namespace MrSharp
{
    class MrSharp
    {
        private DiscordClient DiscordClient { get; set; }

        private CommandsNextModule CommandsNextModule { get; set;  }

        private ConfigManager ConfigManager { get; set;  }

        public MrSharp()
        {
            ConfigManager = new ConfigManager();
            ConfigManager.Load();
        }

        public async Task Run()
        {
            DiscordConfiguration configuration = new DiscordConfiguration
            {
                TokenType = TokenType.Bot,
                Token = ConfigManager.BaseConfig.DiscordConfig.Token,
                AutoReconnect = ConfigManager.BaseConfig.DiscordConfig.AutoReconnect,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            DiscordClient = new DiscordClient(configuration);
            DiscordClient.Ready += HandleReady;
            
            CommandsNextConfiguration commandsConfiguration = new CommandsNextConfiguration
            {
                StringPrefix = ConfigManager.BaseConfig.SystemConfig.Prefix,
                EnableDms = false,
                EnableMentionPrefix = true,
                CaseSensitive = false
            };

            CommandsNextModule = DiscordClient.UseCommandsNext(commandsConfiguration);

            CommandsNextModule.RegisterCommands<FunCommands>();
            
            await DiscordClient.ConnectAsync();
            await Task.Delay(-1);
        }
        
        private Task HandleReady(ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}