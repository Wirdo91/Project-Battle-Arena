using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// https://youtu.be/rMCLWt1DuqI?t=20m
/// </summary>

public class PlayerSkinController : MonoBehaviour
{
    private string _defaultSpriteSheetName = "DefaultCharacter";

    [SerializeField]
    private string _characterSpriteSheetName = "DefaultCharacter";

    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //TODO IF using ARPG sheet. One animation per sheet length (frame for animation) is enough. Afterwards in LateUpdate identify which direction, and adjust the sheet name as shown down
        Sprite[] subSprites = Resources.LoadAll<Sprite>("Sprites/" + _characterSpriteSheetName);

        string spriteNameAppend = _renderer.sprite.name;

        spriteNameAppend = spriteNameAppend.Replace(_defaultSpriteSheetName, "");

        var newSprite = Array.Find(subSprites, item => item.name == _characterSpriteSheetName + spriteNameAppend);

        if (newSprite)
        {
            _renderer.sprite = newSprite;
        }
    }
}
