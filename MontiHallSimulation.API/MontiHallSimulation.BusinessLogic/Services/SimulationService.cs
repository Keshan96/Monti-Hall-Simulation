using MontiHallSimulation.BusinessLogic.Interfaces;
using MontiHallSimulation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontiHallSimulation.BusinessLogic.Services
{
    public class SimulationService : ISimulationService
    {
        public async Task<SimulationResult> GetSimulationResult(bool IsSwitch, int numberOfGames)
        {

            if (numberOfGames == 0) {
                throw new ApplicationException("Number of games should be grater than 0");
            }
            int wins = 0;
            int losses = 0;

            Random random = new Random();

            for (int i = 0; i < numberOfGames; i++)
            {
                int carBehindDoor = random.Next(0, 3);
                int playerChoice = random.Next(0, 3);

                int montyOpens;
                do
                {
                    montyOpens = random.Next(0, 3);
                } while (montyOpens == carBehindDoor || montyOpens == playerChoice);

                if (IsSwitch)
                {
                    playerChoice = 3 - playerChoice - montyOpens; 
                }

                if (playerChoice == carBehindDoor)
                {
                    wins++;
                }
                else
                {
                    losses++;
                }
            }

            return new SimulationResult
            {
                TotalGames = numberOfGames,
                Wins = wins,
                Losses = losses,
                ChangedDoor = IsSwitch
            };
        }
    }
}
