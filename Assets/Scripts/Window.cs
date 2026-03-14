using UnityEngine;

public class Window : MonoBehaviour, IDamageable
{
    [SerializeField]public float hitsToBreak = 4;
    private bool isSmashed = false;

    //private void Start()
    //{
    //    windowHealth = hitsToBreak * PlayerAttack.WeaponDamage;
    //}
    public bool IsAlive
    {
        get { return hitsToBreak > 0 && !isSmashed; }
    }
    public void ApplyDamage(float damageAmount)
    {

        if (!IsAlive) return;

        //windowHealth -= damageAmount;
        hitsToBreak--;

        if (hitsToBreak <= 0)
        {
            isSmashed = true;
            Destroy(gameObject);
            Debug.Log("Window smashed");
            gameObject.SetActive(false);
        }
        else
        {

            Debug.Log($"Damage applied: {damageAmount}. Remaining hits: {hitsToBreak}");
        }
    }
}
