    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeRoomVars : MonoBehaviour
{

    public int integer;

    public bool upExit;
    public bool downExit;
    public bool leftExit;
    public bool rightExit;

    public bool closestRoom;
    public bool furthestRoom;

    /*void Start()
    {
        if (this.gameObject.tag == "Floor")
        {
            this.transform.GetChild(2).gameObject.SetActive(true);

            //this.gameObject.GetComponents(MonoBehaviour);
            //this.gameObject.AddComponent<CompositeShadowCaster2D> ();
            //UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(this.gameObject, "Assets\Scripts\storeRoomVars.cs (23,13)", "ShadowCaster2D");
        }
    }*/
}
