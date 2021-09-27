using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    Transform player;

    [SerializeField]
    bool moveAround = false;

    [SerializeField] bool smooth = true;
    [SerializeField] float smoothSpeed = 0.125f;

    private Vector3 defaultOffset = new Vector3(0, 0, -10);
    //private Camera camera;
    private Vector3 offsetForOffset;
    private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] float maxDistance = 0.5f;
    [SerializeField] float scaleFactor = 0.5f;

    [System.NonSerialized]
    public Transform m_secondObjective;

    Camera myCamera;

    void Start()
    {
        player = LevelManager.Instance.Player.transform;
        myCamera = transform.GetComponentInChildren<Camera>();
        //scaleFactor = 0.5f; //This is to make the camera not go all the way to the mouse cursor position, tweak it until it feels right.
        //maxDistance = 3.0f; //This limits how far the camera can go from the player, tweak it until it feels right.
    }

    void LateUpdate()
    {
        if (player != null)
        {
            RaycastHit hit;
            Vector3 desiredPosition;
            Ray ray = myCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            /*if (Physics.Raycast(ray, out hit) && Input.GetKey(KeyCode.LeftShift))
            {
                offsetForOffset = (hit.point - player.position) * scaleFactor; // Aqui hallamos la posicion entre el raton y el jugador, y li¡o multiplicamos por una escala
            }
            else*/
            if (m_secondObjective != null)
            {
                offsetForOffset = (m_secondObjective.position - player.position) * 0.5f;

                offset = defaultOffset + offsetForOffset;
                desiredPosition = player.transform.position + offset;
            }
            else
            {
                offsetForOffset = Vector3.zero; // This makes it so that if the camera raycast doesn't hit, we go to directly over the player.

                if (offsetForOffset.magnitude > maxDistance)
                {
                    offsetForOffset.Normalize(); // Make the vector3 have a magnitude of 1
                    offsetForOffset = offsetForOffset * maxDistance;
                }
                offset = defaultOffset + offsetForOffset;

                desiredPosition = player.transform.position + offset;
            }

            if (smooth)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            }
        }
    }
}
