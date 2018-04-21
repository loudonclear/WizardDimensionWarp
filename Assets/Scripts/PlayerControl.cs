using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
    public static bool gameOver = false;

    public float forwardSpeed = 10;
	public float jumpHeight = 20;
    public Text scoreText;
    public Text gameOverText;

    [SerializeField] float m_GroundCheckDistance = 0.1f;

    private Vector3 m_GroundNormal;

    private bool m_jumpPress;
	private bool m_IsGrounded = true;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameOver = false;
    }

    void Update () {
        CheckGroundStatus();

        float h = CrossPlatformInputManager.GetAxis("Jump");
        if (h != 0)
        {
            m_jumpPress = true;
            if (m_IsGrounded)
            {
                m_IsGrounded = false;
                Vector3 vel = rb.velocity;
                vel.y = jumpHeight;
                GetComponent<Rigidbody>().velocity = vel;
            }
        } else
        {
            m_jumpPress = false;
        }
		
	}

    private void FixedUpdate()
    {
        if (!gameOver) { 
            scoreText.text = (Mathf.Round(10 * transform.position.z)).ToString();

            Vector3 vel = rb.velocity;
            vel.z = forwardSpeed;
            GetComponent<Rigidbody>().velocity = vel;

            if (transform.position.y <= -10)
            {
                gameOver = true;
                gameOverText.gameObject.SetActive(true);
            }
        } else
        {
            if (m_jumpPress)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
	}

	void OnCollisionEnter (Collision col)
	{
        if (col.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            gameOverText.gameObject.SetActive(true);
        }
	}

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
        }
    }
}
