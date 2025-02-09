﻿using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Bodardr.Utility.Runtime
{
    public class TransitionCanvas : DontDestroyOnLoad<TransitionCanvas>
    {
        private const string TRANSITION_CANVAS = "TRANSITION CANVAS";
        private static Image transitionImage;

        private void Awake()
        {
            gameObject.name = TRANSITION_CANVAS;

            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            canvas.sortingOrder = 99;

            gameObject.AddComponent<GraphicRaycaster>();

            var image = Instantiate(new GameObject("Image", typeof(RectTransform), typeof(Image)), transform);
            var rectTransform = image.GetComponent<RectTransform>();

            //Stretch
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;

            transitionImage = image.GetComponent<Image>();
            transitionImage.color = Color.clear;

            gameObject.SetActive(false);
        }

        public static void ChangeScene(string sceneName, Action loadCallback = null, float easeDuration = 1f)
        {
            Coroutiner.Instance.StartCoroutine(ChangeSceneCoroutine(sceneName, loadCallback, easeDuration));
        }

        public static IEnumerator ChangeSceneCoroutine(string sceneName, Action loadCallback = null,
            float easeDuration = 1f)
        {
            yield return Instance.FadeOutCoroutine(easeDuration);

            yield return SceneManager.LoadSceneAsync(sceneName);

            loadCallback?.Invoke();

            yield return Instance.FadeInCoroutine(easeDuration);
        }

        public static IEnumerator FadeIn(float duration = 1)
        {
            yield return Instance.FadeInCoroutine(duration);
        }

        public static IEnumerator FadeOut(float duration = 1)
        {
            yield return Instance.FadeOutCoroutine(duration);
        }

        private IEnumerator FadeOutCoroutine(float duration = 1)
        {
            if (transitionImage == null)
                Awake();
            
            gameObject.SetActive(true);

            yield return DOTween.ToAlpha(() => transitionImage.color, value => transitionImage.color = value, 1,
                    duration)
                .From(0).SetEase(Ease.InOutSine).SetUpdate(true).WaitForCompletion();
        }

        private IEnumerator FadeInCoroutine(float duration = 1)
        {
            if (transitionImage == null)
                Awake();

            yield return DOTween.ToAlpha(() => transitionImage.color, value => transitionImage.color = value, 0,
                    duration)
                .From(1).SetEase(Ease.InOutSine).SetUpdate(true).WaitForCompletion();

            gameObject.SetActive(false);
        }
    }
}
