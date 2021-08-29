using AutoFixture;
using Battleship.Core.Models;
using Battleship.Core.Settings;
using Battleship.Logic;
using Battleship.Logic.Models;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class AutoPlayTests
    {
        private readonly AutoPlay _sut;
        private readonly IGameController _gameController = Substitute.For<IGameController>();
        private readonly IFixture _fixture = new Fixture();
        private readonly Random _random = new Random();

        public AutoPlayTests()
        {
            _sut = new AutoPlay(_gameController);
        }

        [Fact]
        public void CreateNewGame_ShouldAddNewGameToGamesDictionary()
        {
            var status = new GameStatus(_fixture.Create<Guid>(), _fixture.Create<Guid>());
            _gameController.PrepareNewGame(Arg.Any<PlayerGrid[]>()).Returns(status);

            _sut.CreateNewGame(_fixture.Create<string>(), _fixture.Create<string>());

            _sut.Games.Keys.Should().ContainSingle(s => s == status.GameId);
        }

        [Fact]
        public void CreateNewGame_ShouldReturnCreatedPlayers()
        {
            var name1 = _fixture.Create<string>();
            var name2 = _fixture.Create<string>();
            var status = _fixture.Create<GameStatus>();
            _gameController.PrepareNewGame(Arg.Any<PlayerGrid[]>()).Returns(status);

            var result = _sut.CreateNewGame(name1, name2);

            result.players.ElementAt(0).Name.Should().Be(name1);
            result.players.ElementAt(1).Name.Should().Be(name2);
        }
    }
}
