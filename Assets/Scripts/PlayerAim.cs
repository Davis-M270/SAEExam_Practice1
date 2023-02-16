using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] LayerMask itemLayer;
    [SerializeField] TextMeshProUGUI infoHud;
    [SerializeField] TextMeshProUGUI weight;
    [SerializeField] string infoHudHolder;
    [SerializeField] private float rayCastMaxDistance;
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private bool pickingUp;
    [SerializeField] private AudioSource inventorAdd;
    [SerializeField] PlayerController player;
    public static bool pickUpFreeze = false;
    private RaycastHit target;
    // Update is called once per frame

    private void Start()
    {
        weight.color= Color.green;
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out target, rayCastMaxDistance, itemLayer))
        {
           
            Debug.Log("I hit something");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * target.distance, Color.green);
            if (pickingUp == false)
            { 
            infoHud.text = "Pick up " + target.transform.parent.gameObject.GetComponent<ItemInfo>().itemScript.itemName + ", Press (E)";
            }
            if (Input.GetKeyDown("e"))
            {
                if (pickingUp == true)
                {
                    return;
                }
                else
                {
                    pickingUp = true;
                    pickUpFreeze = true;
                    infoHud.text = "";
                    infoHudHolder = "Picking up " + target.transform.parent.gameObject.GetComponent<ItemInfo>().itemScript.itemName;
                    StartCoroutine(pickUp());
                }
            }
        }
        else
        {
            Debug.Log("I didn't hit anything");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * target.distance, Color.red);
            infoHud.text = "";
        }
    }

    private IEnumerator pickUp()
    {
        infoHud.text = "";
        foreach (char letter in infoHudHolder)
        {

            infoHud.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(1f);
        pickingUp = false;
        inventorAdd.Play();
        player.currentWeight += target.transform.parent.gameObject.GetComponent<ItemInfo>().itemScript.weight;
        if (player.currentWeight > player.maxWeight)
        {
            weight.color= Color.red;
            weight.text = "Weight: " + player.currentWeight.ToString() + " OVER ENCUMBERED";
        }
        else
        {
            weight.color= Color.green;
            weight.text = "Weight: " + player.currentWeight.ToString();
        }
        Destroy(target.transform.parent.gameObject);
        pickUpFreeze= false;
    }
}
