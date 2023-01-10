using UnityEngine;
using UnityEngine.UI;

public class RechargeViewer : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup missilesPanel;
    [SerializeField] private Image imagePrefab;
    [SerializeField] private InstantiatedWeapon currentInstantiatedWeapon;

    public InstantiatedWeapon CurrentInstantiatedWeapon
    {
        get => currentInstantiatedWeapon;
        set
        {
            currentInstantiatedWeapon = value;
        }
    }

    public void CreateMissilesPanel()
    {
        ClearMissilesPanel();
        for (int i = 0; i < CurrentInstantiatedWeapon.Weapon.MissileCount; i++)
        {
            Image currentImage = Instantiate(imagePrefab, parent: missilesPanel.transform);
            currentImage.sprite = CurrentInstantiatedWeapon.MissileSprite;
            currentImage.GetComponentInChildren<Image>().sprite = CurrentInstantiatedWeapon.MissileSprite;
            if (i <= CurrentInstantiatedWeapon.CurrentMissileCount)
            {
                currentImage.transform.GetChild(0).GetComponent<Image>().fillAmount = 1f;
            }
            else
            {
                currentImage.transform.GetChild(0).GetComponent<Image>().fillAmount = 0f;
            }
        }
    }
    
    public void UpdateMissilesPanel()
    {
        int j = 0;
        for (; j < CurrentInstantiatedWeapon.CurrentMissileCount; j++)
        {
            missilesPanel.transform.GetChild(j).GetChild(0).GetComponent<Image>().fillAmount = 1f;
        }
        if (CurrentInstantiatedWeapon.CurrentMissileCount < CurrentInstantiatedWeapon.Weapon.MissileCount)
        {
            missilesPanel.transform.GetChild(j).GetChild(0).GetComponent<Image>().fillAmount = CurrentInstantiatedWeapon.PastMissileRechargeTime / CurrentInstantiatedWeapon.MissileRechargeTime;
            for (int i = j + 1; i < missilesPanel.transform.childCount; i++)
            {
                missilesPanel.transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount = 0f;
            }
        }
    }

    private void ClearMissilesPanel()
    {
        for (int i = 0; i < missilesPanel.transform.childCount; i++)
        {
            Destroy(missilesPanel.transform.GetChild(i).gameObject);
        }
    }
}