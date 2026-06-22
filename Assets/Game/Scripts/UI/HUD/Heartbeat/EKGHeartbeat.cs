using UnityEngine;
using UnityEngine.UI;

public class EKGHeartbeat : MonoBehaviour
{
    private LineRenderer lr;
    public float scrollSpeed = 0.1f;

    void Start() { lr = GetComponent<LineRenderer>(); }

    void Update()
    {
        // Ini bakal nge-scroll tekstur di sepanjang garis
        // Jadi garisnya kelihatan "jalan" kayak di monitor rumah sakit
        float offset = Time.time * scrollSpeed;
        lr.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
