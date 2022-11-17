#if UNITY_EDITOR
using Bodardr.Utility.Runtime;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
#endif
public class AnimatorStateCacheReference : MonoBehaviour
{
    [ReadOnlyInspector]
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private bool customAnimatorReference;

    [ShowIf(nameof(customAnimatorReference))]
    [SerializeField]
    private AnimatorStateCache stateCache;

    public Animator Animator
    {
        get => animator;
        set => animator = value;
    }

    public AnimatorStateCache StateCache => stateCache;

#if UNITY_EDITOR
    private void Awake()
    {
        if (!customAnimatorReference)
            Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!Animator || !Animator.runtimeAnimatorController)
            return;

        if (!stateCache || animator.runtimeAnimatorController != stateCache.AnimatorController)
        {
            stateCache = AnimatorStateCacheCompiler.GetOrCreateStateCache(animator.runtimeAnimatorController);
            EditorUtility.SetDirty(this);
        }
    }
#endif
}