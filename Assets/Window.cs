using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Window : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Canvas canvas;
    private bool isDragging = false;
    private Vector2 offset;

    public UnityEvent OnDragDown;
    public UnityEvent OnDragUp;

    private void Start()
    {
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                Mouse.current.position.ReadValue(),
                canvas.worldCamera,
                out position);

            transform.position = canvas.transform.TransformPoint(position - offset);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        OnDragDown.Invoke();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            eventData.position,
            canvas.worldCamera,
            out var localMousePosition);

        offset = localMousePosition - (Vector2)transform.localPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        OnDragUp.Invoke();
        offset = Vector2.zero;
    }
}
