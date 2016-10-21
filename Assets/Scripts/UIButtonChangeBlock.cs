using UnityEngine;
using System.Collections;

public class UIButtonChangeBlock : MonoBehaviour {

	[SerializeField]
	private BlockType _typeToPlace = BlockType.EMPTY;

	[SerializeField]
	WorldController _worldController;

	public void UpdateSelectedBlock()
	{
		_worldController.UpdateSelectedBlock(_typeToPlace);
	}
}
