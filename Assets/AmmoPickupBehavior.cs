using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupBehavior : DefaultPickupBehavior
{
    public override void OnPickup()
    {
        GameManager.Instance.GetPlayerInstance().GetComponent<LimitedAmmo>().RefillAmmo();

        base.OnPickup();
    }
}
