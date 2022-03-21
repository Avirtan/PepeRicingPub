using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void TouchHandler();
#nullable enable
    public event TouchHandler? StartTouch;
    public event TouchHandler? EndTouch;
#nullable disable
    [SerializeField]
    private bool isDebug;
    private Vector2 position;
    private Vector2 startTouchPosition;
    private Vector2 direction;
    private float startTime = 0;
    private float diffTime = 0;
    private float distanceSwap = 0;

    public float SpeedSwipe => (distanceSwap / diffTime) / 1.6f;
    public Vector2 Direction => direction;
    public Vector2 Position => position;
    public float DistanceSwap => distanceSwap;
    void Start()
    {
        startTouchPosition = Vector2.zero;
    }

    void Update()
    {
        TouchInput();
        if (isDebug)
        {
            DebugInput();
        }
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Began)
            {
                startTime = startTime == 0 ? Time.time : startTime;
                startTouchPosition = startTouchPosition == Vector2.zero ? _touch.position : startTouchPosition;
                position = _touch.position;
                direction = (position - startTouchPosition).normalized;
                StartTouch?.Invoke();
            }
            else if (_touch.phase == TouchPhase.Ended)
            {
                Vector3 inputPos = _touch.position;
                inputPos.z = Camera.main.nearClipPlane;
                diffTime = Time.time - startTime;
                distanceSwap = Vector2.Distance(position, startTouchPosition);
                EndTouch?.Invoke();
                Reset();
            }
        }
    }

    private void DebugInput()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     startTime = startTime == 0 ? Time.time : startTime;
        //     startTouchPosition = startTouchPosition == Vector2.zero ? Input.mousePosition : startTouchPosition;
        //     position = Input.mousePosition;
        //     direction = (position - startTouchPosition).normalized;
        //     StartTouch?.Invoke();
        // }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     Vector3 inputPos = Input.mousePosition;
        //     inputPos.z = Camera.main.nearClipPlane;
        //     diffTime = Time.time - startTime;
        //     distanceSwap = Vector2.Distance(position, startTouchPosition);
        //     EndTouch?.Invoke();
        //     Reset();
        // }

    }

    private void Reset()
    {
        startTouchPosition = position = Vector2.zero;
        startTime = distanceSwap = 0;
    }
}
