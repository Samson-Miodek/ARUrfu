using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{

    ARAnchorManager m_AnchorManager;
    ARPlaneManager m_PlaneManager;


    // Start is called before the first frame update
    void Awake()
    {
        m_AnchorManager = GetComponent<ARAnchorManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    [SerializeField]
ARTrackedImageManager m_TrackedImageManager;

public GameObject prefab;

void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
{
    foreach (var newImage in eventArgs.added)
    {
       // newImage.transform.position
        UpdateInfo(newImage);
    }

    foreach (var updatedImage in eventArgs.updated)
    {
        // Handle updated event
    }

    foreach (var removedImage in eventArgs.removed)
    {
        // Handle removed event
    }
}
void UpdateInfo(ARTrackedImage trackedImage)
{

  if (trackedImage.trackingState == TrackingState.Tracking)
    {
        Instantiate(prefab, transform.position, transform.rotation);


    }else{
    // Destroy object if you dont want to keep
   }
}
}
