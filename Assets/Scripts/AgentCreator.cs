using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCreator : MonoBehaviour
{
    public Agent[] agents;
    public LayerMask layerMask;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
            {
                Instantiate(agents[0], hitInfo.point, Quaternion.identity);
            }
        }
    }
}
