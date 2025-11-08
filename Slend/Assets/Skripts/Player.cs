using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Text TextCountPaper;
    [SerializeField] Text TextTakePaper;
    [SerializeField] int countPaper;

    [SerializeField] GameObject slender;
    [SerializeField] PostProcessVolume effects;
    PostProcessVolume m_Volume;
    Grain m_Grain;

    private void Start()
    {
        countPaper = GameObject.FindGameObjectsWithTag("Paper").Length;

        //Настраиваем эффекты
        m_Grain = ScriptableObject.CreateInstance<Grain>();
        m_Grain.enabled.Override(true);
        m_Grain.intensity.Override(1f);
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Grain);
    }

    private void Update()
    {
        UpdateText();
        float dist = Vector3.Distance(transform.position, slender.transform.position);
        if (dist < 0.1f)
        {
            dist = 0.1f;
        }

        m_Grain.intensity.value = dist;
        m_Grain.size.value = dist + 3;
        m_Grain.lumContrib.value = dist;
    }

    void UpdateText()
    {
        TextCountPaper.text = "Papers:" + countPaper;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            TextTakePaper.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.G))
            {
                TextTakePaper.gameObject.SetActive(false);
                Destroy(other.gameObject);
                countPaper--;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TextTakePaper.gameObject.SetActive(false);
    }
}