using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float attackDistance;
    public float AttackDistance { get { return attackDistance; } protected set { attackDistance = value; } }

    [SerializeField] protected int damage;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float cooldown;

    [SerializeField] protected int maxAmmo;
    protected int currentAmmo;
    [SerializeField] protected float reloadTime;

    [SerializeField] protected string projectileTag;
    [SerializeField] protected Transform firePoint;
    public InventorySlot slot;

    protected bool canShoot = true;
    protected bool reloading = false;

    private void Start()
    {
        currentAmmo = slot.amount;
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
        slot.SetBulletsAmount(currentAmmo, maxAmmo);
    }

    public virtual void Shoot()
    {
        ObjectPooler.instance.Spawn(projectileTag, firePoint.position, firePoint.rotation).GetComponent<Projectile>().Set(projectileSpeed, damage, "Enemy");
        currentAmmo -= 1;
        slot.SetBulletsAmount(currentAmmo, maxAmmo);

        StartCoroutine(Cooldown());

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    protected IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    public bool CanAttack()
    {
        return canShoot && currentAmmo > 0 && !reloading;
    }

    protected IEnumerator Reload()
    {
        InventorySlot ammoSlot = Inventory.instance.FindSlotWithAmmo();

        while (ammoSlot == null)
        {
            yield return new WaitForSeconds(0.1f);
            ammoSlot = Inventory.instance.FindSlotWithAmmo();
        }

        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
        currentAmmo = maxAmmo;
        ammoSlot.ChangeAmount(-1);
        slot.SetBulletsAmount(currentAmmo, maxAmmo);
    }
}
