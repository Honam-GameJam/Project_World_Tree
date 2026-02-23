using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;

    [Header("UI")]
    public RectTransform tooltipRoot;
    public TMP_Text nameText;
    public TMP_Text descText;
    public TMP_Text valueText;

    [Header("Settings")]
    public float hoverDelay = 0.5f;
    public Vector2 offset = new Vector2(20, -20);

    Canvas canvas;
    Coroutine showRoutine;
    bool isShowing;

    void Awake()
    {
        Instance = this;
        canvas = GetComponentInParent<Canvas>();
        tooltipRoot.gameObject.SetActive(false);

    }

    void Update()
    {
        if (!isShowing) return;
        FollowMouse();
    }

    void FollowMouse()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePos,
            canvas.worldCamera,
            out pos);

        pos += offset;

        Vector2 size = tooltipRoot.sizeDelta;
        Vector2 canvasSize = (canvas.transform as RectTransform).sizeDelta;

        pos.x = Mathf.Clamp(pos.x, -canvasSize.x / 2, canvasSize.x / 2 - size.x);
        pos.y = Mathf.Clamp(pos.y, -canvasSize.y / 2 + size.y, canvasSize.y / 2);

        tooltipRoot.anchoredPosition = pos;
    }

    public void Show(string title, string desc, int value)
    {
        if (showRoutine != null) StopCoroutine(showRoutine);
        showRoutine = StartCoroutine(ShowDelay(title, desc, value));
    }

    IEnumerator ShowDelay(string title, string desc, int value)
    {
        yield return new WaitForSeconds(hoverDelay);

        nameText.text = title;
        descText.text = desc;
        valueText.text = $"°¡Ä¡ {value}";

        tooltipRoot.gameObject.SetActive(true);
        isShowing = true;
    }

    public void Hide()
    {
        if (showRoutine != null) StopCoroutine(showRoutine);
        tooltipRoot.gameObject.SetActive(false);
        isShowing = false;
    }
}