using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 2.0f; // Adjust the speed of movement

    private bool movingRight = true;

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Check if the object has reached the rightmost position
        if (transform.position.x >= 4)
        {
            movingRight = false;
        }

        // Check if the object has reached the leftmost position
        if (transform.position.x <= -2.3f)
        {
            movingRight = true;
        }
    }
}
