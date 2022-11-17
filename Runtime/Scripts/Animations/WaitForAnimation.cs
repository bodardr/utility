using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public class WaitForAnimation : CustomYieldInstruction
    {
        private readonly Animator animator;
        private readonly int layerIndex;
        private readonly int stateHash;
        private readonly bool stateMatches;
        private readonly float timeout;

        private readonly WaitStrategy waitStrategy;
        private float startTime;

        private bool animationPlaying;

        private float currentTimeout;
        private int nextStateHash;

        public override bool keepWaiting
        {
            get
            {
                if (!animationPlaying)
                    return UpdateAnimationPlayingStatus();

                if (waitStrategy == WaitStrategy.TillEndOfAnimationTime)
                    return animator && animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 1;

                if (waitStrategy == WaitStrategy.TillEndOfTransition)
                    return animator && animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash == stateHash;

                return animator && animator.GetNextAnimatorStateInfo(layerIndex).shortNameHash == nextStateHash;
            }
        }

        public WaitForAnimation(Animator animator, SerializableAnimatorStateInfo serializedStateInfo,
            WaitStrategy waitStrategy = WaitStrategy.TillEndOfTransition, float timeout = 0) : this(animator,
            serializedStateInfo.StateNameHash, serializedStateInfo.LayerIndex, waitStrategy, timeout)
        {
        }

        public WaitForAnimation(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            WaitStrategy waitStrategy = WaitStrategy.TillEndOfTransition, float timeout = 0) :
            this(animator, stateInfo.shortNameHash, layerIndex, waitStrategy, timeout)
        {
        }

        public WaitForAnimation(Animator animator, string stateName, int layerIndex,
            WaitStrategy waitStrategy = WaitStrategy.TillEndOfTransition,
            float timeout = 0) : this(animator,
            Animator.StringToHash(stateName), layerIndex, waitStrategy,
            timeout)
        {
        }

        public WaitForAnimation(Animator animator, int stateNameHash, int layerIndex,
            WaitStrategy waitStrategy = WaitStrategy.TillEndOfTransition,
            float timeout = 0)
        {
            this.layerIndex = layerIndex;
            this.animator = animator;
            this.timeout = timeout;
            this.waitStrategy = waitStrategy;

            stateHash = stateNameHash;

            UpdateAnimationPlayingStatus();
        }

        private bool UpdateAnimationPlayingStatus()
        {
            if (!animator)
                return false;

            if (animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash == stateHash)
            {
                animationPlaying = true;

                if (waitStrategy == WaitStrategy.TillStartOfTransition)
                    nextStateHash = animator.GetNextAnimatorStateInfo(layerIndex).shortNameHash;
                else if (waitStrategy == WaitStrategy.TillEndOfAnimationTime)
                    startTime = Time.time;
                return true;
            }

            if (timeout <= 0)
                return true;

            currentTimeout += Time.deltaTime;
            return timeout < currentTimeout;
        }
    }

    public enum WaitStrategy
    {
        TillEndOfAnimationTime,
        TillStartOfTransition,
        TillEndOfTransition
    }
}