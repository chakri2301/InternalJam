using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    Camera mainCamera;
    public float scrollFactor = 0.1f;
    public float minSize = 1f;
    public float maxSize = 6f;
    Vector3 velocity = new();
    public float speed = 10f;
    void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    void OnScroll(InputValue inputValue)
    {
        Debug.Log("Scroll value" + inputValue.Get<float>());
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize + scrollFactor * inputValue.Get<float>(), minSize, maxSize);
    }
    void OnMove(InputValue inputValue)
    {
        Vector2 velDir = inputValue.Get<Vector2>();
        velocity = (Vector3)velDir;
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * velocity);
    }

}
