using HarmonyLib;

namespace GorillaInvisibility.Patches
{
    [HarmonyPatch(typeof(VRRig), "LateUpdate")]
    public class PlayerUpdatePatch
    {
        public static void Postfix(VRRig __instance)
        {
            if (__instance.isOfflineVRRig)
            {
                bool canChangeHiddenState = !NetworkSystem.Instance.InRoom || Plugin.IsAllowed;

                bool shouldPlayerHide = NetworkSystem.Instance.InRoom;

                if (!canChangeHiddenState || __instance.IsPlayerMeshHidden == shouldPlayerHide) return;

                __instance.SetPlayerMeshHidden(shouldPlayerHide);
            }
        }
    }
}
