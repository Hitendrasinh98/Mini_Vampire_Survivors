using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.WeaponSystem
{
    /// <summary>
    /// This is projectile controller for weapon shoot's
    /// hanle collision and do some damage 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public GameObject muzzlePrefab;
        public GameObject hitPrefab;
        public List<GameObject> trails;


        float speed;
        float Range;
        float Damage;

        Vector3 targetPos;
        Vector2 direction;

        /// <summary>
        /// Will Set the pojectile with their data
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="Range"></param>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public void Init(float damage, float Range , float speed , Vector3 direction)
        {
            this.speed = speed;
            this.Damage = damage;
            this.Range = Range;
            Spawn_Muzzle();
            targetPos = transform.position + direction.normalized * Range; 
        }


        void Update()
        {
            direction = targetPos - transform.position;
            if (direction.magnitude < 0.25f)
            {
                Destroy(gameObject);
            }
            transform.position = Vector2.Lerp(transform.position,targetPos, (speed * Time.deltaTime));
        }

        
        void Spawn_Muzzle()
        {
            if (muzzlePrefab != null)
            {
                var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
                muzzleVFX.transform.forward = gameObject.transform.forward;
                var ps = muzzleVFX.GetComponent<ParticleSystem>();
                if (ps != null)
                    Destroy(muzzleVFX, ps.main.duration);
                else
                {
                    var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(muzzleVFX, psChild.main.duration);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Core.IDamagable>().TakeDamage(Damage);
            }

            ContactPoint2D firstContactPoint = collision.contacts[0];
            OnCollide(firstContactPoint);
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                // Handle damage to enemy
                collision.GetComponent<Core.IDamagable>().TakeDamage(Damage);
                Destroy(gameObject);
            }
        }

        void OnCollide(ContactPoint2D contact)
        {
            if (trails.Count > 0)
            {
                for (int i = 0; i < trails.Count; i++)
                {
                    trails[i].transform.parent = null;
                    var ps = trails[i].GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        ps.Stop();
                        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                    }
                }
            }


            Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
            Vector2 pos = contact.point;

            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rot);
                var ps = hitVFX.GetComponent<ParticleSystem>();
                if (ps == null)
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
                else
                    Destroy(hitVFX, ps.main.duration);
            }

        }
    }
}