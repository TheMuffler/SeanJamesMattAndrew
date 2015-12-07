using UnityEngine;
using System.Collections;

public class TransClass : StateMachineBehaviour {

	public int transition; //The number of the scene to transition to

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{


		Application.LoadLevel (transition);
	}

}
