using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LaunchPad : MonoBehaviour
{
    [SerializeField]
    float force;

    void OnTriggerEnter2D(Collider2D collider) {
        Movement movement = collider.GetComponent<Movement>();
        if (movement !=null ) {
            movement.velocity.y = force;
            print(movement.velocity.y);
        }
    }
}
