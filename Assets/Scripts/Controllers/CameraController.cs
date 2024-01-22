using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector3 _delta;
    [SerializeField]
    GameObject _player = null;

    private void Update()
    {
        transform.position = _player.transform.position + _delta;
    }
}
