using System.Runtime.CompilerServices;
using System.Windows.Forms.Design;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        DataToDo d = null;

        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            //MessageBox.Show("Welcome ^^", "Start", MessageBoxButtons.OK);
            InitializeComponent();
            d = DataToDo.data(this);
            d.atStart();
            fillToDoList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox tb = null;
            CheckBox cb = null;
            foreach (Control item in this.Controls)
            {
                if (item.AccessibleName == "enteredToDo")
                {
                    tb = (TextBox)item;
                }
                if (item.AccessibleName == "enteredPrio")
                {
                    cb = (CheckBox)item;
                }
            }
            string text = tb.Text;
            if (text != "")
            {
                clearToDoList();
                tb.Text = "";
                bool prio = cb.Checked;
                if (prio)
                {
                    cb.Checked = false;
                }
                createAdditionalButton(text, prio, true);

                fillToDoList();
            }
            
            /*createAdditionalButton("Mache garnichts", false);
            createAdditionalButton("Gehe zu Uni", false);
            createAdditionalButton("Schlafe", false);
            createAdditionalButton("Kaufe Essen", true);*/
        }


        private void show<T> (T obj) where T : Control
        {
            this.Controls.Add(obj);
        }
        private void remove<T>(T obj) where T : Control
        {
            this.Controls.Remove(obj);
        }

        public void createAdditionalButton(string text, bool bold, bool toTopPossible)
        {
            clearToDoList();
            ToDoElement toDoElement = new ToDoElement(text, bold);
            toDoElement.buttonRemove.Click += new EventHandler(toDoRemove);
            toDoElement.buttonToTop.Click += new EventHandler(toDoToTop);
            d.addToDo(toDoElement, bold, toTopPossible);
            fillToDoList();
        }

        private void toDoRemove(Object sender, EventArgs e)
        {
            clearToDoList();
            Button b = (Button)sender;
            d.removeToDo(b);
            fillToDoList();
        }

        private void toDoToTop(Object sender, EventArgs e)
        {
            clearToDoList();
            Button b = (Button)sender;
            d.toTopToDo(b);
            fillToDoList();
        }
        private void clearToDoList()
        {
            List<ToDoElement> listTde = d.getToDo();
            foreach (ToDoElement tde in listTde)
            {
                this.Controls.Remove(tde.buttonRemove);
                this.Controls.Remove(tde.label);
                this.Controls.Remove(tde.buttonToTop);
            }
        }

        private void fillToDoList()
        {
            int i = 0;
            List<ToDoElement> listTde = d.getToDo();
            foreach (ToDoElement tde in listTde)
            {
                tde.setPosition(85, 120 + i * 50);
                this.Controls.Add(tde.buttonRemove);
                this.Controls.Add(tde.label);
                this.Controls.Add(tde.buttonToTop);
                i++;
            }
        }
        private void checkEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button b = null;
                foreach (Control item in this.Controls)
                {
                    if (item.AccessibleName == "buttonToDo")
                    {
                        b = (Button)item;
                        break;
                    }
                }
                b.PerformClick();
            }
        }
    }
}