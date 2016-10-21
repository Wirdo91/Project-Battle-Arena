using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldBuilder : MonoBehaviour
{

    public static WorldBuilder instance { get { return _instance; } }
    private static WorldBuilder _instance;

    Dictionary<Vector3, Block> _grid = new Dictionary<Vector3, Block>();
    public Dictionary<Vector3, Block> Grid { get { return _grid;} }

    public Vector2 Size = Vector2.zero;

    public GameObject BlockPrefab;

    //TODO Make ground default/changeable

        //TODO Make safe spawn. Maybe

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        ReadWorld();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ReadWorld()
    {
        _grid.Clear();
        foreach (Block block in GetComponentsInChildren<Block>())
        {
            _grid.Add(block.transform.localPosition, block);
        }
    }

    #region World Builder
    public void GenerateWorld(PreDefinedWorld world)
    {
        ClearWorld();
        BuildBaseWorld();
        if (world == PreDefinedWorld.RANDOM)
        {
            foreach (Block block in WorldBuilder.instance.Grid.Values)
            {
                block.UpdateBlock((BlockType)Random.Range(0, 7));
            }
        }
        else
        {
            foreach (Vector3 vector3 in PredefinedWorld.GetPreWorld(world).Keys)
            {
                if (PredefinedWorld.GetPreWorld(world).ContainsKey(vector3))
                    Grid[vector3].UpdateBlock(PredefinedWorld.GetPreWorld(world)[vector3]);
            }
        }
    }

    public void BuildBaseWorld()
	{
		if (BlockPrefab == null)
			return;
		for (int x = 0; x <= Size.x; x++)
		{
			for (int modx = -1; modx <= 1; modx++)
			{
				if (modx == 0)
					continue;
				for (int z = 0; z <= Size.y; z++)
				{
					for (int modz = -1; modz <= 1; modz++)
					{
						if (modz == 0)
							continue;
						if (!(x + z > -2))
						{
							CreateBlock(Vector3.zero);
						}
						else
						{
                            CreateBlock(new Vector3(x * modx, 0, z * modz));
						}
						if (z == 0)
							break;
					}
				}
				if (x == 0)
					break;
			}
		}
	}

	void CreateBlock(Vector3 position)
	{
		Block newBlock = Instantiate(BlockPrefab).GetComponent<Block>();
		newBlock.transform.parent = transform;
		newBlock.transform.localPosition = position;

		newBlock.Init(position);
		_grid.Add(position, newBlock);
	}

    public void ClearWorld()
	{
		foreach(Block block in _grid.Values)
		{
			block.Destroy();
		}
		_grid.Clear();
    }

    #endregion
}
