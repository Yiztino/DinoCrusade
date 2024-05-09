using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class DinoAITest
{
    [UnityTest]
    public IEnumerator Patroling_Test()
    {
        // Creamos un GameObject de prueba para el enemigo
        GameObject enemyGO = new GameObject();
        DinoAI enemyAI = enemyGO.AddComponent<DinoAI>();

        // Creamos puntos de patrulla ficticios
        Vector3 patrolPoint1 = new Vector3(0, 0, 0);
        Vector3 patrolPoint2 = new Vector3(10, 0, 0);

        // Establecemos los puntos de patrulla en el script del enemigo
        enemyAI.walkPoint = patrolPoint1;
        enemyAI.walkPointSet = true;

        // Esperamos un tiempo suficiente para que el enemigo alcance el primer punto de patrulla
        yield return new WaitForSeconds(5);

        // Verificamos que el enemigo se esté moviendo hacia el primer punto de patrulla
        Assert.IsTrue(Vector3.Distance(enemyGO.transform.position, patrolPoint1) < 0.1f, "El enemigo no está patrullando hacia el primer punto");

        // Esperamos un tiempo suficiente para que el enemigo alcance el primer punto de patrulla
        yield return new WaitForSeconds(5);

        // Verificamos que el enemigo haya llegado al primer punto de patrulla y esté buscando el siguiente punto
        Assert.IsFalse(enemyAI.walkPointSet, "El enemigo no está buscando el siguiente punto de patrulla después de alcanzar el primero");

        // Cambiamos el siguiente punto de patrulla
        enemyAI.walkPoint = patrolPoint2;
        enemyAI.walkPointSet = true;

        // Esperamos un tiempo suficiente para que el enemigo alcance el segundo punto de patrulla
        yield return new WaitForSeconds(5);

        // Verificamos que el enemigo se esté moviendo hacia el segundo punto de patrulla
        Assert.IsTrue(Vector3.Distance(enemyGO.transform.position, patrolPoint2) < 0.1f, "El enemigo no está patrullando hacia el segundo punto");

        // Eliminamos el GameObject de prueba
        GameObject.Destroy(enemyGO);
    }
}
