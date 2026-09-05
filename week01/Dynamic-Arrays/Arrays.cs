using System;
using System.Collections.Generic;

public static class Arrays
{
	// Plan for MultiplesOf:
	// 1. Validate inputs: ensure `length` is not negative. If it is, throw an ArgumentOutOfRangeException because
	//    a negative length doesn't make sense for an array size.
	// 2. Allocate an integer array of size `length` to hold the result.
	// 3. Fill the array with the first `length` multiples of `number`.
	//    - The 1st multiple is `number * 1` (index 0), the 2nd is `number * 2` (index 1), and so on.
	//    - For loop: for i from 0 to length-1, set result[i] = number * (i + 1).
	// 4. Return the filled array. If `length` is 0, return an empty array.
	public static int[] MultiplesOf(int number, int length)
	{
		if (length < 0)
			throw new ArgumentOutOfRangeException(nameof(length), "length must be non-negative");

		int[] result = new int[length];
		for (int i = 0; i < length; i++)
		{
			result[i] = number * (i + 1);
		}

		return result;
	}

	public static List<int> RotateListRight(List<int> list, int amount)
	{
		// Detailed step-by-step plan (rotate in-place and return the same list reference):
		// 1. Validate inputs:
		//    - If `list` is null, throw ArgumentNullException.
		//    - If list is empty (Count == 0), there is nothing to rotate; return the list as-is.
		// 2. Normalize `amount` to an equivalent shift within the list length `n`:
		//    - Compute `k = amount % n` to reduce large shifts.
		//    - If `k` is negative (because `amount` may be negative), convert it to a positive shift
		//      by adding `n` and then taking modulo again: `k = (k + n) % n`.
		// 3. If `k == 0`, the list is unchanged; return the original list reference.
		// 4. Build a temporary list (`rotated`) of size `n` containing the rotated ordering:
		//    - The rotated list should take the last `k` elements of the original list and place
		//      them at the beginning, followed by the first `n - k` elements.
		//    - Example: list {1,2,3,4,5,6,7,8,9}, n=9, amount=5 -> k=5, split=4
		//      last k elements = indices 4..8 -> {5,6,7,8,9}
		//      first n-k elements = indices 0..3 -> {1,2,3,4}
		//      rotated = {5,6,7,8,9,1,2,3,4}
		// 5. Copy the rotated contents back into the original `list` in index order so the
		//    // original reference is modified in-place. Do not create a new reference for the
		//    // calling code (some assignments/tests may expect the same list object to be changed).
		// 6. Return the original list reference (now modified) to satisfy the method signature.

		if (list == null)
			throw new ArgumentNullException(nameof(list));

		int n = list.Count;

		// If the list is empty, nothing to do
		if (n == 0)
			return list;

		// Normalize amount to range [0, n)
		int k = amount % n;
		if (k < 0)
			k = (k + n) % n;

		// If k == 0, no rotation needed
		if (k == 0)
			return list;

		int split = n - k; // index where the tail of length k starts

		var rotated = new List<int>(n);

		// Add the last 'k' elements first (they become the new head)
		for (int i = split; i < n; i++)
			rotated.Add(list[i]);

		// Then add the first 'n - k' elements
		for (int i = 0; i < split; i++)
			rotated.Add(list[i]);

		// Copy rotated values back into the original list to modify it in-place
		for (int i = 0; i < n; i++)
			list[i] = rotated[i];

		return list;
	}
}

