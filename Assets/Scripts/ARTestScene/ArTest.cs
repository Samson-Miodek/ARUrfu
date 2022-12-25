using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ArTest : MonoBehaviour
{
    [SerializeField] private GameObject planeMarker;
    [SerializeField] private GameObject CUBEGameObject;

    public GameObject Room;

    private GameObject Archor;

    public bool setArchor;

    private ARRaycastManager ARRaycastManagerObj;
    void Start()
    {
        ARRaycastManagerObj = FindObjectOfType<ARRaycastManager>();
        m_TrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        Archor = GameObject.FindWithTag("Archor");
        Instantiate(Room, Archor.transform.position, Archor.transform.rotation);


    }

    void Update()
    {
        return;



        if (Input.touches[0].phase != TouchPhase.Began) return;

        if (setArchor)
        {


            Archor = GameObject.FindWithTag("Archor");
            Instantiate(Room, Archor.transform.position, Archor.transform.rotation);
            setArchor = false;
            return;
        }
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManagerObj.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        planeMarker.transform.position = hits[0].pose.position;

        Instantiate(CUBEGameObject, hits[0].pose.position, Quaternion.identity);

    }

    public void ArchorToggle()
    {
        setArchor = !setArchor;
    }

    public void SetState()
    {

    }



    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            Debug.Log(newImage.transform.position);
        }
    }
}
