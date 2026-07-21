# Property-Based Testing: Merging Sorted Lists

## The Story

`Merger.Merge` takes two already-sorted lists of integers and merges them into a single sorted
list — the same merge step at the heart of merge sort.

It has a bug. Rather than hunting for it with hand-picked examples, you will describe the
**properties** a correct merge must always have, and let [CsCheck](https://github.com/AnthonyLloyd/CsCheck)
generate hundreds of inputs trying to break them. When a property fails, CsCheck **shrinks** the
failure to the smallest counter-example it can find.

## Your task

Open `MergeSort.Tests/MergerProperties.cs`. It contains:

- A worked property, `MergedOutputIsSorted`, which already passes — note that a buggy merge can
  still produce sorted output, so this property alone is not enough.
- Two `TODO` properties for you to complete:
  - `MergePreservesLength` — the merged list should be as long as both inputs combined.
  - `MergePreservesAllElements` — the merged list should contain exactly the same elements.

Complete the two properties, run the tests, and read the shrunk minimal counter-example. Then fix
`Merger.Merge` until every property holds.

## Run the tests once

```bash
dotnet test
```

## Run the tests in watch mode

```bash
dotnet watch --project MergeSort.Tests test
```
