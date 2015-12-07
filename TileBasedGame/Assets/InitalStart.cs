using UnityEngine;
using System.Collections;

public class InitalStart : StateMachineBehaviour {
	
	public bool passed;
	public Canvas menuCanvas;


	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.passed=true;
		//menuCanvas.SetActive(true);
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.passed=true;
		//menuCanvas.SetActive(true);
	}
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		this.passed=true;
		//menuCanvas.SetActive(true);
	}
}
