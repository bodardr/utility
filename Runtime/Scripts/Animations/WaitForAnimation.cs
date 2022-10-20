using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public class WaitForAnimation : CustomYieldInstruction
    {
        private readonly Animator animator;

        private readonly int layerIndex;
        private readonly int stateHash;
        private readonly bool stateMatches;

        private bool animationPlaying;

        private float currentTimeout = 0;
        private float maxTimeout = 0;
        private int nextStateHash;

        public WaitForAnimation(Animator animator, SerializableAnimatorStateInfo serializedStateInfo,
            float maxTimeout = 0) : this(animator, serializedStateInfo.StateNameHash, serializedStateInfo.LayerIndex,
            maxTimeout)
        {
        }

        public WaitForAnimation(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, float maxTimeout = 0) :
            this(animator,
                stateInfo.shortNameHash, layerIndex, maxTimeout)
        {
        }

        public WaitForAnimation(Animator animator, string stateName, int layerIndex, float maxTimeout = 0) : this(
            animator,
            Animator.StringToHash(stateName), layerIndex, maxTimeout)
        {
        }

        public WaitForAnimation(Animator animator, int stateNameHash, int layerIndex, float maxTimeout = 0)
        {
            this.layerIndex = layerIndex;
            this.animator = animator;
            this.maxTimeout = maxTimeout;

            stateHash = stateNameHash;

            UpdateAnimationPlayingStatus();
        }

        public override bool keepWaiting
        {
            get
            {
                if (!animationPlaying)
                    return UpdateAnimationPlayingStatus();

                //Once the animation plays, we wait for the next state to change, indicating a transition.
                return animator && animator.GetNextAnimatorStateInfo(layerIndex).shortNameHash == nextStateHash;
            }
        }

        private bool UpdateAnimationPlayingStatus()
        {
            if (!animator)
                return false;

            if (animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash == stateHash)
            {
                animationPlaying = true;
                nextStateHash = animator.GetNextAnimatorStateInfo(layerIndex).shortNameHash;
                return true;
            }

            if (maxTimeout <= 0)
                return true;

            currentTimeout += Time.deltaTime;
            return maxTimeout < currentTimeout;
        }
    }
}