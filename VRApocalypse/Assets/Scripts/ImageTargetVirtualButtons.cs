/*
 * This script is responsible for the virtual button behaviours
 */
using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using System;

public class ImageTargetVirtualButtons : MonoBehaviour, IVirtualButtonEventHandler
{
    #region PUBLIC_MEMBER_VARIABLES
    #endregion // PUBLIC_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS
    void Start()
    {
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            vbs[i].RegisterEventHandler(this);
        }

    }
    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS
    /// Called when the virtual button has just been pressed:
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("OnButtonPressed::" + vb.VirtualButtonName);
        switch (vb.VirtualButtonName)
        {
            //If shoot virtual button is pressed it invokes the shoot method
            case "Shoot":
                gameObject.GetComponentInChildren<ShotgunBehaviour>().Shoot();
                break;
                //Removed slashing
                /*case "Slash":
                    gameObject.GetComponentInChildren<KnifeBehaviour>().Slash();
                    break;*/
        }

    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("OnButtonPressed::" + vb.VirtualButtonName);
    }
    #endregion // PUBLIC_METHODS
}