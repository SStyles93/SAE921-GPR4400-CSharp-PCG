using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorPopulate :  LinkPopulate
{
    public override void PcgPopulate()
    {
        Instantiate(_prefabTank.BossDoor, transform.position, Quaternion.identity);
    }
}
