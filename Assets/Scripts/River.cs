using UnityEngine;

public class River : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private Vector3 scrollDirection;
    [SerializeField] private float maxScrollDistance;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position + scrollSpeed * Time.deltaTime * scrollDirection;
        Vector3 offset = newPosition - initialPosition;

        if (Mathf.Abs(offset.x) > maxScrollDistance)
        {
            newPosition.x = initialPosition.x + (newPosition.x % maxScrollDistance);
        }
        if (Mathf.Abs(offset.y) > maxScrollDistance)
        {
            newPosition.y = initialPosition.y + (newPosition.y % maxScrollDistance);
        }

        transform.position = newPosition;
    }
}
