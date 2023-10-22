using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class DataToDo
    {
        public LinkedList<ToDoElement> toDoElements = new LinkedList<ToDoElement>();
        private static Form1 f;

        static private DataToDo toDoData = null;
        private DataToDo() {toDoData= this;}
        static public DataToDo data(Form1 f2)
        {
            f = f2;
            if (toDoData == null)
            {
                toDoData = new DataToDo();
            }
            return toDoData;
        }

        public void addToDo(ToDoElement element, bool start, bool toTopPossible)
        {
            if (start && toTopPossible)
            {
                toDoElements.AddFirst(element);
            }
            else
            {
                toDoElements.AddLast(element);
            }
            saveList();
        }

        public void removeToDo(Button element)
        {
            foreach(ToDoElement tde in toDoElements)
            {
                if (tde.buttonRemove == element)
                {
                    toDoElements.Remove(tde);
                    break; // Sehr Wichtig !!!!! Sonst hat der beim naechsten Durchgang ein Fehler
                }
            }
            saveList();
        }

        public void toTopToDo(Button element)
        {
            foreach (ToDoElement tde in toDoElements)
            {
                if (tde.buttonToTop == element)
                {
                    ToDoElement tde2 = tde;
                    toDoElements.Remove(tde);
                    toDoElements.AddFirst(tde2);
                    break;
                }
            }
            saveList();
        }

        public List<ToDoElement> getToDo()
        {
            List<ToDoElement> l = new List<ToDoElement>();
            foreach (ToDoElement tde in toDoElements)
            {
                l.Add(tde);
            }
            return l;
        }

        public void atStart()
        {
            if (File.Exists("ToDo.apb"))
            {
                string[] lines = File.ReadAllLines("ToDo.apb");
                foreach (string line in lines)
                {
                    string[] s = line.Split('$');
                    if (s[2] == "0")
                    {
                        f.createAdditionalButton(s[1], false, false);
                    }
                    else
                    {
                        f.createAdditionalButton(s[1], true, false);
                    }
                }
            }
            else
            {
                using (FileStream fs = File.Create("ToDo.apb"));
            }
            
            //Jede Linie wird so gespeichert: "$Text $Bold". 
        }

        public void saveList()
        {
            File.WriteAllText(@"ToDo.apb", string.Empty);
            using (StreamWriter writer = new StreamWriter("ToDo.apb"))
            {
                foreach (ToDoElement tde in toDoElements)
                {
                    if (tde.label.Font.Style == FontStyle.Bold)
                    {
                        writer.WriteLine("$" + tde.label.Text + " $1");
                    }
                    else
                    {
                        writer.WriteLine("$" + tde.label.Text + " $0");
                    }
                }
            } 
        }
    }
}
