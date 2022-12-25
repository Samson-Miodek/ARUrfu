using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImgController : MonoBehaviour
{
    // ������
    public GameObject Anchor;
    private bool FindAnchorbool;
    // ������ ��� ������, ������� ��������������� ������������ Anchor
    public GameObject Room;
    public TMP_Text debug;

    public bool SetAnchor;


    private ARTrackedImageManager ARTrackedImageManagerScript;

    private void Awake()
    {
        ARTrackedImageManagerScript = FindObjectOfType<ARTrackedImageManager>();
    }

    public void ToggleAnchor()
    {
        SetAnchor = true;
    }

    private void MyDebug(string qw)
    {
        debug.text = qw;
    }

    public void FindAnchor()
    {
        FindAnchorbool = true;
    }

    // Update is called once per frame
    void Update()
    {
        // ���������� Anchor
        if (FindAnchorbool)
        {
            FindAnchorbool = false;
            MyDebug("Anchor starting");
            Anchor = GameObject.FindWithTag("Anchor");
            if(Anchor is null)
            {
                MyDebug("Anchor not found");
                return;
            }
            Anchor.name = "Anchor";
            MyDebug("Anchor set");
   
        }


        if (SetAnchor)
        {

            SetAnchor = false;
            if (Anchor is null)
            {
                MyDebug("Anchor not set");
                return;
            }
            // ��������� ������� Room � ����� ������� Anchor
            var state = Instantiate(Room, Anchor.transform.position, Anchor.transform.rotation);
            state.transform.rotation = new Quaternion(state.transform.rotation.x + 0.7f, state.transform.rotation.y, state.transform.rotation.z, state.transform.rotation.w);

            MyDebug("obj created");
            // ���������� ������������ ����������� (�����)
            ARTrackedImageManagerScript.SetTrackablesActive(false);

        }

    }
}
