using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "ParkourAction/Action", order = 1)]
public class ParkourActionData : ScriptableObject
{
    public float minHeight;
    public float maxHeight;
    public string ActionName;

    [Header("Target Matching Data")]
    public bool shouldTargetMatch;
    public AvatarTarget avtarTarget;
    public float startTime;
    public float endTime;
    public Vector3 positionXYZWeight;

    public bool CheckForPossibleAction(Vector3 hitPoint, Transform player)
    {
        float height = hitPoint.y - player.position.y;
        if (height >= minHeight && height <= maxHeight)
            return true;

        return false;
    }
}
