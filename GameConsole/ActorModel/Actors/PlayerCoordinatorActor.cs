using System;
using Akka.Actor;
using GameConsole.ActorModel.Messages;

namespace GameConsole.ActorModel.Actors
{
    internal class PlayerCoordinatorActor : ReceiveActor
    {
        private const int DefaultStartingHealth = 100;

        public PlayerCoordinatorActor()
        {
            Receive<CreatePlayerMessage>(message =>
            {
                DisplayHelper.WriteLine($"PlayerCoordinatorActor received CreatePlayerMessage for {message.PlayerName}");

                Context.ActorOf(
                    Props.Create(() =>
                                new PlayerActor(message.PlayerName, DefaultStartingHealth)), message.PlayerName);
            });
        }
    }
}