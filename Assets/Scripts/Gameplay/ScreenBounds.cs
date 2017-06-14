using UnityEngine;
using System.Collections;

public class ScreenBounds : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3 middlePos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 5f));
        gameObject.transform.position = middlePos;

        float height = Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 5f)), Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 5f)));

        // Left Collider
        BoxCollider2D left = gameObject.AddComponent<BoxCollider2D>();
        Vector3 leftPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 5f));
        leftPos.x -= 2f;

        left.offset = new Vector2(leftPos.x, leftPos.y);
        left.size = new Vector2(1f, height);

        // Right Collider
        BoxCollider2D right = gameObject.AddComponent<BoxCollider2D>();
        Vector3 rightPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 5f));
        rightPos.x += 2f;

        right.offset = new Vector2(rightPos.x, rightPos.y);
        right.size = new Vector2(1f, height);

    }
}
