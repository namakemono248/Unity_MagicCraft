using UnityEngine;

public abstract class Rune : ScriptableObject
{
    public string runeName;
    public Sprite icon;

    [TextArea]
    public string description;

    // ルーンの効果を魔法に刻む
    public abstract void Apply(MagicData data);
}
