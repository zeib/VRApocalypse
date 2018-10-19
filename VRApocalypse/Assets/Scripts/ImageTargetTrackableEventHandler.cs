/*
 * This script pauses the game when the target is lost or found, 
 * showing or removing the pause message and freezing time
 */
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ImageTargetTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PUBLIC_MEMBER_VARIABLES
    public GameObject pausedTextLeft;
    public GameObject pausedTextRight;
    #endregion PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES
    private TrackableBehaviour mTrackableBehaviour;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region PUBLIC_METODS
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }


    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS
    private void OnTrackingFound()
    {
        Time.timeScale = 1.0f;
        pausedTextLeft.SetActive(false);
        pausedTextRight.SetActive(false);
    }

    private void OnTrackingLost()
    {
        Time.timeScale = 0.0f;
        pausedTextLeft.SetActive(true);
        pausedTextRight.SetActive(true);
    }

    #endregion // PRIVATE_METHODS
}
