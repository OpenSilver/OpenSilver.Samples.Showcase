using OpenSilver.Simulator;
using System;

namespace OpenSilver.Samples.Showcase.Simulator
{
    static class Startup
    {
        [STAThread]
        static int Main(string[] args)
        {
            return SimulatorLauncher.Start(typeof(OpenSilver.Samples.Showcase.App));
        }
    }
}
