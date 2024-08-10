using Microsoft.AspNetCore.Mvc;
using MontiHallSimulation.API.Controllers;
using MontiHallSimulation.BusinessLogic.Interfaces;
using MontiHallSimulation.BusinessLogic.Services;
using MontiHallSimulation.Core.Entities;
using Moq;

namespace MontiHallSimulation.Test
{
    public class MontyHallSimulationServiceTests
    {
        private readonly Mock<ISimulationService> simulationService = new();
      
        [Fact]
        public async void RunSimulations_ShouldReturnCorrectResult_WhenChangeDoorIsTrue()
        {
            var numberOfGames = 1000;
            var changeDoor = true;

            simulationService.Setup(s => s.GetSimulationResult(changeDoor, numberOfGames)).ReturnsAsync((bool changeDoor, int numberOfGames) =>
            {
                int wins = changeDoor ? numberOfGames * 2 / 3 : numberOfGames / 3;
                int losses = numberOfGames - wins;
                return new SimulationResult
                {
                    TotalGames = numberOfGames,
                    Wins = wins,
                    Losses = losses,
                    ChangedDoor = changeDoor
                };
            });

            var simulationController = new SimulationController(simulationService.Object);

            var result = await simulationController.Simulate(numberOfGames, changeDoor);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<SimulationResult>(okResult.Value);

            Assert.Equal(numberOfGames, returnValue.TotalGames);
            Assert.True(returnValue.Wins > returnValue.Losses);
            Assert.True(returnValue.ChangedDoor);
            Assert.True(returnValue.ChangedDoor);
        }

        

        [Fact]
        public async void RunSimulations_ShouldReturnCorrectResult_WhenChangeDoorIsFalse()
        {
            var numberOfGames = 1000;
            var changeDoor = false;

            simulationService.Setup(s => s.GetSimulationResult(changeDoor, numberOfGames)).ReturnsAsync((bool changeDoor, int numberOfGames) =>
            {
                int wins = changeDoor ? numberOfGames * 2 / 3 : numberOfGames / 3;
                int losses = numberOfGames - wins;
                return new SimulationResult
                {
                    TotalGames = numberOfGames,
                    Wins = wins,
                    Losses = losses,
                    ChangedDoor = changeDoor
                };
            });

            var simulationController = new SimulationController(simulationService.Object);

            var result = await simulationController.Simulate(numberOfGames, changeDoor);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<SimulationResult>(okResult.Value);

            Assert.Equal(numberOfGames, returnValue.TotalGames);
            Assert.True(returnValue.Losses > returnValue.Wins); 
            Assert.False(returnValue.ChangedDoor);
        }

        [Fact]
        public async void RunSimulations_ShouldReturnZeroGames_WhenNumberOfGamesIsZero()
        {
            var numberOfGames = 0;
            var changeDoor = false;

            simulationService.Setup(s => s.GetSimulationResult(changeDoor, numberOfGames)).ThrowsAsync(new ApplicationException("Number of games should be greater than 0"));

            var simulationController = new SimulationController(simulationService.Object);
            var exception = Assert.ThrowsAsync<ApplicationException>(() => simulationController.Simulate(numberOfGames, changeDoor));

            Assert.Equal("Number of games should be greater than 0", exception.Result.Message);

        }

    }
}