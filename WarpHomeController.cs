using UnityEngine;
using UnityEngine.InputSystem;

public class WarpHomeController : MonoBehaviour
{
    [Header("Player")]
    public PlayerMove player;

    [Header("Home Stage")]
    public StageBounds homeStage;
    public Vector2 homeSpawn; // Homeでのスポーン座標

    void Update()
    {
        if (Keyboard.current == null) return;

        // Qキー押した瞬間
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            WarpToHome();
        }
    }

    void WarpToHome()
    {
        // 現在のステージをOFF
        if (StageManager.Current != null)
        {
            StageManager.Current.gameObject.SetActive(false);
        }

        // HomeステージをON
        homeStage.gameObject.SetActive(true);

        // StageManagerに宣言（Current更新）
        StageManager.ForceSetStage(homeStage);

        // プレイヤーをHome座標にワープ
        player.WarpTo(homeSpawn);

        Debug.Log($"[WarpHomeController] Player warped to Home: {homeStage.name}");
    }
}
