using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum GamePhase
{
    INGAMEPHASE,
    BUILDPHASE
}

public class WorldController : MonoBehaviour
{
	Vector3? _selectedBlockGridPosition = null;
	[SerializeField]
	LayerMask _clickableLayers;

    private GamePhase _currentPhase = GamePhase.INGAMEPHASE;

    [SerializeField]
    private RectTransform _menuPanel;

	// Use this for initialization
	void Start()
	{
    }

    void Update()
    {

        #region TestInput
        if (Input.GetKeyUp(KeyCode.R)) //Resets the world
        {
            foreach (Block block in WorldBuilder.instance.Grid.Values)
            {
                block.ResetBlock();
            }
        }
        if (Input.GetKeyUp(KeyCode.T)) //Prints number of blocks
        {
            Debug.Log(WorldBuilder.instance.Grid.Values.Count);
        }
        if (Input.GetKeyUp(KeyCode.P)) //Changes phase to the next in the enum
        {
            ChangePhase((GamePhase) (((int) _currentPhase + 1)%Enum.GetNames(typeof (GamePhase)).Length));
        }
        #endregion

        if (Input.GetMouseButtonUp(0)) //Used for selecting block
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_menuPanel, Input.mousePosition))
                return;
            Debug.Log("Mouse Clicked");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 500.0f, _clickableLayers)) //If clicked object os of _clickableLayers
            {
                Debug.Log(hit.transform.name + " was hit");
                if (hit.transform.GetComponent<Block>() != null) //If contains block (safety check)
                {
                    if (_selectedBlockGridPosition != null) //deselect
                        WorldBuilder.instance.Grid[(Vector3) _selectedBlockGridPosition].IsBlockSelected = false;
                    _selectedBlockGridPosition = hit.transform.localPosition;
                    Debug.Log("Index: " + _selectedBlockGridPosition + " out of " + WorldBuilder.instance.Grid.Count +
                              " clicked");
                    WorldBuilder.instance.Grid[(Vector3) _selectedBlockGridPosition].IsBlockSelected = true;
                }
                else //Not a block (should't happen)
                {
                    if (_selectedBlockGridPosition != null) //deselect
                        WorldBuilder.instance.Grid[(Vector3) _selectedBlockGridPosition].IsBlockSelected = false;
                    Debug.LogWarning("Object is not a block - Check _clickableLayers");
                    _selectedBlockGridPosition = null;
                }
            }
            else //deselect current
            {
                if (_selectedBlockGridPosition != null) //deselect
                    WorldBuilder.instance.Grid[(Vector3) _selectedBlockGridPosition].IsBlockSelected = false;
                _selectedBlockGridPosition = null;
                Debug.Log("Nothing hit");
            }
        }
    }

    private void ChangePhase(GamePhase newPhase)
    {
        _currentPhase = newPhase;

        //Show/Hide selectors
        foreach (Block block in WorldBuilder.instance.Grid.Values)
        {
            block.ResetBlock();
            block.ShowSelector(_currentPhase == GamePhase.BUILDPHASE);
        }

        //Show/Hide menu
        _menuPanel.gameObject.SetActive(_currentPhase == GamePhase.BUILDPHASE);

        //Despawn Players
        //TODO See above

        //When go to game, rotate world
        //Have default rotation used when building
    }

    public void UpdateSelectedBlock(BlockType newType)
	{
	    if (_selectedBlockGridPosition == null)
	        return;
		if (newType == BlockType.GROUND && WorldBuilder.instance.Grid[(Vector3)_selectedBlockGridPosition].BlockType == BlockType.EMPTY)
		{
            WorldBuilder.instance.Grid[(Vector3)_selectedBlockGridPosition].UpdateBlock(newType);
			//TODO Spend new ground block
		}
		else if ((newType == BlockType.FROZEN || newType == BlockType.SPIKED || newType == BlockType.FALLING) && WorldBuilder.instance.Grid[(Vector3)_selectedBlockGridPosition].BlockType == BlockType.GROUND)
		{
            WorldBuilder.instance.Grid[(Vector3)_selectedBlockGridPosition].UpdateBlock(newType);
			//TODO Spend trap
			//TODO Remove second restriction, just use one block and one trap
		}
		else
		{
			Debug.Log("Block can not be placed here");
		}
	}
}
