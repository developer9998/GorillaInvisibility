using HarmonyLib;

namespace GorillaInvisibility.Patches
{
    [HarmonyPatch(typeof(VRRig), "LateUpdate")]
    public class PlayerUpdatePatch
    {
        private static bool _isHidden;

        public static void Prefix(VRRig __instance)
        {
            if (__instance.isOfflineVRRig)
            {
                _isHidden = __instance.IsPlayerMeshHidden;
            }
        }

        public static void Postfix(VRRig __instance)
        {
            if (__instance.isOfflineVRRig)
            {
                bool canChangeHiddenState = !NetworkSystem.Instance.InRoom || Plugin.IsAllowed || (!Plugin.IsAllowed && _isHidden);

                bool shouldPlayerHide = NetworkSystem.Instance.InRoom && Plugin.IsAllowed;

                if (!canChangeHiddenState || __instance.IsPlayerMeshHidden == shouldPlayerHide) return;

                __instance.SetPlayerMeshHidden(shouldPlayerHide);
            }
        }
    }
}
