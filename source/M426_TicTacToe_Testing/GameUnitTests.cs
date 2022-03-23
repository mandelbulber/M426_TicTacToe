using M426_TicTacToe.Enums;
using M426_TicTacToe.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace M426_TicTacToe_Testing
{
    [TestClass]
    public class GameUnitTests
    {
        #region Game.IsPlayer1()

        [TestMethod]
        public void IsPlayer1TrueTest()
        {
            // Arrange
            FieldState[] fieldStates = new FieldState[9]
            {
                FieldState.none,
                FieldState.player1,
                FieldState.none,
                FieldState.player2,
                FieldState.none,
                FieldState.none,
                FieldState.none,
                FieldState.none,
                FieldState.none
            };
            Game game = new()
            {
                Board = JsonConvert.SerializeObject(fieldStates)
            };

            // Act
            bool isPlayer1 = game.IsPlayer1();

            // Assert
            Assert.IsTrue(isPlayer1);
        }

        [TestMethod]
        public void IsPlayer1FalseTest()
        {
            // Arrange
            FieldState[] fieldStates = new FieldState[9]
            {
                FieldState.player2,
                FieldState.none,
                FieldState.none,
                FieldState.none,
                FieldState.none,
                FieldState.none,
                FieldState.player1,
                FieldState.none,
                FieldState.player1
            };
            Game game = new()
            {
                Board = JsonConvert.SerializeObject(fieldStates)
            };

            // Act
            bool isPlayer1 = game.IsPlayer1();

            // Assert
            Assert.IsFalse(isPlayer1);
        }

        #endregion
    }
}
