using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

[Serializable]
public class SerializableAnimatorStateInfo
{
    [SerializeField]
    private int layerIndex;

    [SerializeField]
    private int stateNameHash;

    [SerializeField]
    private string stateName;

    [SerializeField]
    private float absoluteLength;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float speedMultiplier;

    [SerializeField]
    private int tagHash;

    [SerializeField]
    private bool loops;

#if UNITY_EDITOR
    public SerializableAnimatorStateInfo(AnimatorState state, Motion motion, int layerIndex)
    {
        this.layerIndex = layerIndex;
        speedMultiplier = state.speed;
        stateNameHash = state.nameHash;
        stateName = state.name;
        tagHash = Animator.StringToHash(state.tag);

        absoluteLength = motion ? motion.averageDuration / speedMultiplier : 0;
        loops = motion && motion.isLooping;
    }
#endif

    /// <summary>
    /// The state's layer index.
    /// </summary>
    public int LayerIndex => layerIndex;

    /// <summary>
    ///   <para>The full path hash for this state.</para>
    /// </summary>
    public int StateNameHash => stateNameHash;

    /// <summary>
    /// The state's name
    /// </summary>
    public string StateName => stateName;

    /// <summary>
    ///   <para>Current duration of the state.</para>
    /// </summary>
    public float AbsoluteLength => absoluteLength;

    /// <summary>
    ///   <para>The playback speed of the animation. 1 is the normal playback speed.</para>
    /// </summary>
    public float Speed => speed;

    /// <summary>
    ///   <para>The Tag of the State.</para>
    /// </summary>
    public int TagHash => tagHash;

    /// <summary>
    ///   <para>Is the state looping.</para>
    /// </summary>
    public bool Loops => loops;
}