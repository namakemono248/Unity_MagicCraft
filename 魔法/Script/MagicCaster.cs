using UnityEngine;

/// <summary>
/// 魔法挙動
/// </summary>
public class MagicCaster : MonoBehaviour
{
    private PlayerMove playerMove;

    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    public void Cast(RuneGrammar grammar)
    {
        if (GameStateTest.isCrafting) return;
        if (grammar == null) return;

        if (grammar.projectilePrefab == null)
        {
            Debug.LogWarning("RuneGrammar に projectilePrefab が設定されていません！");
            return;
        }

        MagicData data = new MagicData();
        foreach (var rune in grammar.runes)
            rune.Apply(data);

        GameObject proj = Instantiate(
            grammar.projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        Vector2 dir = playerMove.lastDirection;
        if (dir != Vector2.zero)
            proj.transform.right = dir;

        var mp = proj.GetComponent<MagicProjectile>();
        if (mp != null)
            mp.SetData(data);
    }
}
