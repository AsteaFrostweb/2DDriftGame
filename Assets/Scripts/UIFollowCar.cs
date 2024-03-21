using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIFollowCar : MonoBehaviour
{
    public Transform target; // The car's transform
    public Camera minimapCamera; // The camera used for rendering the minimap
    public Vector2 minimap_size;
    public Vector3 offset; // Offset from the car's position
    public Vector3 scale;

    private void Start()
    {
        Vector3 screenPosition = minimapCamera.WorldToScreenPoint(target.position);

        Vector2 minimap_pos = new Vector2(Mathf.Lerp(0, minimap_size.x, (screenPosition.x / minimapCamera.pixelWidth)), Mathf.Lerp(0, minimap_size.y, (screenPosition.y / minimapCamera.pixelHeight)));
        Debug.Log("MiniMap_pos:" + minimap_pos.ToString());

        offset.x = (Screen.width - minimap_size.x);
        
    }
    // Update is called once per frame
    void Update()
    {
        offset.x = (Screen.width - minimap_size.x);

        if (target != null && minimapCamera != null)
        {
            
            // Convert the car's world position to screen space using the minimap camera
            Vector3 screenPosition = minimapCamera.WorldToScreenPoint(target.position);

            Vector2 minimap_pos = new Vector2(Mathf.Lerp(0, minimap_size.x, (screenPosition.x / minimapCamera.pixelWidth)), Mathf.Lerp(0, minimap_size.y, (screenPosition.y / minimapCamera.pixelHeight)));
            /*
            screenPosition.x *= scale.x;
            screenPosition.y *= scale.y;
            screenPosition.z *= scale.z;
            */

            Vector3 position = new Vector3(minimap_pos.x + offset.x, minimap_pos.y + offset.y, 0f);

            // Set the position of the UI element to match the car's screen position
            transform.position = position;
        }
    }
}
