using System;
using System.Collections.Generic;
using UnityEngine;
using UnityConsole.Internal;
using CSharpDocumentation;

namespace UnityConsole
{
    [Summary("Utility for caching and navigating recently executed console commands.")]
    [Remarks("When initiating navigation up (after a new input entry was submitted or the console was cleared), we navigate to the last submitted input entry. \nWhen initiating navigation down (after a new input entry was submitted or the console was cleared), we navigate BELOW the last submitted input entry. \nWhen navigating up, we navigate ABOVE the last navigated-to input entry. \nWhen navigating down, we navigate BELOW the last navigated-to input entry.")]
    public class ConsoleInputHistory
    {
        // Input history from most newest to oldest
        private IndexedQueue<string> inputHistory;

        // Whether we have already initiated navigation (until the next input entry submission or console clear)
        private bool isNavigating;

        // The last submitted input entry, i.e. the one to navigate to when initiating navigation up. 
        private int lastSubmittedInputEntry;

        // The most recently navigated-to input entry.
        private int lastNavigatedToInputEntry;

        [Summary("Constructs the console input history with the given maximum capacity.")]
        public ConsoleInputHistory(int maxCapacity)
        {
            inputHistory = new IndexedQueue<string>(maxCapacity);
        }

        [Summary("Navigate (or initiate navigation) up the input history")]
        [Returns("The navigated-to input entry")]
        public string NavigateUp()
        {
            if (!isNavigating && !inputHistory.IsEmpty)
                InitiateNavigationUp();
            else
                lastNavigatedToInputEntry--;

            return validateLastNavigatedToInputEntry();
        }

        [Summary("Navigate (or initiate navigation) down the input history")]
        [Returns("The navigated-to input entry")]
        public string NavigateDown()
        {
            if (!isNavigating && !inputHistory.IsEmpty)
                InitiateNavigationDown();

            lastNavigatedToInputEntry++;

            return validateLastNavigatedToInputEntry();
        }

        private void InitiateNavigationUp()
        {
            lastNavigatedToInputEntry = lastSubmittedInputEntry;
            isNavigating = true;
        }

        private void InitiateNavigationDown()
        {
            lastNavigatedToInputEntry = lastSubmittedInputEntry;
            isNavigating = lastNavigatedToInputEntry < inputHistory.Count - 1;
        }

        private string validateLastNavigatedToInputEntry()
        {
            lastNavigatedToInputEntry = inputHistory.IsEmpty ? 0 : Mathf.Clamp(lastNavigatedToInputEntry, 0, inputHistory.Count - 1);
            if (isNavigating)
                return inputHistory[lastNavigatedToInputEntry];
            else
                return "";
        }

        [Summary("Adds a new input entry to the input history.")]
        public void AddNewInputEntry(string input)
        {
            // store whether the last navigated-to input entry is about to get obsolete for being too old
            bool lastNavigatedToInputEntryObsolete = inputHistory.IsFull && lastNavigatedToInputEntry == 0;

            // Don't add the same input entry twice in a row
            if (!inputHistory.IsEmpty && input.Equals(inputHistory[lastSubmittedInputEntry], StringComparison.OrdinalIgnoreCase))
            {
                lastSubmittedInputEntry = lastNavigatedToInputEntry;
                isNavigating = false;
                return;
            }

            // Handle the case where the input history was already at max capacity
            if (inputHistory.IsFull)
            {
                inputHistory.Dequeue();
                lastNavigatedToInputEntry--; // adjust for the fact that beginning of the queue has shifted to the left by 1 element
            }

            // Insert the new input entry
            inputHistory.Enqueue(input);
            lastSubmittedInputEntry = inputHistory.Count - 1;

            // If the last navigated-to and the new input entry are identical, then we want the last submitted input entry to be the last navigated-to input entry
            if (isNavigating && !lastNavigatedToInputEntryObsolete && input.Equals(inputHistory[lastNavigatedToInputEntry], StringComparison.OrdinalIgnoreCase))
                lastSubmittedInputEntry = lastNavigatedToInputEntry;

            // When we initiate navigating up again, we should navigate to the last submitted input entry
            isNavigating = false;
        }

        [Summary("Clears the input history and resets its navigation.")]
        public void Clear()
        {
            inputHistory.Clear();
            lastSubmittedInputEntry = 0;
            isNavigating = false;
        }
    }
}
