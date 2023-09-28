using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] int extraLifeAddAmount = 1;

    public int ExtraLifeAddAmount { get { return extraLifeAddAmount; } }
}
