using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                

    private FireReleaser fireReleaser;

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        fireReleaser = GameObject.FindGameObjectWithTag("FireReleaser").GetComponent<FireReleaser>();
    }

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce;

        bool androidFiring = fireReleaser.shooting && m_PlayerNumber == 1;

        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {   
            // at max charge, not yet fired
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        } else if(Input.GetButtonDown(m_FireButton) || androidFiring) {
            // have we pressed fire for the first time?
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();

        } else if((Input.GetButton(m_FireButton) || androidFiring) && !m_Fired){
            // Holding the fire button, not yet fired
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            m_AimSlider.value = m_CurrentLaunchForce;

        } else if((Input.GetButtonUp(m_FireButton) || !androidFiring) && !m_Fired){
            // we released the button, having not fired yet
            Fire();
        }
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        m_Fired = true;

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
        
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
        
        m_CurrentLaunchForce = m_MinLaunchForce;
        Handheld.Vibrate();
    }
}