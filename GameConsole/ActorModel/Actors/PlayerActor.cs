using System;
using Akka.Actor;
using Akka.Persistence;
using GameConsole.ActorModel.Commands;
using GameConsole.ActorModel.Events;

namespace GameConsole.ActorModel.Actors
{
    class PlayerActor : ReceivePersistentActor
    {
        private readonly string _playerName;
        private int _health;        

        public PlayerActor(string playerName, int startingHealth)
        {
            _playerName = playerName;
            _health = startingHealth;

            DisplayHelper.WriteLine($"{_playerName} created");

            Command<HitPlayer>(command => HitPlayer(command));
            Command<DisplayStatus>(command => DisplayPlayerStatus());
            Command<SimulateError>(command => SimulateError());

            Recover<PlayerHit>(message =>
            {
                DisplayHelper.WriteLine($"{_playerName} replaying PlayerHit {message} from journal, updating actor state");
                _health -= message.DamageTaken;
            });
        }

        public override string PersistenceId => $"command-{_playerName}";

        private void HitPlayer(HitPlayer command)
        {
            DisplayHelper.WriteLine($"{_playerName} received HitPlayer command");
            
            var @event = new PlayerHit(command.Damage);

            DisplayHelper.WriteLine($"{_playerName} persisting PlayerHit event");

            Persist(@event, playerHitEvent =>
            {
                DisplayHelper.WriteLine($"{_playerName} persisted PlayerHit event ok, updating actor state");
                _health -= @event.DamageTaken;
            });
        }

        private void DisplayPlayerStatus()
        {
            DisplayHelper.WriteLine($"{_playerName} received DisplayStatus");

            Console.WriteLine($"{_playerName} has {_health} health");
        }

        private void SimulateError()
        {
            DisplayHelper.WriteLine($"{_playerName} received SimulateError");

            throw new ApplicationException($"Simulated exception in command: {_playerName}");
        }
    }
}
