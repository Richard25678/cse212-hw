using System;
using System.Collections.Generic;

public static class Arrays
{
	// Plan for MultiplesOf:
	// 1. Validate inputs:
	//    - If `length` is negative, throw `ArgumentOutOfRangeException` because array length cannot be negative.
	//    - If `length` is zero, return an empty `double[]` immediately.
	// 2. Allocate a `double[]` of size `length` to hold the result.
	// 3. Populate the array with multiples of `number`:
	//    - The element at index 0 should be `number * 1` (the first multiple).
	//    - For each index `i` from 0 to `length-1`, set `result[i] = number * (i + 1)`.
	// 4. Return the populated array containing exactly `length` elements.
	public static double[] MultiplesOf(double number, int length)
	{
		if (length < 0)
			throw new ArgumentOutOfRangeException(nameof(length), "length must be non-negative");

		if (length == 0)
			return new double[0];

		double[] result = new double[length];
		for (int i = 0; i < length; i++)
		{
			result[i] = number * (i + 1);
		}

		return result;
	}

	// Plan for RotateListRight (in-place):
	// 1. Validate inputs:
	//    - If `data` is null, throw `ArgumentNullException`.
	//    - If `data.Count` is 0 or `amount` is 0 (or a multiple of `data.Count`), nothing to do.
	// 2. Normalize `amount` to `k = amount % n` where `n = data.Count` so very large amounts
	//    or amounts equal to `n` are reduced to the minimal equivalent shift.
	// 3. Build a temporary array of length `n` containing the rotated ordering:
	//    - Copy the last `k` elements of `data` into the first `k` positions of `rotated`.
	//    - Then copy the first `n - k` elements of `data` into the remaining positions.
	//    - Example: data {1,2,3,4,5,6,7,8,9}, amount=3 -> k=3 -> rotated {7,8,9,1,2,3,4,5,6}
	// 4. Copy values from the temporary `rotated` array back into `data` by assigning each index,
	//    modifying the original list in-place (do not replace the `data` reference).
	// 5. Return void (method modifies `data` directly).
	public static void RotateListRight(List<int> data, int amount)
	{

		if (data == null)
			throw new ArgumentNullException(nameof(data));

		int n = data.Count;
		if (n == 0)
			return;

		// Normalize amount to range [0, n)
		int k = amount % n;
		if (k < 0)
			k = (k + n) % n;
		if (k == 0)
			return;

		int split = n - k;

		int[] rotated = new int[n];

		// Copy last k elements into front of rotated
		for (int i = 0; i < k; i++)
		{
			rotated[i] = data[split + i];
		}

		// Copy first n-k elements after that
		for (int i = 0; i < split; i++)
		{
			rotated[k + i] = data[i];
		}

		// Write back into original list to modify in-place
		for (int i = 0; i < n; i++)
			data[i] = rotated[i];
	}
}

