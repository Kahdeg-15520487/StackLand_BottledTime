using BepInEx;
using BerryLoaderNS;

namespace SandClock
{
    [BepInPlugin("sandclock", "Sand Clock", "0.0.1")]
    [BepInDependency("BerryLoader")]
    class Plugin : BaseUnityPlugin
    {
        public static BepInEx.Logging.ManualLogSource L;

        private void Awake()
        {
            L = Logger;
            L.LogInfo($"hello from {nameof(Awake)}");
        }
    }
}