# Decompositions Playground

We want to decompose a number is a way to represent it as a sum of smaller "standard" numbers. Let imagine we have a number $n$ where $n$ ∈ (0, 10] and we want to decompose it in a way that the sum of the decompositions is equal to $n$.

Also, only the following "standard" numbers are allowed: `1, 2, 3, 4, 5, 6, 8, 9, 10`. All these numbers can be expressed as a sum of smaller numbers. Lets enumerate them all:

	 1 = { {  1 } }
	 2 = { {  2 }, { 1, 1 } }
	 3 = { {  3 }, { 2, 1 }, { 1, 1, 1 } }
	 4 = { {  4 }, { 3, 1 }, { 2, 2 }, { 2, 1, 1 }, { 1, 1, 1, 1 } }
	 5 = { {  5 }, { 4, 1 }, { 3, 2 }, { 3, 1, 1 }, { 2, 2, 1 }, { 2, 1, 1, 1 }, { 1, 1, 1, 1, 1 } }
	 6 = { {  6 }, { 5, 1 }, { 4, 2 }, { 4, 1, 1 }, { 3, 3 }, { 3, 2, 1 }, ....  }
	 8 = { {  8 }, { 6, 2 }, { 6, 1, 1 }, { 5, 2, 1 }, { 5, 1, 1, 1 }, .... }
	 9 = { {  9 }, { 8, 1 }, { 6, 3 }, { 6, 2, 1 }, .... }
	10 = { { 10 }, { 9, 1 }, { 8, 2 }, { 8, 1, 1 }, .... }

Just by looking at those trivial decompositions we can see that any number can be decomposed recursively in the sum of **two** smaller number. For example, the number `3` has only two smaller number `2` and `1` that it can be decomposed on (`3 = 2 + 1`). But at the same time `2` can also be decomposed has `1 + 1`.

![Graphical view](graph.png?raw=true "Tree of decompositions")

Each node of this tree contains a value and a list of the possible alternatives, which are represented as a typical binary tree node with a left and right branch.

## Generation without repetition

Once we have the decomposition tree we can implement an algorithm that walks the tree in a way that generates the decompositions without duplications. By a duplication we mean `{2, 1}` and `{1, 2}` contains the exact same elements and only vary in the order of them so, they are equivalent and shouldn't repeat.

```
   PrintTree(tree: DecompositionTree, level: int):
     indent = level * ' '
     print (tree.Value)
     foreach alt in tree.Alternatives:
       print (indent + alt.Left.Value + " + ")
       visit (alt.Right, level + 1)
```

And the expected outputs is:

```
9
    8 + 1
    7 + 2
        1 + 1
    6 + 3
        2 + 1
    5 + 4
        3 + 1
        2 + 2
            1 + 1
```

```
{ 9 }
{ 8, 1 }
{ 6, 3 }
{ 6, 2, 1 }

{ 5, 4 }
{ 5, 3, 1 }
{ 5, 2, 2 }
{ 5, 2, 1, 1 }

{ 4, 4, 1 }
{ 4, 3, 2 }
{ 3, 3, 3 }
{ 6, 1, 1, 1 }
{ 4, 3, 1, 1 }
{ 4, 2, 2, 1 }
{ 3, 3, 2, 1 }
{ 3, 2, 2, 2 }
```