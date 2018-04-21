using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RotateScript : MonoBehaviour
{
    public float rotateSpeed = 700.5f;
    public float coolDown = 0.2f;
    public Transform player;

    private const int numPlatforms = 3;
    private const float rotAngle = 360f / numPlatforms;

    private float timer = 0;
    private bool m_turn;
    private Quaternion m_goalRot;
    private Rigidbody prb;

    private void Start()
    {
        prb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (PlayerControl.gameOver) return;

        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        if (!m_turn && h != 0 && timer > coolDown)
        {
            m_turn = true;
            timer = 0;
            m_goalRot = Quaternion.Euler(0, 0, -rotAngle * Mathf.Sign(h) + transform.rotation.eulerAngles.z);

            Vector3 vel = prb.velocity;
            if (vel.y < 0)
            {
                vel.y = 0;
                prb.velocity = vel;
            }
        }
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (PlayerControl.gameOver) return;

        if (m_turn)
        {
            if (Quaternion.Angle(transform.rotation, m_goalRot) <= 0.1f)
            {
                transform.rotation = m_goalRot;
                m_turn = false;
                player.position = new Vector3(0, player.position.y, player.position.z);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, m_goalRot, Time.deltaTime * rotateSpeed);
                player.position = Vector3.Slerp(player.position, new Vector3(0, player.position.y, player.position.z), Time.deltaTime * rotateSpeed);
            }
        }
    }
}
