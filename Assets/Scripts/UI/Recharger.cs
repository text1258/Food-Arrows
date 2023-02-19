using UnityEngine;
using UnityEngine.UI;

public class Recharger : MonoBehaviour
{
    public static Recharger Instance;

    [SerializeField] private GridLayoutGroup missilesPanel;
    [SerializeField] private Image imagePrefab;

    private bool missilesPanelCreated = false;
    public float SpeedUp { get; set; } = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateMissilesPanel()
    {
        ClearMissilesPanel();
        for (int i = 0; i < InstantiatedWeapon.Instance.Weapon.MissileCount; i++)
        {
            Image currentImage = Instantiate(imagePrefab, parent: missilesPanel.transform);
            currentImage.sprite = InstantiatedWeapon.Instance.Weapon.MissileSprite;
            currentImage.transform.GetChild(0).GetComponent<Image>().sprite = InstantiatedWeapon.Instance.Weapon.MissileSprite;
            if (i <= InstantiatedWeapon.Instance.CurrentMissileCount)
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
        for (; j < InstantiatedWeapon.Instance.CurrentMissileCount; j++)
        {
            missilesPanel.transform.GetChild(j).GetChild(0).GetComponent<Image>().fillAmount = 1f;
        }
        if (InstantiatedWeapon.Instance.CurrentMissileCount < InstantiatedWeapon.Instance.Weapon.MissileCount)
        {
            missilesPanel.transform.GetChild(j).GetChild(0).GetComponent<Image>().fillAmount = InstantiatedWeapon.Instance.PastMissileRechargeTime / InstantiatedWeapon.Instance.Weapon.MissileRechargeTime;
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