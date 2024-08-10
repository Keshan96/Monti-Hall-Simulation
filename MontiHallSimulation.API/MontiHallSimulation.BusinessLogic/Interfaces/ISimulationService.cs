using MontiHallSimulation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontiHallSimulation.BusinessLogic.Interfaces
{
    public interface ISimulationService
    {
        Task<SimulationResult> GetSimulationResult(bool IsSwitch,int numberOfTime);
    }
}
