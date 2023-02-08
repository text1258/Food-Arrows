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
    [HideInInspector] private Visitor currentVisitor;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(spawnPosition, 0.1f);
    }

    private IEnumerator Start()
    {
        if (Player.Instance.CurrentVisitorIndex != "")
        {
            currentVisitor = Instantiate(visitors[Convert.ToInt32(Player.Instance.CurrentVisitorIndex)]);
        }
        else
        {
            int currentVisitorIndex = Random.Range(0, visitors.Count);
            Player.Instance.CurrentVisitorIndex = currentVisitorIndex.ToString();
            currentVisitor = Instantiate(visitors[currentVisitorIndex]);
        }
        if (Player.Instance.CurrentOrder != null)
        {
            currentVisitor.order = Player.Instance.CurrentOrder;
        }
        while (true)
        {
            if (currentVisitor == null)
            {
                int currentVisitorIndex = Random.Range(0, visitors.Count);
                Player.Instance.CurrentVisitorIndex = currentVisitorIndex.ToString();
                currentVisitor = Instantiate(visitors[currentVisitorIndex]);
            }
            if (currentVisitor.order == null)
            {
                currentVisitor.order = Player.Instance.AvailableFood[Random.Range(0, Player.Instance.AvailableFood.Count)];
                Player.Instance.CurrentOrder = currentVisitor.order;
            }
            currentVisitor.transform.position = new Vector3(spawnPosition.x, currentVisitor.GetComponent<MeshRenderer>().bounds.size.y / 2, spawnPosition.z);
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
}