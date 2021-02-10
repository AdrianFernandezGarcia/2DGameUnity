using UnityEngine;

    public class Camera2DFollow : MonoBehaviour
    {
        private Transform target;
        private float smoothMoveFactor = 0.2f;
        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;

        // Use this for initialization
        private void Start()
        {
            target = GameObject.Find("Player").transform;
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;
            Vector3 targetPos = target.position + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref m_CurrentVelocity, smoothMoveFactor);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }


