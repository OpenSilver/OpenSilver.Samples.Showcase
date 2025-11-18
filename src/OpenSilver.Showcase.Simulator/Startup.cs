using OpenSilver.Simulator;
using System;

namespace OpenSilver.Showcase.Simulator
{
    static class Startup
    {
        [STAThread]
        static int Main(string[] args)
        {
            SimulatorLaunchParameters launchParameters = new SimulatorLaunchParameters();
            launchParameters.InitParams = "InitParamsForSimulatorKey=SimValue,OtherInitParamsForSimulatorKey=OtherSimValue";
            return SimulatorLauncher.Start(typeof(OpenSilver.Showcase.App), launchParameters);
        }
    }
}
