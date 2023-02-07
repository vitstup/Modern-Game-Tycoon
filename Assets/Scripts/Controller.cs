using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Camera uiCamera;

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;

    [SerializeField] private float wasdSpeed;
    [SerializeField] private float mouseSpeed;

    private Vector3 newPosition; // Camera will move to this point
    private Vector3 startPosition; // Used For Mouse Move
    private Vector3 mouseChange; // Used For Mouse Move

    private double PreviosUpdatesTime;
    private float ThisUpdateRealTime;

    private void Awake()
    {
        newPosition = transform.localPosition;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ThisUpdateRealTime = (float)(Time.realtimeSinceStartup - PreviosUpdatesTime);
        PreviosUpdatesTime = Time.realtimeSinceStartup;


        //if (isOverUi()) return;

        if (TimeManager.instance.runStatus == RunStatus.stoped)
        {
            newPosition = transform.localPosition;
            return;
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);

        if (Input.GetKey(KeyCode.S)) newPosition -= Vector3.up * wasdSpeed;
        if (Input.GetKey(KeyCode.W)) newPosition += Vector3.up * wasdSpeed;
        if (Input.GetKey(KeyCode.A)) newPosition -= Vector3.right * wasdSpeed;
        if (Input.GetKey(KeyCode.D)) newPosition += Vector3.right * wasdSpeed;

        MouseHandler();

        Move();
    }

    private void Zoom(float increment)
    {
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - increment, minZoom, maxZoom);
        uiCamera.orthographicSize = mainCamera.orthographicSize;
    }

    private void Move()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, ThisUpdateRealTime * 5);
    }

    private void MouseHandler()
    {
        if (Input.GetMouseButtonDown(1)) startPosition = Input.mousePosition;
        if (Input.GetMouseButton(1)) mouseChange = Input.mousePosition;

        float x = startPosition.x - mouseChange.x;
        float y = startPosition.y - mouseChange.y;

        startPosition = mouseChange;

        newPosition += new Vector3(x * mouseSpeed, y * mouseSpeed, 0);
    }
}
