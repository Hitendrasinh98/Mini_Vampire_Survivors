using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Mini_Vampire_Surviours.Gameplay.PlayerSystem
{

    public class PlayerDamageEffect : MonoBehaviour
    {
        public Volume postProcessingVolume;
        private Vignette vignette;
        [SerializeField] float intensity = 0.6f;

        private bool isHit;
        private float hitEffectDuration = 1f;
        private float hitEffectTimer;

        private void Awake()
        {
            Core.EventManager.Instance.AddObserver_OnPlayerHit(OnPlayerHit);
        }

        private void OnDestroy()
        {
            Core.EventManager.Instance.RemoveObserver_OnPlayerHit(OnPlayerHit);
        }

        private void Start()
        {
            if (postProcessingVolume.profile.TryGet<Vignette>(out Vignette vignette))
            {
                this.vignette = vignette;
            }
        }

        private void Update()
        {
            if (isHit)
            {
                hitEffectTimer += Time.deltaTime;
                float progress = hitEffectTimer / hitEffectDuration;
                vignette.color.value = Color.Lerp(Color.red, Color.black, progress);
                vignette.intensity.value = Mathf.Lerp(intensity, 0f, progress);

                if (progress >= 1f)
                {
                    isHit = false;
                    hitEffectTimer = 0f;
                }
            }
        }

        [ContextMenu("Hit")]
        void Debug_Effect()
        {
            OnPlayerHit(00, 0, 0);
        }

        public void OnPlayerHit(float damageTook , float currentHealth ,int maxHealth)
        {
            isHit = true;
            hitEffectTimer = 0f;
            vignette.color.value = Color.red;
            vignette.intensity.value = intensity;
        }
    }
}
