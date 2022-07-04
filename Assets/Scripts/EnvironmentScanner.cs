using UnityEngine;

public class EnvironmentScanner
{
    private Transform player;
    private Vector3 forwardRayCastOffSet;
    private float heightRayLength;
    private LayerMask layerMask;
    private float forwardRayLength;

    public EnvironmentScanner(Transform transform, Vector3 forwardRayCastOffset, LayerMask layerMask, float forwardRaylength, float heightRayLength)
    {
        player = transform;
        this.forwardRayCastOffSet = forwardRayCastOffset;
        this.layerMask = layerMask;
        this.forwardRayLength = forwardRaylength;
        this.heightRayLength = heightRayLength;
    }

    public (bool, RaycastHit, bool, RaycastHit) ObstrucleCheck()
    {
        RaycastHit forwardHit;
        RaycastHit heightHit = new RaycastHit();
        bool hasHeightHit = false;
        bool hasForwardHit = Physics.Raycast(player.position + forwardRayCastOffSet, player.forward, out forwardHit, forwardRayLength, layerMask);

        Debug.DrawRay(player.position + forwardRayCastOffSet, player.forward * forwardRayLength, hasForwardHit? Color.red : Color.white);

        if(hasForwardHit)
        {
            Vector3 heightRayOriginPoint = forwardHit.point + (Vector3.up * heightRayLength);
            hasHeightHit = Physics.Raycast(heightRayOriginPoint, Vector3.down, out heightHit, heightRayLength, layerMask);

            Debug.DrawRay(heightRayOriginPoint, Vector3.down * heightRayLength, Color.red);
        }

        return (hasForwardHit, forwardHit, hasHeightHit, heightHit);
    }
}
