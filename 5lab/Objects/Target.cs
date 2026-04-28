using System.Drawing.Drawing2D;

namespace _5lab.Objects
{
    class Target : BaseObject
    {

        public float Radius = 20f;

        public Action<Target> OnSizeZero;

        public Target(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.LightGreen), -Radius, -Radius, Radius * 2, Radius * 2);

            Radius -= 0.1f;

            if (Radius <= 0)
            {
                OnSizeZero(this);
            }
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }

    }
}