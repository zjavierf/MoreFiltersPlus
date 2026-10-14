using IPA;
using HarmonyLib;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace MoreFiltersPlus {
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin {
        internal static Plugin? Instance { get; private set; }
        internal static IPALogger? Log { get; private set; }
        internal static Harmony? HarmonyInstance { get; private set; }

        [Init]
        public void Init(IPALogger logger) {
            Instance = this;
            Log = logger;
            Log.Info("[MoreFilters+] Filters and Sorters Initialized.");
        }

        [OnStart]
        public void OnApplicationStart() {
            
            HarmonyInstance = new Harmony("zjavierf.BeatSaber.MoreFiltersPlus");
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            Log?.Info("[MoreFilters+] Harmony patches applied successfully.");

            MoreFiltersInitializer.RegisterFilters();
            MoreSortersInitializer.RegisterSorters();
        }

        [OnExit]
        public void OnApplicationQuit() {
            HarmonyInstance?.UnpatchSelf();
        }
    }
}