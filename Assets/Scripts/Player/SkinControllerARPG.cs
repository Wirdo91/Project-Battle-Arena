using System;
using UnityEngine;
using System.Collections;

public enum AnimationRow
{
    StabUp = 4,
    StabLeft,
    StabDown,
    StabRight,
    MoveUp = 8,
    MoveLeft,
    MoveDown,
    MoveRight,
    SwingUp = 12,
    SwingLeft,
    SwingDown,
    SwingRight,
    BowUp = 16,
    BowLeft,
    BowDown,
    BowRight,
    Die = 20
}

public class SkinControllerARPG : MonoBehaviour
{
    [SerializeField]
    private Texture2D _characterTexture;

    private string _defaultSpriteSheetName = "AnimationSprite";

    private SpriteRenderer _renderer;

    public AnimationRow _currentAnimationRow;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Sprite[] subSprites = Resources.LoadAll<Sprite>("Sprites/ARPG/body/male/" + _characterTexture.name);

        string spriteNameAppend = _renderer.sprite.name;

        spriteNameAppend = spriteNameAppend.Replace(_defaultSpriteSheetName, "");

        var newSprite = Array.Find(subSprites, item => item.name == _characterTexture.name + (int)_currentAnimationRow + "," + spriteNameAppend);

        if (newSprite)
        {
            _renderer.sprite = newSprite;
        }
    }
}
