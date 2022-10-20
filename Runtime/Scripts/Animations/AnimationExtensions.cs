using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public static class AnimationExtensions
    {
        public static AnimationClip FindAnimation(this Animator animator, Func<string, bool> predicate)
        {
            var anims = animator.runtimeAnimatorController.animationClips;
            return anims.FirstOrDefault(a => predicate(a.name));
        }

        public static IEnumerator WaitForEndOfTransition(AnimatorTransitionInfo transitionInfo)
        {
            yield return new WaitForSeconds(transitionInfo.duration);
        }
    }
}