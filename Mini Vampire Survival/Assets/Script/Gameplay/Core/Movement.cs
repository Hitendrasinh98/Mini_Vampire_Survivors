using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    /// <summary>
    /// Movement Component withc will be used to move any Entity with the healp of IMovalbe interface.    
    /// </summary>
    public class Movement : MonoBehaviour, IMovable
    {
        [SerializeField] float animBlendSpeed = 3;
        [SerializeField] float MaxLocomotionBlendAmount = 2;

        [Header("current Progress")]
        [SerializeField] Transform target;
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] string locoMotionBlendTreeKey;
        [SerializeField] float maxBlendAmount = 2;


        Animator animator;
        bool isFlipped;
        float targetBlendAmount;
        float currentBlendAmount;
        Vector3 initialScale;

        public void Init(Transform target, float moveSpeed, string locoMotionBlendTreeKey)
        {
            this.moveSpeed = moveSpeed;
            this.locoMotionBlendTreeKey = locoMotionBlendTreeKey;

            this.target = target;
            animator = target.GetComponent<Animator>();
            initialScale = target.localScale;
        }


        public void Move(Vector2 direction)
        {
            UpdateAnimation(direction);

            if (direction.magnitude > 0)
            {
                if (direction.x < 0 != isFlipped)
                {
                    isFlipped = !isFlipped;
                    Set_FaceFlipped(isFlipped);
                }
            }

            target.Translate(direction * moveSpeed * Time.deltaTime);
        }


        void UpdateAnimation(Vector2 direction)
        {
            targetBlendAmount = direction.magnitude > 0 ? maxBlendAmount : 0;
            currentBlendAmount = Mathf.Lerp(currentBlendAmount, targetBlendAmount, Time.deltaTime * animBlendSpeed);
            animator.SetFloat(locoMotionBlendTreeKey, currentBlendAmount);
        }

        /// <summary>
        /// Use this to flip the Player character
        /// </summary>
        /// <param name="isFliped"></param>
        public void Set_FaceFlipped(bool isFliped)
        {
            initialScale.x = isFliped ? -initialScale.x : Mathf.Abs(initialScale.x);
            target.localScale = initialScale;
        }
    }
}