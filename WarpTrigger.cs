using UnityEngine;

public class WarpTrigger : MonoBehaviour
{
    public PlayerMove player;
    public StageBounds targetStage;
    public Vector2 spawnPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // ★ ステージ切替（Active制御込み）
        StageManager.ForceSetStage(targetStage);

        // ★ プレイヤー移動
        player.WarpTo(spawnPosition);
    }
}
