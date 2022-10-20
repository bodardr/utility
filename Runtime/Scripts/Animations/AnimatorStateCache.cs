using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateCache : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private string animatorControllerGUID;

    [HideInInspector]
    [SerializeField]
    private string[] keys;

    [HideInInspector]
    [SerializeField]
    private string[] values;

    private Dictionary<string, List<SerializableAnimatorStateInfo>> serializedStates = new();

    public string AnimatorControllerGUID
    {
        get => animatorControllerGUID;
        set => animatorControllerGUID = value;
    }

    public Dictionary<string, List<SerializableAnimatorStateInfo>> SerializedStates => serializedStates;

    public List<SerializableAnimatorStateInfo> this[string key] => SerializedStates[key];

    public void OnBeforeSerialize() => serializedStates?.Serialize(out keys, out values);

    public void OnAfterDeserialize() => serializedStates =
        DictionaryExtensions.Deserialize<string, List<SerializableAnimatorStateInfo>>(keys, values);
}