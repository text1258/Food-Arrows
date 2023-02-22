using UnityEngine;
using UnityEngine.UI;

public class Recharger : MonoBehaviour
{
    public static Recharger instance;

    [SerializeField] private GridLayoutGroup missilesPanel;
    [SerializeField] private Image imagePrefab;

    private bool missilesPanelCreated = false;
    public float SpeedUp { get; set; } = 1f;

    private void Awake()
    {
        instance = this;
    }

    public void CreateMissilesPanel()
    {
        ClearMissilesPanel();
        for (int i = 0; i < InstantiatedWeapon.instance.Weapon.MissileCount; i++)
        {
            Image currentImage = Instantiate(imagePrefab, parent: missilesPanel.transform);
            currentImage.sprite = InstantiatedWeapon.instance.Weapon.MissileSprite;
            currentImage.transform.GetChild(0).GetComponent<Image>().sprite = InstantiatedWeapon.instance.Weapon.MissileSprite;
            if (i <= InstantiatedWeapon.instance.CurrentMissileCount)
            {
                currentImage.transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
            }
            else
            {
                currentImage.transform.GetChild(0).GetComponent<Image>().fillAmount = 0f;
            }
        }
        missilesPanelCreated = true;
    }

    public void UpdateMissilesPanel()
    {
        if (missilesPanelCreated == false)
        {
            CreateMissilesPanel();
        }
        int j = 0;
        for (; j < InstantiatedWeapon.instance.CurrentMissileCount; j++)
        {
            missilesPanel.transform.GetChild(j).GetChild(0).GetComponent<Image>().fillAmount = 1f;
        }
        if (InstantiatedWeapon.instance.CurrentMissileCount < InstantiatedWeapon.instance.Weapon.MissileCount)
        {
            missilesPanel.transform.GetChild(j).GetChild(0).GetComponent<Image>().fillAmount = InstantiatedWeapon.instance.PastMissileRechargeTime / InstantiatedWeapon.instance.Weapon.MissileRechargeTime;
            for (int i = j + 1; i < missilesPanel.transform.childCount; i++)
            {
                missilesPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount = 0f;
            }
        }
    }

    private void ClearMissilesPanel()
    {
        foreach (Transform child in missilesPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}