using AutoFixture;
using Battleship.Core.Models;
using Battleship.Core.Settings;
using Battleship.Logic;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class AIPlayerTests
    {
        private readonly AIPlayer _sut;
        private readonly IFixture _fixture = new Fixture();
        private readonly Random _random = new Random();

        public AIPlayerTests()
        {
            _sut = new AIPlayer
            {
                Name = _fixture.Create<string>()
            };
        }

        [Fact]
        public void PositionFleet_ShouldReturnPLayerGridWithPlayerId()
        {
            var playerId = _sut.Id;

            var result = _sut.PositionFleet(_random);

            result.Should().BeOfType(typeof(PlayerGrid));
            result.PlayerId.Should().Equals(playerId);
        }

        [Fact]
        public void PositionFleet_ShouldCreateFleetMatchingFleetFromSettings()
        {
            var settingsShips = GameSettings.Ships;
            var numberOfShips = settingsShips.Values.Sum();

            var result = _sut.PositionFleet(_random);

            result.Ships.Should().BeOfType(typeof(List<ShipSquare>));
            result.Ships.Should().HaveCount(c => c == numberOfShips);
            foreach (var (name, count) in settingsShips)
            {
                result.Ships.Where(s => s.ShipName == name).Should().HaveCount(count);
            }
        }

        [Fact]
        public void PositionFleet_ShouldCreateFleetFittingInGrid()
        {
            var x = GameSettings.XLength;
            var y = GameSettings.YLength;

            var result = _sut.PositionFleet(_random);

            foreach (var square in result.Ships)
            {
                square.Coordinates.X.Should().BeLessThan(x);
                square.Coordinates.Y.Should().BeLessThan(y);
            }
        }

        [Fact]
        public void PositionFleet_ShouldCreateFleetWithNotOveerlappingShips()
        {
            var result = _sut.PositionFleet(_random);

            result.Ships.Should().OnlyHaveUniqueItems();
        }

        [Fact]
        public void PositionFleet_ShouldFillOcanGridWithCoordinatsOfShips()
        {
            var gridBeforeFilling = _sut.OceanGrid;

            var result = _sut.PositionFleet(_random);
            var ocean = result.Squares;
            
            foreach (var square in result.Ships)
            {
                ocean[square.Coordinates.X, square.Coordinates.Y].Should().BeTrue();
            }
        }
    }
}
