using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatorStateCache : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private RuntimeAnimatorController animatorController;

    [HideInInspector]
    [SerializeField]
    private string[] keys;

    [HideInInspector]
    [SerializeField]
    private string[] values;

    private Dictionary<string, SerializableAnimatorStateInfo[]> serializedStates = new();

    public RuntimeAnimatorController AnimatorController
    {
        get => animatorController;
        set => animatorController = value;
    }

    public Dictionary<string, SerializableAnimatorStateInfo[]> SerializedStates => serializedStates;

    public SerializableAnimatorStateInfo[] this[string key] => SerializedStates[key];

    public void OnBeforeSerialize() => serializedStates?.Serialize(out keys, out values);

    public void OnAfterDeserialize() => serializedStates =
        DictionaryExtensions.Deserialize<string, SerializableAnimatorStateInfo[]>(keys, values);

    public SerializableAnimatorStateInfo FindState(Func<SerializableAnimatorStateInfo, bool> predicate) =>
        SerializedStates.SelectMany(x => x.Value).First(predicate);
}