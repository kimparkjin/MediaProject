using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ARGameController : MonoBehaviour
{
    public GameObject Portal;

    public GameObject ARCamera;

    public GridGenerator Generator;

    public GameObject EnemyPrefab;

    public GameObject Player;

    private bool isAnchorMade = false;

    // Start is called before the first frame updated
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnchorMade == false)
        {
            _generateAnchor();
        }
        else
        {
            // Make player be able to fire
            Player.GetComponent<PlayerController>().B_Fire = true;
            Destroy(gameObject);
        }
    }

    private void _generateAnchor()
    {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHit hit;
        if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            // Enable the portal
            Portal.SetActive(true);

            // Create a new Anchor
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            // Set the position of the portal to be the same as the hit position
            Portal.transform.position = hit.Pose.position;
            Portal.transform.rotation = hit.Pose.rotation;

            // We want the portal to face the camera
            Vector3 cameraPosition = ARCamera.transform.position;

            // The portal should only rotate around the Y axis
            cameraPosition.y = hit.Pose.position.y;

            // Rotate the portal to face the camera
            Portal.transform.LookAt(cameraPosition, Portal.transform.up);

            // Attach Portal to the anchor
            Portal.transform.parent = anchor.transform;

            // Create Enemy
            var enemy = GameObject.Instantiate(EnemyPrefab);
            enemy.transform.Translate(Portal.transform.position + Portal.transform.forward * (-5.0f));
            enemy.transform.LookAt(Portal.transform);
            Quaternion rot = enemy.transform.rotation;
            Vector3 euler = rot.eulerAngles;
            euler.x = 0.0f;
            euler.z = 0.0f;
            rot = Quaternion.Euler(euler);
            enemy.transform.rotation = rot;
            enemy.transform.parent = anchor.transform;

            enemy.GetComponent<EnemyController>().Player = ARCamera;

            // indicate that anchor have been made.
            isAnchorMade = true;

            // we don't need plane generator from this point
            Generator.Run = false;
        }
    }

}
