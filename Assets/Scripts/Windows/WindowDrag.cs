using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WindowDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Cache")]
    [SerializeField] private RectTransform headerTransform;
    [SerializeField] private RectTransform windowTransform;
    [SerializeField] private RectTransform contentTransform;

    private Vector3 dragOffset;
    private Canvas canvas;
    private RectTransform desktop;


    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        desktop = GameObject.FindGameObjectWithTag("Border").GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragOffset = GetScaledMousePosition() - windowTransform.localPosition;
        dragOffset.z = 0;

        windowTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 position = GetScaledMousePosition() - dragOffset;

        position = ClampToDesktop(position);

        windowTransform.localPosition = position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragOffset = Vector3.zero;
    }

    private Vector3 GetScaledMousePosition()
    {
        return Input.mousePosition / canvas.scaleFactor;
    }

    private Vector3 ClampToDesktop(Vector3 _position)
    {
        Vector3 clampedPos = _position;

        clampedPos.x = Mathf.Clamp(clampedPos.x, (desktop.position.x - (desktop.rect.width / 2.0f)) + (headerTransform.rect.width / 2.0f), (desktop.position.x + (desktop.rect.width / 2.0f)) - (headerTransform.rect.width / 2.0f));
        clampedPos.y = Mathf.Clamp(clampedPos.y, (desktop.position.y - (desktop.rect.height / 2.0f)) - (contentTransform.rect.height / 2.0f) + (headerTransform.rect.height / 2.0f), (desktop.position.y + (desktop.rect.height / 2.0f)) - (contentTransform.rect.height / 2.0f) - (headerTransform.rect.height / 2.0f));
        clampedPos.z = 0;

        return clampedPos;
    }
}
