using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[ExecuteInEditMode]
public class EditorTool : MonoBehaviour
{
    [SerializeField]
    WorldBuilder _worldBuilder;

    [SerializeField]
    PreDefinedWorld _worldToGenerate = PreDefinedWorld.DIAMOND4P;

    [Tooltip("Builds a base world")]
    public bool Build_World = false;
    [Tooltip("Generate the pre set world(Check WorldBuilder)")]
    public bool Generate_World = false;
    [Tooltip("Updates each block to reflect editor changed settings")]
    public bool Update_World = false;
    [Tooltip("Removes all blocks in the world")]
    public bool Destroy_World = false;
    [Tooltip("Prints the world layout for saving")]
    public bool Print_World = false;
	
	// Update is called once per frame
	void Update () {

        #region Editor Buttons
        if (Build_World)
        {
            _worldBuilder.ReadWorld();
            _worldBuilder.ClearWorld();
            _worldBuilder.BuildBaseWorld();

            Build_World = false;
        }
        if (Generate_World)
        {
            _worldBuilder.ReadWorld();
            _worldBuilder.ClearWorld();
            _worldBuilder.GenerateWorld(_worldToGenerate);

            Generate_World = false;
        }
        if (Update_World)
        {
            _worldBuilder.ReadWorld();
            foreach (Block block in _worldBuilder.Grid.Values)
            {
                block.ForceUpdateCurrentBlock();
            }

            Update_World = false;
        }
        if (Destroy_World)
        {
            _worldBuilder.ReadWorld();
            _worldBuilder.ClearWorld();

            Destroy_World = false;
        }
        if (Print_World)
        {
            _worldBuilder.ReadWorld();
            PrintWorld();

            Print_World = false;
        }
        #endregion
    }

    void PrintWorld()
    {
        if (_worldBuilder.Grid.Count == 0)
        {
            Debug.Log("Please press play, before trying to print world. Grid is empty");
        }
        else
        {
            StringBuilder worldDictionaryString = new StringBuilder();
            foreach (KeyValuePair<Vector3, Block> pair in _worldBuilder.Grid)
            {
                if (pair.Value.BlockType != BlockType.EMPTY)
                {
                    worldDictionaryString.AppendFormat(", {{ new Vector3({0}f, {1}f, {2}f), BlockType.{3} }}", pair.Key.x, pair.Key.y, pair.Key.z,
                        pair.Value.BlockType);
                }
            }

            worldDictionaryString.Remove(0, 2);

            Debug.Log(worldDictionaryString.ToString());
        }
    }
}
