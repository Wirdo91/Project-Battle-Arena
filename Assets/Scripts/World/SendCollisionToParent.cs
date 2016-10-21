using UnityEngine;
using System.Collections;

public class SendCollisionToParent : MonoBehaviour {

	public delegate void OnCollisionEnterCustom(Collision col);
	public OnCollisionEnterCustom onCollisionEnterCustom;

	void OnCollisionEnter(Collision col)
	{
		if (onCollisionEnterCustom != null)
			onCollisionEnterCustom(col);
	}

	public delegate void OnCollisionExitCustom(Collision col);
	public OnCollisionExitCustom onCollisionExitCustom;

	void OnCollisionExit(Collision col)
	{
		if (onCollisionExitCustom != null)
			onCollisionExitCustom(col);
	}

	public delegate void OnCollisionStayCustom(Collision col);
	public OnCollisionStayCustom onCollisionStayCustom;

	void OnCollisionStay(Collision col)
	{
		if (onCollisionStayCustom != null)
			onCollisionStayCustom(col);
	}
}
