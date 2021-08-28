using AutoFixture;
using Battleship.Core.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class GameTests
    {
        private readonly Game _sut;
        private readonly IFixture _fixture = new Fixture();

        public GameTests()
        {
            _sut = new Game();
        }

        [Fact]
        public void SttingNext_ShouldUpdateValuesOfRoundAndIsFirstShotInThisRound()
        {
            var firstNext = _fixture.Create<Guid>();
            var secondNext = _fixture.Create<Guid>();

            _sut.Next = firstNext;
            var isFirstShotInThisRound_firstNext = _sut.IsFirstShotInThisRound;
            var round_firstNext = _sut.Round;

            _sut.Next = secondNext;
            var isFirstShotInThisRound_secondNext = _sut.IsFirstShotInThisRound;
            var round_secondNext = _sut.Round;


            _sut.Next = firstNext;
            var isFirstShotInThisRound_thirdNext = _sut.IsFirstShotInThisRound;
            var round_thirdNext = _sut.Round;

            isFirstShotInThisRound_firstNext.Should().BeTrue();
            round_firstNext.Should().Be(1);
            isFirstShotInThisRound_secondNext.Should().BeFalse();
            round_secondNext.Should().Be(1);
            isFirstShotInThisRound_thirdNext.Should().BeTrue();
            round_thirdNext.Should().Be(2);
        }
    }
}
