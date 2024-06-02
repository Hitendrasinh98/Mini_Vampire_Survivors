using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.WeaponSystem
{
    public enum WeaponTypeEnum { None ,MagicWand ,FireArm};
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] ConfigData.So_WeaponConfig so_WeaponConfig;

        [Header("Current Progress")]
        [SerializeField] bool isActive;

        PlayerSystem.Player player;
        private IWeapon primaryWeapon;
        private List<IWeapon> secondaryWeapons = new List<IWeapon>();

        private void Awake()
        {
            Core.EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            Core.EventManager.Instance.AddObserver_OnGameComeplete(OnGameComplete);
            Core.EventManager.Instance.AddObserver_OnPowerUpSelect(OnPowerSelect);

        }

        private void OnDestroy()
        {
            Core.EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            Core.EventManager.Instance.RemoveObserver_OnGameComeplete(OnGameComplete);
            Core.EventManager.Instance.RemoveObserver_OnPowerUpSelect(OnPowerSelect);
        }

        void OnGameStart(Core.EventManager.GameStartData gameStartData)
        {
            player = Mediator.Instance.m_Player;
            ActivateSystem(gameStartData.primaryWeapon);
        }

        void OnGameComplete()
        {
            DeActivateSystem();
        }

        void OnPowerSelect(ConfigData.LevelUPPowerEnum powerUpType, float amount)
        {
            if (powerUpType == ConfigData.LevelUPPowerEnum.FireRate)
            {
                primaryWeapon?.PowerUp_FireRate(amount);
            }
            else if (powerUpType == ConfigData.LevelUPPowerEnum.Damage)
            {
                primaryWeapon?.PowerUp_PrimaryWeaponDamage(amount);
            }
        }

        public void ActivateSystem(WeaponTypeEnum weaponType)
        {
            AddPrimaryWeapon(weaponType);
            isActive = true;
        }

        public void DeActivateSystem()
        {
            isActive = false;
        }

        void Update()
        {
            if (!isActive)
                return;

            // Auto-shoot for primary weapon
            primaryWeapon?.Shoot();

            // Auto-shoot for secondary weapons
            for (int i = 0; i < secondaryWeapons.Count; i++)
            {
                secondaryWeapons[i].Shoot();
            }
        }

        void AddPrimaryWeapon(WeaponTypeEnum weaponType)
        {
            Transform primaryWeaponSlot = player.PrimaryWeaponSlot;
            if (primaryWeapon != null)
            {
                Destroy(primaryWeaponSlot.GetChild(0).gameObject);
            }

            GameObject targetWeapon = so_WeaponConfig.availableWeapons[0].gameObject; 
            for (int i = 0; i < so_WeaponConfig.availableWeapons.Count; i++)
            {
                if (so_WeaponConfig.availableWeapons[i].weaponType == weaponType)
                {
                    targetWeapon = so_WeaponConfig.availableWeapons[i].gameObject;
                }
            }
            GameObject weapon = Instantiate(targetWeapon, primaryWeaponSlot.position, primaryWeaponSlot.rotation, primaryWeaponSlot);
            primaryWeapon = weapon.GetComponent<IWeapon>();
        }
    }
}
