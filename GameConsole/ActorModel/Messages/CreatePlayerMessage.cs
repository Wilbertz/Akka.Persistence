namespace GameConsole.ActorModel.Messages
{
    internal class CreatePlayerMessage
    {
        public string PlayerName { get; }

        public CreatePlayerMessage(string playerName)
        {
            PlayerName = playerName;
        }
    }
}