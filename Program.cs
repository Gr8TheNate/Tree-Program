using System;

namespace Tree_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[1000];
            int counter = 0;
            String line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\Nathan\\documents\\visual studio 2015\\Projects\\Tree Program\\Tree Program\\TreeRandomData.txt");
            while ((line = file.ReadLine()) != null)
            {
                array[counter] = Int32.Parse(line);
                counter++;
            }

            file.Close();
            
            Tree t = new Tree();
            t.checkOperatingStatistics(array);
            Console.ReadLine();
        }

    }//End Program Class

    class TreeNode
    {
        private int data;
        private int frequency;
        private TreeNode left;
        private TreeNode right;

        //Make TreeNode
        public TreeNode(int data)
        {
            this.data = data;
            this.frequency = 1;
            this.left = null;
            this.right = null;
        }

        //Getters and setters
        public int getData()
        {
            return data;
        }

        public int getFrequency()
        {
            return frequency;
        }

        public void incrementFrequency()
        {
            this.frequency += 1;
        }

        public TreeNode getLeft()
        {
            return left;
        }

        public void setLeft(int left)
        {
            this.left = new TreeNode(left);
        }

        public TreeNode getRight()
        {
            return right;
        }

        public void setRight(int right)
        {
            this.right = new TreeNode(right);
        }

    }//End TreeNode Class

    class Tree
    {
        protected TreeNode root;
        private int comparisons;
        
        //Initialize Tree
        public Tree()
        {
            this.root = null;
        }

        public void incrementComparisons()
        {
            this.comparisons += 1;
        }

        public int getComparisons()
        {
            return comparisons;
        }

        public void resetComparisons()
        {
            this.comparisons = 0;
        }

        //Builds Tree from array
        public void buildTree(int[] array)
        {
            TreeNode current;
            for (int i = 0; i < array.Length; i++)
            {
                if (root == null)
                {
                    root = new TreeNode(array[i]);
                }
                else
                {
                    incrementComparisons();
                    current = root;
                    bool searching = true;
                    while (searching)
                    {
                        if (array[i] < current.getData())       //If data is less than current.data, send left
                        {
                            if (current.getLeft() == null)
                            {
                                current.setLeft(array[i]);
                                searching = false;
                            }
                            else
                            {
                                incrementComparisons();
                                current = current.getLeft();
                            }
                        }
                        else if (array[i] > current.getData())      //If data is greater than current.data, send right 
                        {
                            if (current.getRight() == null)
                            {
                                current.setRight(array[i]);
                                searching = false;
                            }
                            else
                            {
                                incrementComparisons();
                                current = current.getRight();
                            }
                        }
                        else                                        //If data is equal to current.data, increment frequency                     
                        {
                            incrementComparisons();
                            current.incrementFrequency();
                            searching = false;
                        }
                    } //End while loop
                }//End else
            } //End for loop
        }

        //Print Tree iteratively
        public void printIteratively(TreeNode t)
        {
            Stack s = new Stack();
            t = root;

            do
            {
                while(t != null)
                {
                    s.push(t);
                    t = t.getLeft();            //If t.left is not null, then t is compared to t.left
                    if (t != null)
                        incrementComparisons();
                }

                if (!s.isEmpty())
                {
                    t = s.pop();
                    for (int i = 0; i < t.getFrequency(); i++)
                    {
                        Console.Write(t.getData() + ", ");
                    }
                    t = t.getRight();           //If t.right is not null, then t is compared to t.right
                    if (t != null)
                        incrementComparisons();
                }

            } while (t != null || !s.isEmpty());//While t is not null OR stack is not empty

        }

        //Print Tree recursively
        public void printRecursively(TreeNode t)
        {
            if (t.getLeft() != null)
            {
                incrementComparisons();
                printRecursively(t.getLeft());
            }

            for (int i = 0; i < t.getFrequency(); i++)
            {
                Console.Write(t.getData() + ", ");      //Prints all frequencies of t.data
            }

            if (t.getRight() != null)
            {
                incrementComparisons();
                printRecursively(t.getRight());
            }
        }

        //Check number of comparisons in building, sorting, and printing tree
        public void checkOperatingStatistics(int[] list)
        {
            Console.WriteLine("Checking Operating Statistics");
            Console.WriteLine("-------------------------------");
            buildTree(list);
            Console.WriteLine("The buildTree method has : " + getComparisons() + " comparisons.");
            resetComparisons();
            Console.WriteLine("\nIterative Print:");
            printIteratively(root);
            Console.WriteLine("\nThe iterative method has : " + getComparisons() + " comparisons.");
            resetComparisons();
            Console.WriteLine("\nRecursive Print:");
            printRecursively(root);
            Console.WriteLine("\nThe recursive method has : " + getComparisons() + " comparisons.");
            resetComparisons();
            bubbleSort(list);
            Console.WriteLine("\nThe bubbleSort has " + getComparisons() + " comparisons");
        }

        //Brute force search
        public void bubbleSort(int[] array)
        {
            bool swapping = true;

            while (swapping)
            {
                swapping = false;
                for (int k = 1; k < array.Length; k++)
                {
                    if (array[k] < array[k - 1])
                    {
                        incrementComparisons();
                        int temp = array[k - 1];
                        array[k - 1] = array[k];
                        array[k] = temp;

                        swapping = true;
                    }
                }
            }

        }

    }//End Tree Class

    class Stack
    {
        TreeNode[] stack;
        private int top;

        //Initializes Stack
        public Stack()
        {
            stack = new TreeNode[20];
            this.top = -1;
        }

        public Stack(int max)
        {
            stack = new TreeNode[max];
            this.top = -1;
        }

        //Returns true if stack is empty
        public bool isEmpty()
        {
            return (top == -1);
        } 

        //Pushes node onto the stack
        public void push(TreeNode t)
        {
            top++;
            stack[top] = t;
        }

        //Pops node off the stack
        public TreeNode pop()
        {
            TreeNode t = stack[top];
            top--;
            return t;
        }
    }//End Stack class

}//End namespace
