using Battleship.Core.Enums;
using Battleship.Core.Models;
using Battleship.Logic;
using Battleship.Logic.Models;
using ConsoleApp.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class BattleshipGamePlay : IObserver<GameStatus>, IDisposable
    {
        private IDisposable _unsubscriber;
        private readonly IGameController _controller;
        private readonly AutoPlay _autoPlay;
        private readonly ConsoleWriter _writer;

        public Dictionary<Guid, (string name, Grid targetGrid)> Status { get; set; } = new Dictionary<Guid, (string, Grid)>();


        public BattleshipGamePlay(IGameController controller, AutoPlay autoPlay, ConsoleWriter writer)
        {
            _controller = controller;
            _autoPlay = autoPlay;
            _writer = writer;
        }

        public async Task PlayAsync()
        {
            var newGame = _autoPlay.CreateNewGame("Rob", "Bob");

            foreach (var player in newGame.Players)
            {
                Status.Add(player.Id, (player.Name, new Grid()));
            }

            _unsubscriber = _controller.SubscribeForStatusUpdates(this);
            var gameEnd = await _autoPlay.StartGameAsync(newGame.GameId);

            _writer.Write(new string('*', 30));
            _writer.Write(gameEnd.Text);
            _writer.Write(new string('*', 30));
        }

        public void OnNext(GameStatus value)
        {
            Status[value.ShotResponse.Shooter].targetGrid.Squares[value.ShotResponse.Cooridnates.X, value.ShotResponse.Cooridnates.Y] = value.ShotResponse.Status switch
            {
                ShotResult.Miss => false,
                _ => true
            };
            _writer.Write($"{Status[value.ShotResponse.Shooter].name}: {value.ShotResponse.Cooridnates}, {value.ShotResponse.Status}");
            if (value.ShotResponse.Message != string.Empty)
            {
                _writer.Write(value.ShotResponse.Message);
            }
            _writer.WriteGridToConsole(Status[value.ShotResponse.Shooter].targetGrid);
        }


        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public void Dispose()
        {
            _unsubscriber.Dispose();
        }
    }
}
