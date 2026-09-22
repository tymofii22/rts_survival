using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private EquipIDs equipID;
    [SerializeField] private ToolIDs toolIDs;
    [SerializeField] private MaterialIDs materialIDs;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMove>())
        {

        }
    }

}
