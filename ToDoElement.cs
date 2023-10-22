using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class ToDoElement
    {
        public Button buttonRemove { get; }
        public Label label { get; }
        public Button buttonToTop { get; }

        public ToDoElement(string text, bool bold)
        {
            buttonRemove = new Button();
            buttonRemove.Text = "X";
            buttonRemove.Font = new Font("Arial", 15);
            buttonRemove.Size = new Size(40, 40);
            buttonRemove.Visible = true;
            buttonRemove.AccessibleName = "ToDoListElements";

            label = new Label();
            label.Text = text;
            label.Size = new Size(400, 50);
            if (bold)
            {
                label.Font = new Font("Arial", 14, FontStyle.Bold);
            }
            else
            {
                label.Font = new Font("Arial", 14);
            }
            label.Visible = true;
            label.AccessibleName = "ToDoListElements";

            buttonToTop = new Button();
            buttonToTop.Text = "UP";
            buttonToTop.Font = new Font("Arial", 15);
            buttonToTop.Size = new Size(50, 40);
            buttonToTop.Visible = true;
            buttonToTop.AccessibleName = "ToDoListElements";
        }

        public void setPosition(int x, int y)
        {
            buttonRemove.Location = new Point(x, y);
            label.Location = new Point(x + 55, y + 10);
            buttonToTop.Location = new Point(x + 480, y);
        }
    }
}
