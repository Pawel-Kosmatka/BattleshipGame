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
        public void StartNewGame_ShouldAddNewGameToGames()
        {
            var status = new GameStatus(_fixture.Create<Guid>(), _fixture.Create<Guid>());
            _gameController.PrepareNewGame(Arg.Any<PlayerGrid[]>()).Returns(status);

            _sut.StartNewGame();

            _sut.Games.Keys.Should().ContainSingle(s => s == status.GameId);
         
        }
    }
}
