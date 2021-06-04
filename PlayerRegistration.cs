// C# program for insertion in AVL Tree

/* The program will register players and their ages and will store each information using AVL-Tree algorithm 
 * and then print each of the player’s name and age. 
 * The user will enter name and age of each team player one after the other. 
 * Once the user enters “quit” for name and “0” for age, the program will end 
 * and print all the record stored to the console.*/

using System;

class Node
{
	public int key, height;
	public String name;
	public Node left, right;

	public Node(int d, String s)
	{
		key = d;
		height = 1;
		name = s;
	}
}

public class AVLTree
{
	Node root;

	// height of the tree
	int Height(Node N)
	{
		if (N == null)
			return 0;

		return N.height;
	}

	// A utility function to get the maximum of two integers
	int Max(int a, int b)
	{
		return (a > b) ? a : b;
	}

	// A utility function to right
	// rotate subtree rooted with y
	// See the diagram given above.
	Node RightRotate(Node y)
	{
		Node x = y.left;
		Node T2 = x.right;

		// Perform rotation
		x.right = y;
		y.left = T2;

		// Update heights
		y.height = Max(Height(y.left),
					Height(y.right)) + 1;
		x.height = Max(Height(x.left),
					Height(x.right)) + 1;

		// Return new root
		return x;
	}

	// A utility function to left
	// rotate subtree rooted with x
	// See the diagram given above.
	Node LeftRotate(Node x)
	{
		Node y = x.right;
		Node T2 = y.left;

		// Perform rotation
		y.left = x;
		x.right = T2;

		// Update heights
		x.height = Max(Height(x.left),
					Height(x.right)) + 1;
		y.height = Max(Height(y.left),
					Height(y.right)) + 1;

		// Return new root
		return y;
	}

	// Get Balance factor of node N
	int GetBalance(Node N)
	{
		if (N == null)
			return 0;

		return Height(N.left) - Height(N.right);
	}

	Node Insert(Node node, int key, String name)
	{

		/* 1. Perform the normal BST insertion */
		if (node == null)
			return (new Node(key, name));

		if (key < node.key)
			node.left = Insert(node.left, key,name);
		else if (key > node.key)
			node.right = Insert(node.right, key,name);
		else // Duplicate keys not allowed
			return node;

		/* 2. Update height of this ancestor node */
		node.height = 1 + Max(Height(node.left),
							Height(node.right));

		/* 3. Get the balance factor of this ancestor
			node to check whether this node became
			unbalanced */
		int balance = GetBalance(node);

		// If this node becomes unbalanced, then there
		// are 4 cases Left Left Case
		if (balance > 1 && key < node.left.key)
			return RightRotate(node);

		// Right Right Case
		if (balance < -1 && key > node.right.key)
			return LeftRotate(node);

		// Left Right Case
		if (balance > 1 && key > node.left.key)
		{
			node.left = LeftRotate(node.left);
			return RightRotate(node);
		}

		// Right Left Case
		if (balance < -1 && key < node.right.key)
		{
			node.right = RightRotate(node.right);
			return LeftRotate(node);
		}

		/* return the (unchanged) node pointer */
		return node;
	}

	// A utility function to print preorder traversal
	// of the tree.
	// The function also prints height of every node
	void PreOrder(Node node)
	{
		if (node != null)
		{
			Console.Write(node.name +": "+ node.key + " --------- ");
			PreOrder(node.left);
			PreOrder(node.right);
		}
	}

	// Driver code
	public static void Main(String[] args)
	{
		AVLTree tree = new AVLTree();

		Console.WriteLine("Enter Name and Then Age of Players and Enter quit for name and 0 for age to END the program ");
		/* Constructing tree given in the above figure */
		String nameHere;
		int ageHere;
		
		do
		{
			nameHere = (Convert.ToString(Console.ReadLine())).ToUpper();
			ageHere = Convert.ToInt32(Console.ReadLine());
			tree.root = tree.Insert(tree.root, ageHere, nameHere);

		} while (!((nameHere.Equals("QUIT"))&&(ageHere==0)));

		Console.Write("Players and their age: ");
		tree.PreOrder(tree.root);

	}
}
