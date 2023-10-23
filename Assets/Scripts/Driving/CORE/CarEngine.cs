using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Septim.Driving
{
    public class CarEngine : MonoBehaviour
    {
        [Header("[CORE]")]
        [SerializeField]
        private List<Transform> waypoints;
        /*
        [SerializeField]
        private Transform rootWaypoint;
        */
        private int currentWaypoint = 0;
        //private int currentWaypointTempCount = 0;

        [Space]
        [Header("Perameters")]
        [SerializeField]
        private float maxSteerAngle = 45f;
        [SerializeField]
        private float maxSpeedKmPerH = 50f;
        [SerializeField]
        private float torsion = 50f;

        private Rigidbody rigidBody;
        private float curSpeedKmPerH;

        [Space]
        [Header("Wheels")]
        [SerializeField]
        private WheelCollider wheelFL;
        [SerializeField]
        private WheelCollider wheelFR;

        [Space]
        [Header("Collider checker")]
        [SerializeField]
        private Collider colliderOpen;
        [SerializeField]
        private Collider colliderClose;


        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();

        }

        private void FixedUpdate()
        {
            ApplySteer();
            Drive();
            CheckWaypointDistance();
            curSpeedKmPerH = GetSpeedKmPerHour();
        }

        private void ApplySteer()
        {
            Vector3 relativeVector = transform.InverseTransformPoint(waypoints[currentWaypoint].position);
            float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
            wheelFL.steerAngle = newSteer;
            wheelFR.steerAngle = newSteer;
        }

        private void Drive()
        {
            if(curSpeedKmPerH <= maxSpeedKmPerH)
            {
                wheelFL.motorTorque = torsion;
                wheelFR.motorTorque = torsion;
            }
            else
            {
                wheelFL.motorTorque = 0;
                wheelFR.motorTorque = 0;
            }
        }

        private void CheckWaypointDistance()
        {
            if(Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 10f)
            {
                currentWaypoint++;
                if(currentWaypoint >= waypoints.Count)
                {
                    currentWaypoint = 0;
                }

            }
        }

        public float GetSpeedKmPerHour()
        {
            return rigidBody.velocity.magnitude * 3.6f;
        }
    }
}

