using Microsoft.AspNetCore.Mvc;
using MontiHallSimulation.BusinessLogic.Interfaces;
using MontiHallSimulation.Core.Entities;

namespace MontiHallSimulation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController:ControllerBase
    {
        private readonly ISimulationService _simulationService;

        public SimulationController(ISimulationService simulationService)
        {
            _simulationService = simulationService;
        }

        [HttpGet("simulate")]
        public async Task<ActionResult<SimulationResult>> Simulate(int numberOfGames, bool changeDoor)
        {
            var result = await _simulationService.GetSimulationResult(changeDoor,numberOfGames);
            return Ok(result);
        }
    }
}
