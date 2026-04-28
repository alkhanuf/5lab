using _5lab.Objects;

namespace _5lab
{
    public partial class Form1 : Form
    {

        Player player;
        Marker marker;
        List<BaseObject> objects = new();
        Random rand = new Random();
        int score = 0;
        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            
            
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(player);
            objects.Add(marker);

            for (int i = 0; i < 2; i++)
            {
                createTarget();
            }

            player.OnTargetOverlap += (t) =>
            {
                objects.Remove(t);

                score++;
                lblScore.Text = $"Очки: {score}";

                createTarget();
            };
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            updatePlayer();

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                }
            }

            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.7f;
                player.vY += dy * 0.7f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void createTarget()
        {
            int newX = rand.Next(50, pbMain.Width - 50);
            int newY = rand.Next(50, pbMain.Height - 50);
            Target newTarget = new Target(newX, newY, 0);

            newTarget.OnSizeZero += (t) =>
            {

                int newXpos = rand.Next(50, pbMain.Width - 50);
                int newYpos = rand.Next(50, pbMain.Height - 50);
                newTarget.Radius = 20f;
                newTarget.X = newXpos;
                newTarget.Y = newYpos;
             };

            objects.Add(newTarget);
        }
    }
}