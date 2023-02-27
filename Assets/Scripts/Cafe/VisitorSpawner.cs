using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VisitorSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private List<Visitor> visitors;
    [SerializeField] private GiveOrderButton giveOrderButton;
    [SerializeField] private AllLevels allLevels;

    private Visitor currentVisitor;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spawnPosition, 0.1f);
    }

    private IEnumerator Start()
    {
        if (string.IsNullOrEmpty(Player.instance.CurrentVisitorIndex) == false)
        {
            currentVisitor = Instantiate(visitors[Convert.ToInt32(Player.instance.CurrentVisitorIndex)]);
        }
        else
        {
            int currentVisitorIndex;
            currentVisitorIndex = Random.Range(0, visitors.Count);
            Player.instance.CurrentVisitorIndex = currentVisitorIndex.ToString();
            currentVisitor = Instantiate(visitors[currentVisitorIndex]);
        }
        if (Player.instance.CurrentOrder != null)
        {
            currentVisitor.order = Player.instance.CurrentOrder;
        }
        while (true)
        {
            if (currentVisitor == null)
            {
                int currentVisitorIndex;
                currentVisitorIndex = Random.Range(0, visitors.Count);
                Player.instance.CurrentVisitorIndex = currentVisitorIndex.ToString();
                currentVisitor = Instantiate(visitors[currentVisitorIndex]);
            }
            if (currentVisitor.order == null)
            {
                currentVisitor.order = RandomAvailableFood();
                Player.instance.CurrentOrder = currentVisitor.order;
            }
            currentVisitor.transform.position = new Vector3(spawnPosition.x, spawnPosition.y + (currentVisitor.GetComponent<MeshRenderer>().bounds.size.y / 2), spawnPosition.z);
            //Move visitor to center
            currentVisitor.StartMove();
            while (currentVisitor.transform.position.x >= 0)
            {
                currentVisitor.transform.Translate(Vector3.left * currentVisitor.Speed * Time.deltaTime);
                currentVisitor.StartMove();
                yield return null;
            }
            //
            currentVisitor.Stand();
            giveOrderButton.CurrentVisitor = currentVisitor;
            giveOrderButton.gameObject.SetActive(true);
            yield return new WaitUntil(() => currentVisitor.isSatisfied);
            //Move visitor behind the screen
            currentVisitor.StartMove();
            while (currentVisitor.transform.position.x >= -spawnPosition.x)
            {
                currentVisitor.transform.Translate(Vector3.left * currentVisitor.Speed * Time.deltaTime);
                yield return null;
            }
            //
            Destroy(currentVisitor.gameObject);
            currentVisitor = null;
        }
    }

    private Food RandomAvailableFood()
    {
        List<Food> availableFood = new List<Food>();
        for (int i = 0; i <= Player.instance.CurrentLevel.Number - 1; i++)
        {
            foreach (Food food in allLevels.Levels[i].OpenInThisLevelFoods)
            {
                availableFood.Add(food);
            }
        }
        return availableFood[Random.Range(0, availableFood.Count)];
    }
}