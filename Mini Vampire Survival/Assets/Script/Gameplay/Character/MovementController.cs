using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] Transform targetObject;
        [SerializeField] float moveSpeed = 5f;
        
        private Vector2 moveDirection;

        void Update()
        {
            ProcessInputs();
        }

        void FixedUpdate()
        {
            Move();
        }

        void ProcessInputs()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector2(moveX, moveY).normalized;
        }

        void Move()
        {
            targetObject.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
