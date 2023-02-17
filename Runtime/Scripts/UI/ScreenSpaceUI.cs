using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("UI/Screen-Space UI")]
public class ScreenSpaceUI : MonoBehaviour
{
    private new Camera camera;
    private Canvas parentCanvas;
    private RectTransform rectTransform;

    private Vector3 screenPosition;

    private bool isCulled;

    [SerializeField]
    private UpdateType updateType;

    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private Vector2 viewportOffset;

    [SerializeField]
    private UnityEvent onCulled;

    [SerializeField]
    private UnityEvent onUnculled;

    public Transform Target
    {
        get => target;
        set => target = value;
    }


    public Vector3 ScreenPosition => screenPosition;

    public bool IsCulled
    {
        get => isCulled;
        set
        {
            if (value == IsCulled)
                return;
            
            isCulled = value;
            
            if(value)
                onCulled.Invoke();
            else
                onUnculled.Invoke();
        }
    }


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();
        camera = parentCanvas.worldCamera ? parentCanvas.worldCamera : Camera.main;
    }

    private void Update()
    {
        if (updateType != UpdateType.Normal)
            return;

        UpdatePosition();
    }

    private void FixedUpdate()
    {
        if (updateType != UpdateType.Fixed)
            return;

        UpdatePosition();
    }

    private void LateUpdate()
    {
        if (updateType != UpdateType.Late)
            return;

        UpdatePosition();
    }


    public void UpdatePosition()
    {
        if (!target)
            return;

        var screenPoint = camera.WorldToScreenPoint(target.position);

        IsCulled = screenPoint.z < 0 ||
                     screenPoint.x < 0 || screenPoint.x > Screen.width ||
                     screenPoint.y < 0 || screenPoint.y > Screen.height;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform,
            screenPoint + camera.ViewportToScreenPoint(viewportOffset),
            parentCanvas.renderMode == RenderMode.ScreenSpaceCamera ? camera : null, out screenPosition);

        rectTransform.position = screenPosition;
    }

    public void SetOffset(Vector2 newOffset)
    {
        viewportOffset = newOffset;
    }
}