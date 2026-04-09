using UnityEngine;
using System.Collections.Generic;

public class BookshelfPuzzle : MonoBehaviour
{
    // Puzzle Settings
    public List<ShelfBook> correctOrder = new();
    private readonly List<ShelfBook> playerInput = new();

    // All Puzzle Books
    public List<ShelfBook> allBooks = new();

    // Game Manager
    public GameManager gameManager;

    // Reward
    public DoorOpener finalDoor;
    private bool solved = false;
    public void AddInput(ShelfBook book)
    {
        Debug.Log($"Puzzle got input from {book.name}");

        if (solved) return;
        if (playerInput.Contains(book)) return;

        playerInput.Add(book);

        int currentIndex = playerInput.Count - 1;

        if (currentIndex >= correctOrder.Count)
        {
            Debug.Log("Too many input. Resetting Puzzle");
            ResetPuzzle();
            return;
        }

        if (correctOrder[currentIndex] == null)
        {
            Debug.LogError($"CorrectOrder element {currentIndex} is null");
            return;
        }

        Debug.Log($"Checking index {currentIndex}: pressed {book.name}, expected {correctOrder[currentIndex].name}");

        if (playerInput[currentIndex] != correctOrder[currentIndex])
        {
            Debug.Log($"Wrong Order. Resetting Puzzle");
            ResetPuzzle();
            return;
        }

        if (playerInput.Count == correctOrder.Count)
        {
            SolvePuzzle();
        }
    }

    public void SolvePuzzle()
    {
        solved = true;
        Debug.Log("Bookshelf Puzzle Complete!");

        foreach (ShelfBook book in playerInput)
        {
            book.ChangeState(BookStates.DONE);
        }

        Debug.Log("Bookshelf puzzle complete!");

        if (gameManager != null)
        {
            gameManager.RegisterPuzzleComplete();
        }

        if (finalDoor != null)
        {
            finalDoor.OpenDoor();
        }
    }

    public void ResetPuzzle()
    {
        if (solved) return;

        Debug.Log("Resetting Puzzle called");

        playerInput.Clear();

        foreach (ShelfBook book in allBooks)
        {
            if (book != null && book.State == BookStates.PRESSED)
            {
                book.ChangeState(BookStates.IDLE);
            }
        }
    }
    
}
