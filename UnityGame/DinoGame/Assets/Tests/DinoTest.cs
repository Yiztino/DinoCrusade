using NUnit.Framework;
using UnityEngine;
using TMPro;

public class DinosaurCounterTest
{
    [Test]
    public void UpdateObjectCount_CountsObjectsWithTag()
    {
        // Arrange
        GameObject testObject = new GameObject();
        DinosaurCounter dinosaurCounter = testObject.AddComponent<DinosaurCounter>();

        // Act
        dinosaurCounter.UpdateObjectCount();

        // Assert
        Assert.AreEqual(0, dinosaurCounter.objectCount);
    }

    [Test]
    public void AreAllObjectsDestroyed_ReturnsTrueWhenNoObjects()
    {
        // Arrange
        GameObject testObject = new GameObject();
        DinosaurCounter dinosaurCounter = testObject.AddComponent<DinosaurCounter>();

        // Act
        bool result = dinosaurCounter.AreAllObjectsDestroyed();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void AreAllObjectsDestroyed_ReturnsFalseWhenObjectsExist()
    {
        // Arrange
        GameObject testObject = new GameObject();
        DinosaurCounter dinosaurCounter = testObject.AddComponent<DinosaurCounter>();
        dinosaurCounter.objectCount = 1; // Assuming there's one object initially

        // Act
        bool result = dinosaurCounter.AreAllObjectsDestroyed();

        // Assert
        Assert.IsFalse(result);
    }
}
