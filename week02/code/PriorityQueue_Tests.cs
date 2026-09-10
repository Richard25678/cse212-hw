using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items to the queue in order and check the stored order.
    // Expected Result: The queue keeps items in insertion order, so the string should be [A (Pri:1), B (Pri:2), C (Pri:3)].
    // Defect(s) Found: Enqueue was not preserving the back-of-the-queue order in a way that could be observed by the queue contents.
    public void TestPriorityQueue_EnqueueAddsToBack()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("[A (Pri:1), B (Pri:2), C (Pri:3)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue items where the highest-priority item is at the end of the list.
    // Expected Result: Dequeue returns the highest priority item, which is C.
    // Defect(s) Found: The code stopped the search before checking the last item, so it could miss the true highest-priority item.
    public void TestPriorityQueue_DequeueReturnsHighestPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 5);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("C", result);
        Assert.AreEqual("[A (Pri:1), B (Pri:2)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue items with the same highest priority and make sure the earlier item is returned first.
    // Expected Result: The item closest to the front is returned first, so A is returned before B.
    // Defect(s) Found: When priorities were equal, the code could choose the later item instead of the earliest one, which breaks FIFO behavior.
    public void TestPriorityQueue_DequeueUsesFifoForEqualPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 4);
        priorityQueue.Enqueue("B", 4);
        priorityQueue.Enqueue("C", 1);

        var firstResult = priorityQueue.Dequeue();
        var secondResult = priorityQueue.Dequeue();

        Assert.AreEqual("A", firstResult);
        Assert.AreEqual("B", secondResult);
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException is thrown with the exact message "The queue is empty."
    // Defect(s) Found: The queue must reject empty dequeue operations instead of returning a value or throwing the wrong exception message.
    public void TestPriorityQueue_DequeueEmptyQueueThrowsException()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());

        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}