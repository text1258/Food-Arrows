using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingFoodAnimation : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Vector3 cookingPosition;
    [SerializeField] private float productsGatheringAnimationTime;
    [SerializeField] private float foodGoToPlayerAnimationTime;
    [SerializeField] private ParticleSystem onCookingParticleSystem;
    [SerializeField] private Vector3 maxSpawnProductsPosition;
    [SerializeField] private Vector3 minSpawnProductsPosition;
    [HideInInspector] public Food cookingFood;
    
    private void OnDrawGizmos()
    {;
        Gizmos.color = new Color(255, 0, 0, 0.5f);
        Gizmos.DrawSphere(cookingPosition, 0.15f);
        Gizmos.color = new Color(0, 0, 255, 0.5f);
        Gizmos.DrawCube((maxSpawnProductsPosition + minSpawnProductsPosition) / 2, maxSpawnProductsPosition - minSpawnProductsPosition);
    }

    public IEnumerator CookingAnimate()
    {
        List <GameObject> cookingProducts = new List<GameObject>();
        foreach (Product currentProduct in cookingFood.CookingProducts)
        {
            GameObject currentInstantiatedProduct = Instantiate(currentProduct.ItemPrefab,
                RandomVector(maxSpawnProductsPosition, minSpawnProductsPosition), Quaternion.identity);
            cookingProducts.Add(currentInstantiatedProduct);
            if (currentInstantiatedProduct.GetComponent<InstantiatedProduct>() == null)
            {
                currentInstantiatedProduct.AddComponent<InstantiatedProduct>();
            }
        }
        float traveledPath = 0f;
        while (Vector3.Distance(cookingProducts[0].transform.position, cookingPosition) > 0.01f)
        {
            traveledPath += Time.deltaTime / productsGatheringAnimationTime;
            foreach (var currentGameObject in cookingProducts)
            {
                currentGameObject.transform.position = Vector3.Slerp(currentGameObject.GetComponent<InstantiatedProduct>().startPosition, cookingPosition, traveledPath);
            }
            yield return null;
        }
        traveledPath = 0f;
        onCookingParticleSystem.transform.position = cookingPosition;
        onCookingParticleSystem.Play();
        cookingProducts.ForEach(Destroy);
        GameObject instantiatedFood = Instantiate(cookingFood.ItemPrefab, cookingPosition, Quaternion.identity); 
        Vector3 startPosition = instantiatedFood.transform.position;
        while (Vector3.Distance(instantiatedFood.transform.position, player.transform.position) > 0.01f)
        {
            traveledPath += Time.deltaTime / foodGoToPlayerAnimationTime;
            instantiatedFood.transform.position = Vector3.Slerp(startPosition, player.transform.position, traveledPath); 
            yield return null;
        }
        Destroy(instantiatedFood);
        yield break;
    }
    
    private static Vector3 RandomVector(Vector3 a, Vector3 b)
    {
        return new Vector3(Random.Range(a.x, b.x), Random.Range(a.y, b.y), Random.Range(a.z, b.z));
    }
}