using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupBehavior : DefaultPickupBehavior
{
    public override void OnPickup()
    {
        GameManager.Instance.GetPlayerInstance().GetComponent<Health>().ResetHP();

        base.OnPickup();
    }
}
