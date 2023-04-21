using UnityEngine;

public class csCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public float moveSpeed;
    private Vector2 targetPosition;

    public BoxCollider2D bound;

    private Vector2 minBound;
    private Vector2 maxBound;

    private float halfWidth;
    private float halfHeight;

    private Camera theCamera;

    public bool isEvent;

    private void Awake()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (target.gameObject != null && !isEvent)
        {
            targetPosition.Set(target.position.x, target.position.y);

            this.transform.position = Vector2.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector2(clampedX, clampedY);
        }
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }



}