using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Name_Sorter
{
    class Program
    {

        public class Name
        {
            public string firstname1;
            public string firstname2;
            public string firstname3;
            public string surname;
        }

        ///*  888
        [STAThread]         // args will be the parameters following .exe 
        //*/
        static void Main(string[] args)
        {

            ArrayList arraylist = new ArrayList();
            //string curPath=System.AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine(curPath);
            //Console.WriteLine(Directory.GetCurrentDirectory());
            //Console.WriteLine(Environment.CurrentDirectory);

            // Read the names from the unsorted-names-list.txt

            ///*  888
            UnsortedNames(FilePath(args[0]),arraylist);
            //*/

            //// Pre-run
            ////UnsortedNames(FilePath_Pre(), arraylist);
            ////

            int namesCount = arraylist.Count;
            Name[] names = new Name[namesCount];
            Name[] sortedNames = new Name[namesCount];

            NamesToTable(names, arraylist);

            //foreach (var s in names)
            //{
            //    Console.WriteLine("{0} {1} {2} {3}", s.firstname1, s.firstname2, s.firstname3, s.surname);
            //}
            //Console.WriteLine("\n");

            SortedNames(names, sortedNames);

            Output(FilePath("sorted-names-list.txt"), sortedNames);

            //Console.WriteLine("\n");
            //foreach (var s in sortedNames)
            //{
            //    Console.WriteLine("{0} {1} {2} {3}", s.firstname1, s.firstname2, s.firstname3, s.surname);
            //}


        }
        
        public static string FilePath(string relativePath)

        {
            // Get the path of unsorted-names-list.txt
            string unsNamesFile=System.IO.Path.GetFullPath(relativePath);
            return unsNamesFile;
        }
        
        public static void UnsortedNames(string filePath, ArrayList arraylist)
        {
            //Read the unsorted names from the specified text file.
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {

                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        //Console.WriteLine(line);
                        arraylist.Add(line);
                        line = sr.ReadLine();

                    }
                }
                // Console.WriteLine(arraylist.Count);
                // Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in reading unsorted-names-list.txt:");
                Console.WriteLine(ex.Message);
                arraylist = null;
            }
            //return arraylist;
        }
        public static void NamesToTable(Name[] names, ArrayList arraylist)
        {
            // Convert the names into firstname1, firstname2, firstname3, surname
            int namei = 0;
            foreach (string s in arraylist)
            {
                string[] partsOfName = s.Split(' ');
                if (partsOfName.Length == 1)
                {
                    names[namei] = new Name { firstname1 = "", firstname2 = "", firstname3 = "", surname = partsOfName[0] };
                }
                else if (partsOfName.Length == 2)
                {
                    names[namei] = new Name { firstname1 = partsOfName[0], firstname2 = "", firstname3 = "", surname = partsOfName[1] };
                }
                else if (partsOfName.Length == 3)
                {
                    names[namei] = new Name { firstname1 = partsOfName[0], firstname2 = partsOfName[1], firstname3 = "", surname = partsOfName[2] };
                }
                else if (partsOfName.Length == 4)
                {
                    names[namei] = new Name { firstname1 = partsOfName[0], firstname2 = partsOfName[1], firstname3 = partsOfName[2], surname = partsOfName[3] };
                }
                else
                {
                    Console.WriteLine("Please check the name {0}", s);
                }
                namei++;
            }
        }
        public static void SortedNames(Name[] names, Name[] sortedNames)
        {
            // Sort the names by surname, firstname1, firstname2, firstname 3
            var sorted = from name in names
                         orderby name.surname, name.firstname1, name.firstname2, name.firstname3
                         select name;
            int namei = 0;
            foreach (var name in sorted)
            {
                sortedNames[namei] = new Name
                {
                    firstname1 = name.firstname1,
                    firstname2 = name.firstname2,
                    firstname3 = name.firstname3,
                    surname = name.surname
                };
                namei++;
            }
        }
        public static void Output(string filePath,Name[] sortedNames)
        {
            // Display the sorted names and store them into sorted-name-list.txt

            Console.WriteLine("\nThe sorted names are:\n");

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var name in sortedNames)
                {
                    string s= "";
                    if (name.firstname1=="")
                    {
                        Console.WriteLine(name.surname);
                        sw.WriteLine(name.surname);
                    }
                    else if (name.firstname1!="" & name.firstname2 == "")
                    {
                        s = String.Join(" ",name.firstname1,name.surname);
                        Console.WriteLine(s);
                        sw.WriteLine(s);
                    }else if(name.firstname1 != "" & name.firstname2 != "" & name.firstname3=="")
                    {
                        s = String.Join(" ", name.firstname1,name.firstname2, name.surname);
                        Console.WriteLine(s);
                        sw.WriteLine(s);
                    }
                    else
                    {
                        s = String.Join(" ", name.firstname1, name.firstname2, name.firstname3, name.surname);
                        Console.WriteLine(s);
                        sw.WriteLine(s);
                    }
                }

            }

            Console.WriteLine("\nThe sorted names are stored in sorted-name-list.txt");
        }

        //Pre-run
        public static string FilePath_Pre()
        {
            // Get the path of unsorted-names-list.txt

            string curPath = Environment.CurrentDirectory;
            string[] toFindPath = curPath.Split('\\');
            string filePath = "";
            int i;
            for (i = 0; i < toFindPath.Length; i++)
            {
                if (toFindPath[i] == "bin") break;
            }
            for (int j = 0; j < i - 1; j++)
            {
                filePath = filePath + toFindPath[j] + "\\";
            }
            string unsNamesFile = filePath + "/unsorted-names-list.txt";

            return unsNamesFile;
        }
        //

    }
}