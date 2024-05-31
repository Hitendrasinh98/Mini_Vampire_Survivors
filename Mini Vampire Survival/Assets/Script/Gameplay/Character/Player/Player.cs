using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Player
{ 
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Animator animator { get; private set; }
        


        System.Action<Collision2D> OnCollide;
        Vector3 localScale = new Vector3(1, 1, 1);

        public void AddObserver_OnCollide(System.Action<Collision2D> callback) => OnCollide += callback;
        public void RemoveObserver_OnCollide(System.Action<Collision2D> callback) => OnCollide -= callback;





        public void Init()
        {
            localScale = transform.localScale;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollide?.Invoke(collision);
        }
    }

}