using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableID : MonoBehaviour
{
    [SerializeField] private ItemID id;

    public ItemID Id => id;
}
