using System;
using System.Collections.Generic;

public static class Arrays
{
	public static List<int> RotateListRight(List<int> list, int amount)
	{
		// Step-by-step plan:
		// 1. Validate input (null or empty) and return a copy as appropriate.
		// 2. Normalize amount to be within [0, n) using modulo.
		// 3. If normalized amount is 0, return a shallow copy.
		// 4. Compute split index = n - amount.
		// 5. Build new list by taking last 'amount' elements and then the first 'n-amount' elements.
		// 6. Return the new rotated list.

		if (list == null)
			throw new ArgumentNullException(nameof(list));

		int n = list.Count;

		// If list is empty or amount is 0, return a shallow copy
		if (n == 0)
			return new List<int>(list);

		// Normalize amount to positive range [0, n)
		int k = amount % n;
		if (k < 0)
			k = (k + n) % n;

		if (k == 0)
			return new List<int>(list);

		int split = n - k; // index where the tail of length k starts

		var result = new List<int>(n);

		// Add the last 'k' elements first (they become the new head)
		for (int i = split; i < n; i++)
			result.Add(list[i]);

		// Then add the first 'n - k' elements
		for (int i = 0; i < split; i++)
			result.Add(list[i]);

		return result;
	}
}

