using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class XRButton : XRBaseInteractable
{
    [Header("Button Settings")]
    public Transform buttonTransform;
    public float pressDistance = 0.1f;

    [Header("Button Events")]
    public UnityEvent OnPress = new UnityEvent();
    public UnityEvent OnRelease = new UnityEvent();

    private float yMin;
    private float yMax;
    private float startHeight;
    private float hoverHeight;
    private IXRHoverInteractor hoverInteractor;

    private bool previousPress = false;
    private bool isHovering = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        hoverEntered.AddListener(OnHoverEnter);
        hoverExited.AddListener(OnHoverExit);
        selectEntered.AddListener(OnSelectEnter);
        selectExited.AddListener(OnSelectExit);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        hoverEntered.RemoveListener(OnHoverEnter);
        hoverExited.RemoveListener(OnHoverExit);
        selectEntered.RemoveListener(OnSelectEnter);
        selectExited.RemoveListener(OnSelectExit);
    }

    private void Start()
    {
        if (buttonTransform == null)
        {
            Debug.LogError("Button Transform is not assigned.");
            return;
        }

        yMax = buttonTransform.localPosition.y;
        yMin = yMax - pressDistance;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && hoverInteractor != null)
        {
            float currentHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float delta = hoverHeight - currentHeight;
            float targetHeight = startHeight - delta;
            ApplyHeight(targetHeight);
        }
    }

    private void ApplyHeight(float position)
    {
        SetButtonPosition(position);
        CheckForPress();
    }

    private void SetButtonPosition(float position)
    {
        if (buttonTransform == null) return;

        Vector3 pos = buttonTransform.localPosition;
        pos.y = Mathf.Clamp(position, yMin, yMax);
        buttonTransform.localPosition = pos;
    }

    private void CheckForPress()
    {
        bool isPressed = buttonTransform.localPosition.y <= (yMin + pressDistance * 0.5f);

        if (isPressed != previousPress)
        {
            previousPress = isPressed;

            if (isPressed)
                OnPress.Invoke();
            else
                OnRelease.Invoke();
        }
    }

    private float GetLocalYPosition(Vector3 worldPosition)
    {
        return transform.InverseTransformPoint(worldPosition).y;
    }

    // ---------- Interaction Events ----------

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        hoverInteractor = args.interactorObject;
        hoverHeight = GetLocalYPosition(hoverInteractor.transform.position);
        startHeight = buttonTransform.localPosition.y;
        isHovering = true;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        hoverInteractor = null;
        isHovering = false;
        ApplyHeight(yMax);
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        // Simulate press when selected (e.g., RayInteractor + trigger click)
        OnPress.Invoke();
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        // Simulate release when unselected
        OnRelease.Invoke();
    }

    // Allow button to be selected
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return true;
    }
}
