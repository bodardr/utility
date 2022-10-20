using UnityEngine;

public class RandomAnimationOffset : StateMachineBehaviour
{
    [Header("Cycle Offset Parameter - Assign it to the Cycle Offset in this animation")]
    [SerializeField]
    private string offsetParameterName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(offsetParameterName, Random.value);
    }
}