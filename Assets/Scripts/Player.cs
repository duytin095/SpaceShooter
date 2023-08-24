using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    void Start()
    {
        transform.position = Vector3.zero;
    }


    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        float rightPosBound = 11.2f;
        float leftPosBound = -11.2f;
        float topPosBound = 0;
        float botPosBound = -3.9f;
        float currentPosX = transform.position.x;
        float currentPosY = transform.position.y;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(_speed * Time.deltaTime * direction);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, botPosBound, topPosBound), 0);

        if (currentPosX > rightPosBound)
        {
            transform.position = new Vector3(leftPosBound, currentPosY, 0);
        }
        else if (currentPosX < leftPosBound)
        {
            transform.position = new Vector3(rightPosBound, currentPosY, 0);
        }
    }
}
