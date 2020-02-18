using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private int selectedWeaponIndex = 0;


    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousSelectedWeaponIndex = selectedWeaponIndex;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeaponIndex >= transform.childCount - 1)
            {
                selectedWeaponIndex = 0;
            }
            else
            {
                selectedWeaponIndex++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeaponIndex <= 0)
            {
                selectedWeaponIndex = transform.childCount - 1;
            }
            else
            {
                selectedWeaponIndex--;
            }
        }

        if (previousSelectedWeaponIndex != selectedWeaponIndex)
        {
            SelectWeapon();
        }
    }

    private void SelectWeapon()
    {
        for (int i = 0, weaponCount = transform.childCount; i < weaponCount; i++)
        {
            Transform weapon = transform.GetChild(i);
            if (i == selectedWeaponIndex)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}
