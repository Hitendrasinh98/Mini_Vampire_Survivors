using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    public class Health : MonoBehaviour, IHealth
    {
        [Header("Current Progress")]
        [SerializeField] int maxHealth = 100;
        [field: SerializeField] public int currentHealth { get; private set; }

        System.Action OnDied;
        public void AddObserver_OnDied(System.Action callback) => OnDied += callback;
        public void RemoveObserver_OnDied(System.Action callback) => OnDied -= callback;
        public bool IsAlive => currentHealth > 0;





        public void Init(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = this.maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        }


        void Die()
        {
            OnDied?.Invoke();
        }
    }
}
