using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Player playerScript; // this makes my camera player bound. Could be fixed with some generalisation, but there is likely an easier way.
    [SerializeField] private Vector3 offset; // from the traget's position
    [SerializeField] private float smoothTime;
    [SerializeField] private float lookAheadOffset;

    [Header("Bounds Debug")]
    [SerializeField] private bool useBounds;
    [SerializeField] private float minX; // if not for the showing no non-game area, I would have been more permissive. Also, it is a bit more permissive on the right as you can cling on wall, so if you overshoot, you can grab the wall and save yourself
    [SerializeField] private float maxX;
    [SerializeField] private float minY; 
    [SerializeField] private float maxY; // put a big number because I don't really want a ceiling

    private Vector3 velocity;

    void LateUpdate()
    {
        Vector3 smoothPosition = Vector3.SmoothDamp( // using SmoothDamp because it feels smoother and more organic than lerp
            transform.position, // current position
            target.position + offset + new Vector3( // desired position
                lookAheadOffset * playerScript.direction, // direction is 1 or -1, based on move
                0, 0), 
            ref velocity, // keeps the speed cosistant
            smoothTime); // how smooth does the cam move

        if (useBounds)
        {
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minX, maxX);
            smoothPosition.y = Mathf.Clamp(smoothPosition.y, minY, maxY); 
        }

        transform.position = smoothPosition;
    }
}
