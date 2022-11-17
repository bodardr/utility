using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.Rendering;

public static class VolumeExtensions
{
    public static TweenerCore<float, float, FloatOptions> DOWeight(this Volume volume, float endValue, float duration)
    {
        return DOTween.To(() => volume.weight, val => volume.weight = val, endValue, duration);
    }
}