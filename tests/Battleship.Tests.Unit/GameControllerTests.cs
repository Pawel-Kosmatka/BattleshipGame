using AutoFixture;
using Battleship.Core.Models;
using Battleship.Logic;
using Battleship.Logic.Models;
using Battleship.Logic.StatusNotification;
using Battleship.Persistence;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class GameControllerTests
    {
        private readonly GameController _sut;
        private readonly IFixture _fixture = new Fixture();
        private readonly GameContext _context = Substitute.For<GameContext>();
        private readonly StatusNotificator _notificator = Substitute.For<StatusNotificator>();

        public GameControllerTests()
        {
            _sut = new GameController(_context, _notificator);
        }

        [Fact]
        public void PrepareNewGame_ShouldReturnStatusWithTwoIds()
        {
            var grids = _fixture.CreateMany<PlayerGrid>(2);

            var result = _sut.PrepareNewGame(grids);

            result.Should().BeOfType(typeof(GameStatus));
            result.GameId.Should().NotBeEmpty();
            result.Next.Should().NotBeEmpty();
            result.ShotResponse.Should().BeNull();
        }

        [Fact]
        public void PrepareNewGame_ShouldRegisterNewGame()
        {
            var grids = _fixture.CreateMany<PlayerGrid>(2);

            _sut.PrepareNewGame(grids);

            _context.ReceivedWithAnyArgs(1).RegisterNewGame(Arg.Any<Game>());
            _context.ReceivedWithAnyArgs(1).SetNextPlayer(Arg.Any<Guid>(), Arg.Any<Guid>());
            _context.Games.First().Grids.Should().BeEquivalentTo(grids);
        }

        [Fact]
        public void PrepareNewGame_ShouldNotifyObservers()
        {
            var grids = _fixture.CreateMany<PlayerGrid>(2);

            _sut.PrepareNewGame(grids);

            _notificator.ReceivedWithAnyArgs(1).NotifyObservers(Arg.Any<GameStatus>());
        }
    }
}
