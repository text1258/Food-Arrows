using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingFoodAnimation : MonoBehaviour
{
    public static CookingFoodAnimation instance;

    [SerializeField] private Vector3 cookingPosition;
    [SerializeField] private float productsGatheringAnimationTime;
    [SerializeField] private float foodGoToPlayerAnimationTime;
    [SerializeField] private ParticleSystem onCookingParticleSystem;
    [SerializeField] private Vector3 maxSpawnProductsPosition;
    [SerializeField] private Vector3 minSpawnProductsPosition;
    
    private void OnDrawGizmosSelected()
    {;
        Gizmos.color = new Color(255, 0, 0, 0.5f);
        Gizmos.DrawSphere(cookingPosition, 0.15f);
        Gizmos.color = new Color(0, 0, 255, 0.5f);
        Gizmos.DrawCube((maxSpawnProductsPosition + minSpawnProductsPosition) / 2, maxSpawnProductsPosition - minSpawnProductsPosition);
    }

    private void Awake()
    {
        instance = this;
    }

    public void StartCookAnimation(Food cookingFood)
    {
        StartCoroutine(CookingAnimation(cookingFood));
    }

    private IEnumerator CookingAnimation(Food cookingFood)
    {
        List <GameObject> productsFromRecipe = new List<GameObject>();
        foreach (Product currentProduct in cookingFood.CookingProducts)
        {
            GameObject currentInstantiatedProduct = Instantiate(currentProduct.ItemPrefab,
                RandomVector(maxSpawnProductsPosition, minSpawnProductsPosition), currentProduct.ItemPrefab.transform.rotation);
            productsFromRecipe.Add(currentInstantiatedProduct);
            if (currentInstantiatedProduct.GetComponent<InstantiatedProduct>() == null)
            {
                currentInstantiatedProduct.AddComponent<InstantiatedProduct>();
            }
        }
        float traveledPath = 0f;
        while (Vector3.Distance(productsFromRecipe[0].transform.position, cookingPosition) > 0.01f)
        {
            traveledPath += Time.deltaTime / productsGatheringAnimationTime;
            foreach (var currentGameObject in productsFromRecipe)
            {
                currentGameObject.transform.position = Vector3.Slerp(currentGameObject.GetComponent<InstantiatedProduct>().startPosition, cookingPosition, traveledPath);
            }
            yield return null;
        }
        traveledPath = 0f;
        if (GameObject.Find(onCookingParticleSystem.name) == null)
        {
            onCookingParticleSystem = Instantiate(onCookingParticleSystem);
        }
        onCookingParticleSystem.transform.position = cookingPosition;
        onCookingParticleSystem.Play();
        productsFromRecipe.ForEach(Destroy);
        GameObject instantiatedFood = Instantiate(cookingFood.ItemPrefab, cookingPosition, Quaternion.identity); 
        Vector3 startPosition = instantiatedFood.transform.position;
        while (Vector3.Distance(instantiatedFood.transform.position, Player.instance.transform.position) > 0.01f)
        {
            traveledPath += Time.deltaTime / foodGoToPlayerAnimationTime;
            instantiatedFood.transform.position = Vector3.Slerp(startPosition, Player.instance.transform.position, traveledPath); 
            yield return null;
        }
        Destroy(instantiatedFood.gameObject);
        yield break;
    }
    
    private static Vector3 RandomVector(Vector3 a, Vector3 b)
    {
        return new Vector3(Random.Range(a.x, b.x), Random.Range(a.y, b.y), Random.Range(a.z, b.z));
    }
}