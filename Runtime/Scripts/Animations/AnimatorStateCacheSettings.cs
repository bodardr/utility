using Bodardr.Utility.Runtime;
using UnityEngine;

[ScriptableObjectSingleton("Assets/Settings/Resources")]
public class AnimatorStateCacheSettings : ScriptableObjectSingleton<AnimatorStateCacheSettings>
{
    [SerializeField]
    private bool addStateCacheToAnimatorsAutomatically;

    public bool AddStateCacheToAnimatorsAutomatically => addStateCacheToAnimatorsAutomatically;
}