using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class GridGenerator : MonoBehaviour
{

    public GameObject GridPrefab;

    public bool Run = true;

    private List<DetectedPlane> m_NewDetectedPlane = new List<DetectedPlane>();

    private List<GameObject> m_MadeGrids = new List<GameObject>();

    public

    void Update()
    {
        if(Run == false)
        {
            for(int i = 0; i < m_MadeGrids.Count; ++i)
            {
                Destroy(m_MadeGrids[i]);
            }
            Destroy(gameObject);
        }

        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        Session.GetTrackables<DetectedPlane>(m_NewDetectedPlane, TrackableQueryFilter.New);

        for(int i = 0; i < m_NewDetectedPlane.Count; ++i)
        {
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);
            grid.GetComponent<GridVisualizer>().Initialize(m_NewDetectedPlane[i]);
            m_MadeGrids.Add(grid);
        }
    }
}
