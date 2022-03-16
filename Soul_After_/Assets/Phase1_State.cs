using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1_State : StateMachineBehaviour
{
    public Boss_Phase1 phase1;
    public Boss_Phase2 phase2;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        phase1 = animator.GetComponent<Boss_Phase1>();
        phase2 = animator.GetComponent<Boss_Phase2>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        phase1.enabled = false;
        phase2.enabled = true;
    }
}
