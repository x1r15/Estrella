using UnityEngine;

[CreateAssetMenu(fileName = "MeteorSetting", menuName = "ScriptableObjects/CreateMeteorSetting", order = 1)]
public class MeteorSettings : ScriptableObject
{
    public Sprite[] Sprites;
    public Sprite[] SpritesFG;
    public Sprite[] SpritesBG;
    public int Damage;
    public float SpeedModificator;
    
    public SpriteSet GetRandomSpritesSet()
    {
        var randomIndex = Random.Range(0, Sprites.Length);
        return new SpriteSet
        {
            Sprite = Sprites[randomIndex],
            Fg = SpritesFG[randomIndex],
            Bg = SpritesBG[randomIndex]
        };
    }
}

public class SpriteSet
{
    public Sprite Sprite;
    public Sprite Fg;
    public Sprite Bg;
}


