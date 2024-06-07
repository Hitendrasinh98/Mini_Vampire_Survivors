using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    /// <summary>
    /// Basic Health Component will respresent the health system for their enity wher assigned
    /// </summary>
    public class Health : MonoBehaviour,IHealth
    {

        [field: Header("Current Progress"),SerializeField] public int maxHealth { get; private set; }
        [field: SerializeField] public float remainingHealth { get; private set; }

        /// <summary>
        /// Event will fired when this healht componnet is reduced belove 0
        /// </summary>
        System.Action OnDied;
        public void AddObserver_OnDied(System.Action callback) => OnDied += callback;
        public void RemoveObserver_OnDied(System.Action callback) => OnDied -= callback;
        public bool IsAlive => remainingHealth > 0;





        public void Init(int maxHealth)
        {
            this.maxHealth = maxHealth;
            remainingHealth = this.maxHealth;
        }

        public void TakeDamage(float amount)
        {
            remainingHealth -= amount;
            if (remainingHealth <= 0)
            {
                remainingHealth = 0;
                Die();
            }
        }

        public void Heal(float amount)
        {
            remainingHealth = Mathf.Min(remainingHealth + amount, maxHealth);
        }

        public void PowerHealth(float amount)
        {
            maxHealth += (int)amount;
            remainingHealth += amount;
        }

        void Die()
        {
            OnDied?.Invoke();
        }
    }
}
