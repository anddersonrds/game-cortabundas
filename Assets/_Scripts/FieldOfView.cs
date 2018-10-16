using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    public bool ItsInFoV = false;

    [HideInInspector]
    public List<Transform> VisibleTargets = new List<Transform>();

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    IEnumerator FindeTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    private void Start()
    {
        StartCoroutine("FindeTargetWithDelay", .2f);
    }

    void FindVisibleTarget()
    {
        VisibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward,dirToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    ItsInFoV = true;
                    VisibleTargets.Add(target);                    
                }                
            }
            else
            {
                ItsInFoV = false;
            }
        }       
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }	
	
}
