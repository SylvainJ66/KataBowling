using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KataBowlingService.Tests
{
    public class Game_Tests
    {
        private Game _game;

        public Game_Tests()
        {
            _game = new Game();
        }

        [Fact]
        public void Roll_AndKnockDown0Pins_ShouldReturn0()
        {
            Assert.Equal(0, _game.Roll(0));
        }

        [Fact]
        public void Roll_AndKnockDown1Pins_ShouldReturn1()
        {
            Assert.Equal(1, _game.Roll(1));
        }

        [Fact]
        public void Roll_AndKnockDown11Pins_ShouldReturnException()
        {
            Assert.Throws<Exception>(() => _game.Roll(11));
        }

        [Fact]
        public void Roll_AndKnockDown12Pins_ShouldReturnException()
        {
            Assert.Throws<Exception>(() => _game.Roll(12));
        }

        [Fact]
        public void Roll_20Times0ShouldReturn0()
        {
            int score = 0;
            Enumerable.Range(0, 20).ToList().ForEach(_ => score += _game.Roll(0));

            Assert.Equal(0, score);

        }

        [Fact]
        public void Roll_20Times1ShouldReturn20()
        {
            int score = 0;
            Enumerable.Range(0, 20).ToList().ForEach(_ => score += _game.Roll(1));

            Assert.Equal(20, score);

        }

        [Fact]
        public void Roll_ASpareOnTheFirstFrame()
        {
            _game.Roll(5);
            _game.Roll(5);
            _game.Roll(3);
            _game.Roll(0);
            int finalScore = this.FinishTheGame(16, 0);
            Assert.Equal(16, finalScore);
        }

        [Fact]
        public void Roll_ASpareOnTheSecondFrame()
        {
            _game.Roll(1);
            _game.Roll(1);
            _game.Roll(5);
            _game.Roll(5);
            _game.Roll(3);
            int finalScore = this.FinishTheGame(15, 0);
            Assert.Equal(18, finalScore);
        }

        [Fact]
        public void Roll_AStikeOnTheFirstFrame()
        {
            _game.Roll(10);
            _game.Roll(5);
            _game.Roll(5);
            int finalScore = this.FinishTheGame(17, 0);
            Assert.Equal(30, finalScore);
        }

        [Fact]
        public void Roll_SpareGiveAnExtraBallOnTheThenthFrame()
        {
            _game.Roll(5);
            _game.Roll(5);
            _game.Roll(3);
            Enumerable.Range(0, 17).ToList().ForEach(_ => _game.Roll(0));
            _game.Roll(3);
            int finalScore = _game.Score();
            Assert.Equal(19, finalScore);
        }

        [Fact]
        public void Roll_StrikeGiveAnExtraBallOnTheThenthFrame()
        {
            _game.Roll(10);
            _game.Roll(5);
            _game.Roll(3);
            Enumerable.Range(0, 17).ToList().ForEach(_ => _game.Roll(0));
            _game.Roll(2);
            int finalScore = _game.Score();
            Assert.Equal(30, finalScore);
        }


        private int FinishTheGame(int rollsNumber, int pinsKnockedDown)
        {
            Enumerable.Range(0, rollsNumber).ToList().ForEach(_ => _game.Roll(pinsKnockedDown));
            return _game.Score();
        }
    }
}
