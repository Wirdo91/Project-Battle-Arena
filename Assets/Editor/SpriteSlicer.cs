using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SpriteSlicer : MonoBehaviour {

    [MenuItem("Sprites/Slice Texture")]
    static void SliceTexture()
    {
        if (Selection.objects[0].GetType() != typeof (Texture2D))
        {
            Debug.LogWarning("Not correct type of object");
            return;
        }
        Texture2D myTexture = (Texture2D)Selection.objects[0];

        string path = AssetDatabase.GetAssetPath(myTexture);
        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
        ti.isReadable = true;
        ti.spriteImportMode = SpriteImportMode.Multiple;

        List<SpriteMetaData> newData = new List<SpriteMetaData>();

        int Columns = 13;
        int Rows = 21;

        int SliceWidth = myTexture.width / Columns;
        int SliceHeight = myTexture.height / Rows;

        for (int i = 0; i < myTexture.width; i += SliceWidth)
        {
            for (int j = myTexture.height; j > 0; j -= SliceHeight)
            {
                SpriteMetaData smd = new SpriteMetaData();
                smd.pivot = new Vector2(0.5f, 0.5f);
                smd.alignment = 9;
                smd.name = myTexture.name + (myTexture.height - j) / SliceHeight + "," + i / SliceWidth;
                smd.rect = new Rect(i, j - SliceHeight, SliceWidth, SliceHeight);

                newData.Add(smd);
            }
        }

        Debug.Log("Sliced "+path+" into " + ti.spritesheet.Length + " pieces");

        ti.spritesheet = newData.ToArray();
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);


    }
}
