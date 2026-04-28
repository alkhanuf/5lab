using _5lab.Objects;

namespace _5lab
{
    public partial class Form1 : Form
    {

        MyRectangle myRect;
        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(100, 100, 45);
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            // вытаскиваем матрицу преобразования Graphics
            var matrix = g.Transform;
            matrix.Translate(myRect.X, myRect.Y);
            matrix.Rotate(myRect.Angle); // смещаем ее в пространстве
            g.Transform = matrix; // устанавливаем новую матрицу

            myRect.Render(g);
        }
    }
}
