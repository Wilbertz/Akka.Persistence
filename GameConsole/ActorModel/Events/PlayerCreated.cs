﻿namespace GameConsole.ActorModel.Events
{
    public class PlayerCreated
    {
        public string PlayerName { get; }

        public PlayerCreated(string playerName)
        {
            PlayerName = playerName;
        }
    }
}
