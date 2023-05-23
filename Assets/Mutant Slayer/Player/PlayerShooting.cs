using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpponentDetector))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float enemyDetectorRate;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private OpponentDetector enemyDetector;

    [SerializeField] private Weapon weapon;
    private InventorySlot weaponSlot;

    private Transform currentTarget;
    private bool shooting = false;

    private void Start()
    {
        InvokeRepeating(nameof(FindTarget), enemyDetectorRate, enemyDetectorRate);
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            WeaponAtTarget();
        }

        if (shooting && weapon.CanAttack())
        {
            weapon.Shoot();
        }
    }

    private void FindTarget()
    {
        if (weapon != null)
            currentTarget = enemyDetector.FindTarget(weapon.AttackDistance);
    }

    private void WeaponAtTarget()
    {
        Vector2 lookDir = currentTarget.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        weaponHolder.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1;
        }
        else
        {
            localScale.y = 1;
        }
        weaponHolder.localScale = localScale;
    }

    public void EquipWeaponFromSlot(InventorySlot slot)
    {
        UnequipWeapon();

        weapon = Instantiate(slot.item.obj, weaponHolder).GetComponent<Weapon>();
        weapon.slot = slot;
    }

    public void UnequipWeapon()
    {
        if (weapon != null)
            Destroy(weapon.gameObject);
    }

    public void AttackButtonDown()
    {
        shooting = true;
    }

    public void AttackButtonUp()
    {
        shooting = false;
    }
}
