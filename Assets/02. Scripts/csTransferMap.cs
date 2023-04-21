using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTransferMap : MonoBehaviour
{
    public csCameraFollow csCamera;

    public GameObject boundObj;
    public GameObject doorIn;
    public GameObject doorOut;

    private Transform[] mapIn;
    private Transform[] mapOut;
    private BoxCollider2D[] setBounds;

    public int mapIndex;

    private void Awake()
    {
        setBounds = boundObj.GetComponentsInChildren<BoxCollider2D>();

        mapIn = doorIn.GetComponentsInChildren<Transform>();
        mapOut = doorOut.GetComponentsInChildren<Transform>();        
    }

    public void TransferMap(Transform player, GameObject scanObject)
    {
        mapIndex = System.Convert.ToInt32(scanObject.name.Substring(3));

        if (scanObject.transform.parent.name.Contains("In"))
        {
            if (mapIndex == 0)
            {
                return;
            }
            mapIndex--;
            player.transform.position = mapOut[mapIndex].position;            
        }
        else
        {
            mapIndex++;
            player.transform.position = mapIn[mapIndex].position;
        }

        csCamera.SetBound(setBounds[mapIndex - 1]);
        
    }

}
