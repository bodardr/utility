#if UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

[InitializeOnLoad]
public class AnimatorStateCacheCompiler
{
    public static readonly string STATE_CACHE_INFO_PATH = $"Assets/{nameof(AnimatorStateCache)}s";

    static AnimatorStateCacheCompiler()
    {
        //remove callback at the beginning of static constructor idea taken
        //from : https://forum.unity.com/threads/run-code-on-add-component.452684/#post-7971393
        EditorApplication.playModeStateChanged -= CompileStateCaches;
        EditorApplication.playModeStateChanged += CompileStateCaches;

        ObjectFactory.componentWasAdded -= OnComponentAdded;
        ObjectFactory.componentWasAdded += OnComponentAdded;

        EditorApplication.quitting -= OnQuit;
        EditorApplication.quitting += OnQuit;
    }

    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider()
    {
        var provider = new SettingsProvider("Project/Animator State Cache Settings", SettingsScope.Project,
            new[] { "State", "Cache", "Animator" })
        {
            label = "Animator State Cache",
            activateHandler = (_, rootElement) =>
            {
                var stateCacheSettings = new SerializedObject(AnimatorStateCacheSettings.Instance);

                rootElement.style.flexGrow = 1;
                rootElement.style.paddingTop = 8;

                var title = new Label("Animator State Cache");
                title.AddToClassList("builder-settings-header");
                title.style.fontSize = 18;
                title.style.unityFontDefinition = new StyleFontDefinition(EditorStyles.boldFont);
                title.style.marginTop = 8;
                title.style.marginLeft = 16;

                rootElement.Add(title);

                var inspector = new InspectorElement(stateCacheSettings);
                rootElement.Add(inspector);
            }
        };

        return provider;
    }

    public static AnimatorStateCache GetOrCreateStateCache(RuntimeAnimatorController animator)
    {
        if (!AssetDatabase.IsValidFolder(STATE_CACHE_INFO_PATH))
            AssetDatabase.CreateFolder("Assets/", nameof(AnimatorStateCache) + "s");

        var stateCaches = AssetDatabaseUtility.LoadAssetsInFolder<AnimatorStateCache>(STATE_CACHE_INFO_PATH).ToList();
        return stateCaches.FirstOrDefault(x => x.AnimatorController == animator) ?? CreateStateCache(animator);
    }

    private static AnimatorStateCache CreateStateCache(RuntimeAnimatorController animator)
    {
        AssetDatabase.TryGetGUIDAndLocalFileIdentifier(animator, out var guid, out long _);

        var stateCache = ScriptableObject.CreateInstance<AnimatorStateCache>();
        stateCache.AnimatorController = animator;

        AssetDatabase.CreateAsset(stateCache, $"{STATE_CACHE_INFO_PATH}/{animator.name} ({guid[..4]}).asset");
        AssetDatabase.SaveAssets();

        return stateCache;
    }

    private static void CompileStateCaches(PlayModeStateChange obj)
    {
        if (obj != PlayModeStateChange.ExitingEditMode || !AssetDatabase.IsValidFolder(STATE_CACHE_INFO_PATH))
            return;

        var assets = AssetDatabaseUtility.LoadAssetsInFolder<AnimatorStateCache>(STATE_CACHE_INFO_PATH);

        Stopwatch stopwatch = Stopwatch.StartNew();

        foreach (var stateCache in assets)
        {
            SerializeStateCache(stateCache);
            new SerializedObject(stateCache).ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(stateCache);
        }

        AssetDatabase.SaveAssets();

        stopwatch.Stop();

        Debug.Log($"Compiling State Caches Finished. Time elapsed <b>{stopwatch.ElapsedMilliseconds}ms</b>");
    }

    private static void SerializeStateCache(AnimatorStateCache stateCache)
    {
        var controllerAsset = stateCache.AnimatorController;

        stateCache.SerializedStates.Clear();
        if (controllerAsset is AnimatorOverrideController overrideController)
            FillStateCache(overrideController, stateCache);
        else
            FillStateCache((AnimatorController)controllerAsset, stateCache);
    }

    private static void FillStateCache(AnimatorOverrideController overrideController, AnimatorStateCache asset)
    {
        var originalController = (AnimatorController)overrideController.runtimeAnimatorController;

        List<KeyValuePair<AnimationClip, AnimationClip>> overrides =
            new List<KeyValuePair<AnimationClip, AnimationClip>>();
        overrideController.GetOverrides(overrides);

        for (var i = 0; i < originalController.layers.Length; i++)
        {
            var layer = originalController.layers[i];
            var statesInfos = new List<SerializableAnimatorStateInfo>();

            foreach (var state in layer.stateMachine.states)
            {
                var initialMotion = state.state.motion;

                var overrideClip = overrides.Find(x => x.Key == initialMotion).Value;

                statesInfos.Add(new SerializableAnimatorStateInfo(state.state,
                    overrideClip ? overrideClip : initialMotion, i));
            }

            asset.SerializedStates.Add(layer.name, statesInfos.ToArray());
        }
    }

    private static void FillStateCache(AnimatorController animatorController, AnimatorStateCache asset)
    {
        for (var i = 0; i < animatorController.layers.Length; i++)
        {
            var layer = animatorController.layers[i];
            var statesInfos = new List<SerializableAnimatorStateInfo>();

            foreach (var state in layer.stateMachine.states)
                statesInfos.Add(new SerializableAnimatorStateInfo(state.state,
                    animatorController.GetStateEffectiveMotion(state.state, i), i));

            asset.SerializedStates.Add(layer.name, statesInfos.ToArray());
        }
    }

    private static void OnComponentAdded(Component component)
    {
        if (!AnimatorStateCacheSettings.Instance.AddStateCacheToAnimatorsAutomatically)
            return;

        if (component is not Animator anim)
            return;


        var stateCacheReference = component.gameObject.AddComponent<AnimatorStateCacheReference>();
        stateCacheReference.Animator = anim;
    }

    private static void OnQuit()
    {
        EditorApplication.playModeStateChanged -= CompileStateCaches;
        ObjectFactory.componentWasAdded -= OnComponentAdded;
        EditorApplication.quitting -= OnQuit;
    }

    /*[MenuItem("Tools/ANIMATOR CACHE THING")]
    public static void AnimatorCacheThing()
    {
        foreach (var stateCache in AssetDatabaseUtility.LoadAssetsInFolder<AnimatorStateCache>(STATE_CACHE_INFO_PATH))
        {
            stateCache.AnimatorController =
                AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(
                    AssetDatabase.GUIDToAssetPath(stateCache.AnimatorControllerGUID));
            new SerializedObject(stateCache).ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(stateCache);
        }
        
        AssetDatabase.SaveAssets();
    }*/
}
#endif