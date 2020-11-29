namespace GameConsole.ActorModel.Messages
{
    internal class HitMessage
    {
        public int Damage { get; }

        public HitMessage(int damage)
        {
            Damage = damage;
        }
    }
}