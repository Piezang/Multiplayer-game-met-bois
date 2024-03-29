
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.CodeDom.Compiler;
using System.Web;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Media;
using Microsoft.VisualBasic.Devices;
using System.Drawing.Drawing2D;
using System;

namespace Multiplayer_game_met_bois
{
    public partial class Form1 : Form   //Server class
    {
        public static Form1 form1Instance;
        public PictureBox Pic;
        public static PictureBox hpbox;
        bool DontUpdate = true;
        Stopwatch timer = new Stopwatch();
        static int elapsedTime = 0;
        int count = 0;
        double FixedDeltaTime; //'n poging om 'n variable te kry om 'n vinnige masjien uit te kanseleer 
        PictureBox TankPictureBox;
        PictureBox EnemyPictureBox;
        public Point TankPicPos;
        Bitmap bitmap = new Bitmap(4000, 800);
        Bitmap TankBitMap = new Bitmap(140, 140);
        bool TerrainEdited = false;
        frmTerrain f2 = new frmTerrain();

        public Form1()
        {
            InitializeComponent();
            f2.Show();
            f2.BringToFront();

            this.BackColor = Color.AliceBlue;
            this.TransparencyKey = Color.AliceBlue;
            tabPage1.BackColor = TransparencyKey;

            form1Instance = this;
            hpbox = HPbox;
            timer1.Enabled = true; ;
            KeyPreview = true;
            this.MouseClick += Form1_MouseClicked!;

            bitmap = terrain.TerrainImage(bitmap);
            t = terrain.TerrainImage(bitmap);
            //pictureBox1.Image = bitmap;
            f2.TerrainPicBox.Image = bitmap;

            HPbox.Image = b;



            TankPictureBox = new PictureBox();
            TankPictureBox.Location = new Point(SpawnPt.X - (int)(TankBitMap.Width / 2) - 25, SpawnPt.Y - (int)(TankBitMap.Height / 2) - 25);
            TankPictureBox.Size = TankBitMap.Size;
            this.tabPage1.Controls.Add(TankPictureBox);
            TankPictureBox.BringToFront();
            TankPictureBox.Parent = pictureBox1;
            TankPictureBox.BackColor = Color.FromArgb(0, 0, 0, 0);

            EnemyPictureBox = new PictureBox();
            EnemyPictureBox.Location = new Point(SpawnPt.X - (int)(TankBitMap.Width / 2) - 25, SpawnPt.Y - (int)(TankBitMap.Height / 2) - 25);
            EnemyPictureBox.Size = TankBitMap.Size;
            this.tabPage1.Controls.Add(EnemyPictureBox);
            EnemyPictureBox.BringToFront();
            EnemyPictureBox.Parent = pictureBox1;
            EnemyPictureBox.BackColor = Color.FromArgb(0, 0, 0, 0);
            cmboTankPick.SelectedIndex = 1;
        }
        static Point SpawnPt = new Point(200, 350);
        SharpShooterTank tank; // = new SharpShooterTank(SpawnPt, 1, new Coordinate(0,0), 180);
        SharpShooterTank ServerTank = new SharpShooterTank(new Point(80, 20), 1, new Coordinate(0, 0), 0);
        //ServerTank tree eintlik net op as die ander tank in die konneksie. Nie noodwendig die server se tank nie.

        private void Form1_keyPress(object sender, KeyPressEventArgs e)
        {
            DontUpdate = false;
            if (e.KeyChar == 'z')
            {
                player.SoundLocation = path + "boom.wav";
                //@"c:\mywavfile.wav"
                //player.Play();  //"C:\Users\Alexander\Desk
                bitmap = tank.ShootUlt(bitmap); pictureBox1.Image = bitmap;
                return;
            }
            //MessageBox.Show(e.KeyChar.ToString());
            tank.Move(Char.ToLower(e.KeyChar));
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                MessageBox.Show($"Form.KeyPress: '{e.KeyChar}' pressed.");

                switch (e.KeyChar)
                {
                    case (char)49:
                    case (char)52:
                    case (char)55:
                        MessageBox.Show($"Form.KeyPress: '{e.KeyChar}' consumed.");
                        e.Handled = true;
                        break;
                }
            }
        }
        int lengthMoved = 0;
        int actualMoved = 0;
        Bitmap b = new Bitmap(230, 59);

        private void MoveCameraView(Point pos)
        {
            lengthMoved = pos.X;
            //pictureBox1.Location = new Point(-lengthMoved + 650, pictureBox1.Location.Y);
            f2.TerrainPicBox.Location = new Point(-lengthMoved + 650, pictureBox1.Location.Y);

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            unsafe
            {
                SharpShooterTank sharp = new SharpShooterTank(SpawnPt, 1, new Coordinate(0, 0), 180);
                SharpShooterTank* sharpPointer = &sharp;
                //fixed (SharpShooterTank* sharpPointer = sharp)
                //{
                    if (cmboTankPick.Text == "SharpShooter") { tank = *sharpPointer; }
                //}
                //(sharpPointer)->tank;
            }   //(SharpShooterTank)sharpPointer;

            Thread ServerThread = new Thread(() =>
            Server.start(txtHost.Text, Convert.ToInt32(txtPort.Text), tank.Position));
            ServerThread.Start();
        }
        async void asyncVoorbeeld()
        {
            timer.Reset();
            timer.Start();
            count = 0;

            //Form1 f =new Form1();
            while (true)
            {

                //if (Convert.ToInt32(timer.ElapsedMilliseconds) >= Math.Round(1000/60f)) 
                //{
                //Parallel.Invoke(new Action(() => { FixedUpdate(); }));  dit voel gevaarlik
                //Thread Update = new Thread(new ThreadStart(() => f.FixedUpdate()));
                await FixedUpdate();
                //elapsedTime = Convert.ToInt32(timer.ElapsedMilliseconds); timer.Stop();
                //}

                //if (Convert.ToInt32(timer.ElapsedMilliseconds) >= 1000)
                //{
                //MessageBox.Show("1 seconds passed");
                //break;
                //}

            }
            MessageBox.Show(elapsedTime.ToString());
            MessageBox.Show("fps is: " + count.ToString());
        }

        async Task FixedUpdate()
        {
            //Form1 f = new Form1();
            await Task.Delay(14);  //Baie weird moet eintlik 17 wees maar ok
            TrueFixedUpdate();
        }

        public void TrueFixedUpdate() //Word tussen 61 en 63 keer per sekonde gecall
        {
            count++;
        }

        TerrainGen terrain = new TerrainGen(4000 - 1);
        bool TerrainGenerated = false;
        bool generated = false;
        
        int[] newTerrainFromServer;
        //Die probleem is iets met static variables

        private void btnConnect_Click(object sender, EventArgs e)  //Client
        {
            Server.ServerBitmap = TerrainGen.ServerTerrain;
            //MessageBox.Show(TerrainGen.ServerTerrain[1].ToString());
            Client.connect(txtHostClient.Text, Convert.ToInt32(txtPortClient.Text));
            //MessageBox.Show(Server.ServerBitmap[1].ToString());

            newTerrainFromServer = Client.Ready();
            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Brushes.SaddleBrown);
            Pen pen2 = new Pen(Brushes.DarkGreen);
            g.Clear(Color.Pink);
            for (int i = 0; i < newTerrainFromServer.Length; i++)
            {
                Point pt1 = new Point(i, 800);  //497
                Point pt2 = new Point(i, newTerrainFromServer[i] - 10 + 10);
                Point pt3 = new Point(i, newTerrainFromServer[i] + 10);
                g.DrawLine(pen, pt1, pt2);
                g.DrawLine(pen2, pt2, pt3);
            }
            terrain.terrain = new Bitmap(bitmap);
            //MessageBox.Show(newTerrainFromServer[1].ToString() + " index 1");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Client.Message(txtMessage.Text);
        }

        private void btnSendServer_Click(object sender, EventArgs e)
        {
            //Server.Message(txtOutput.Text, default(Socket)!);
            txtOutput.Clear();
            //change();
        }

        Bitmap test = new Bitmap(140,140);

        private async void TimerUpdate(object sender, EventArgs e)   //100 keer per sekonde
        {
            TerrainEdited = false;
            if (DontUpdate) return;
            if (!Server.Active)    //As hy nie die server is nie...
            {
                if (newTerrainFromServer == null) return;
                if (newTerrainFromServer[200 - 2] == 0) return;
            }                           //pictureBox1.Width //nuut
            if (projectileList.Count != 0)
            {
                foreach (Projectile p in projectileList)
                {
                    if (!p.Destroyed) { bitmap = p.ImageChange(bitmap, 4, 2); TerrainEdited = true; }
                    if (tank.CircleCollided(p.cPosition) && !p.Destroyed)
                    { tank.Damage(p.damage); p.Destroyed = true; Graphics.FromImage(t).FillRectangle(Brushes.Pink, p.Position.X, p.Position.Y, p.plength, p.pheight); }
                    if (p.cPosition.y > 1000) { projectileList.Remove(p); break; }
                    if (p.TerrainInteractionV.X * p.force.x == 0 || p.TerrainInteractionV.Y * p.force.y == 0)
                    { projectileList.Remove(p);
                        Graphics.FromImage(t).FillEllipse(Brushes.Pink, p.Position.X - 45, p.Position.Y - 45, 60, 60);
                        TerrainEdited = true;
                        break;
                    }
                }
            }

            Graphics g;
            //g = Graphics.FromImage(bitmap);
         
            tank.Position = new Point((int)tank.cPosition.x, (int)tank.cPosition.y);  //nuut
            int PanForce = tank.Position.X;
            Server.ServerTankCords = tank.Position;  //Message na die client   
            Server.ServerMousePoint = tank.MousePoint;


            int PanForce = tank.Position.X;
            if ((tank.Position.X > 650) && (tank.Position.X < 3233))
            {
                MoveCameraView(new Point(PanForce, 0), g);

                TankBitMap = tank.UpdateImage(bitmap, TankBitMap, TankBitMap.Size, 50, 50);

                TankBitMap.MakeTransparent(Color.Pink);

                TankPictureBox.Image = TankBitMap;

                TankPictureBox.Location = new Point(600, tank.updateTankPicPos(TankBitMap.Size).Y);
            }
            else
            {
                TankBitMap = tank.UpdateImage(bitmap, TankBitMap, TankBitMap.Size, 50, 50);

                TankBitMap.MakeTransparent(Color.Pink);

                TankPictureBox.Image = TankBitMap;

                TankPictureBox.Location = tank.updateTankPicPos(TankBitMap.Size);
            }

            if (TerrainEdited == true)
            {
                f2.TerrainPicBox.Image = bitmap;  //UpdateImage.updateImage(bitmap, tank, tank.cPosition); 
            }
            
            if (Client.connected)   //As hy die client is gebeur die
            {  
                tank.Position = new Point((int)tank.cPosition.x, (int)tank.cPosition.y);  //nuut
                
                Client.Message(tank.Position.ToString() + ">" + tank.AimPoint + shot);   //Message na die server
                if (shot == "Y") shot = "N";
                string t = Client.ServerCords.Trim();       //Server se response, sy tank se cords
                int x = Convert.ToInt32(t.Substring(t.IndexOf('=')+1, t.IndexOf(',') - t.IndexOf('=')-1));
                int y = Convert.ToInt32(t.Substring(t.IndexOf('Y') + 2, t.IndexOf('}') - t.IndexOf('Y')-2));
                Point Servercanonpoint = Client.ServerMousePoint;
                
                g = Graphics.FromImage(bitmap);               
                //g.FillEllipse(Brushes.Pink, ServerTank.Position.X, ServerTank.Position.Y, 50, 50);
                ServerTank.Position = new Point(x, y);   //PanForce was nie daar nie
                ServerTank.cPosition = new Coordinate(x, y);

                if (Client.ServerProjectileShot) 
                    await SpawnProjectile(Servercanonpoint, ServerTank.Position);  //await dalk hier?
   
                //g.DrawLine(new Pen(Brushes.Yellow), new Point(x + 25, y + 25), Servercanonpoint);
                //g.DrawImage(ServerTank.SharpShooterTankimg, ServerTank.Position.X, ServerTank.Position.Y);
                EnemyPictureBox.Location = new Point(x - 47, y - 47);
                pictureBox1.Image = bitmap;            
            }
            Point othertankCanonPoint = Server.OtherTankCanonPoint;
            if (Server.ClientTankCords!= "") //As hy die server is gebeur die
            {
                //if (Server.ServerProjectileShot == "Y") MessageBox.Show("Projectile shot is true");
                string t = Server.ClientTankCords.Trim();       //Client se response, sy tank se cords
                int x = Convert.ToInt32(t.Substring(t.IndexOf('=') + 1, t.IndexOf(',') - t.IndexOf('=') - 1));
                //MessageBox.Show(x);
                int y = Convert.ToInt32(t.Substring(t.IndexOf('Y') + 2, t.IndexOf('}') - t.IndexOf('Y') - 2));
                
                g = Graphics.FromImage(bitmap);
                //g.DrawLine(new Pen(Brushes.Pink), new Point(x + 25, y + 25), othertankCanonPoint);
                //g.FillEllipse(Brushes.Pink, ServerTank.Position.X-2, ServerTank.Position.Y-2, 50, 50);
                othertankCanonPoint = Server.OtherTankCanonPoint;
                
                ServerTank.Position = new Point(x, y);
                ServerTank.cPosition = new Coordinate(x , y);  //nuut
                if (Server.ProjectileShot) 
                    await SpawnProjectile(othertankCanonPoint, ServerTank.Position);  //await dalk hier?
                //g.DrawLine(new Pen(Brushes.Yellow), new Point(x + 25, y + 25), othertankCanonPoint);
                //g.DrawImage(ServerTank.SharpShooterTankimg, ServerTank.Position.X, ServerTank.Position.Y);
                EnemyPictureBox.Location = new Point(x - 47, y - 47);
                pictureBox1.Image = bitmap;
                //(SpawnPt.X - (int)(TankBitMap.Width / 2) - 25, SpawnPt.Y - (int)(TankBitMap.Height / 2) - 25);
            }
        }
        public static Bitmap t;
        public static int X, Y;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {            
            tank.MouseMoved = true;
            X = e.X; Y = e.Y;
        }

        List<Projectile> projectileList = new List<Projectile>();
        private void Form1_MouseClicked(object sender, MouseEventArgs e)
        {
            
        }

        bool stop = false;
        static string path = "C:/Users/Alexander/Desktop/Programming/2022/Desember/Multiplayer game met bois/assets/klanke/";
        SoundPlayer player = new SoundPlayer(path);
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            stop = true;   
        }
        private async void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //Thread t = new Thread(()=> MouseDownn(sender, e));
            //t.Start();   
            if (e.Button != MouseButtons.Left || stop || ammocount < 1) return;
            while (e.Button == MouseButtons.Left && ammocount > 0)
            {
                await SpawnProjectile(tank.MousePoint, tank.Position);   
                if (stop || e.Clicks > 1)
                { stop = false; return; }
            }
        }

        private int ammocount = SharpShooterTank.MaxAmmo;
        private string shot = "N";

        private void TankPicked(object sender, EventArgs e)
        {
            switch(cmboTankPick.Text)
            {
                case "SharpShooter":
                    {
                        ServerTank = new SharpShooterTank(SpawnPt, 3, new Coordinate(0, 0), 120);
                        //tank = ;
                    } 
                    break;
                case "MachineGun":
                    break;
            }
        }

        private async Task SpawnProjectile(Point mouse, Point pos)
        {
            shot = "Y";
            if (Server.ClientTankCords != "") // as die een die server is:
                Server.ServerProjectileShot = "Y";
            if (pos == ServerTank.Position) //as die bullet van die ander tank afkomstig is:
            {
                ammocount++; shot = "N";  
                if (Server.ClientTankCords != "")  // as die een die server is:
                    Server.ServerProjectileShot = "N";    
            }
           
            ammocount--;  
            lblAmmo.Text = "AMMO: " + ammocount.ToString();
            player.Stop();
            player.SoundLocation = path + "woosh.wav";
            //@"c:\mywavfile.wav"
            //player.Play();  //"C:\Users\Alexander\Desktop\Programming\2022\Desember\Multiplayer game met bois\assets\klanke\woosh.mp3"
            float value = mouse.X - pos.X - 25;
            if (Math.Abs(value) <= 0.5) { value = 0.5f; }
            Projectile p = new Projectile(mouse.X, mouse.Y,
            3.5, new Coordinate((value) / 6,
            (mouse.Y - pos.Y-25) / 6), 10);
            projectileList.Add(p);
            await Task.Delay(100);
        }     
    }

    class Server
    {
        public static int[] ByteArrToInt(byte[] input)
        {
            int[] output = new int[4000];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Convert.ToInt32(input[4 * i] + 
                    input[4 * i + 1] + input[4 * i + 2] + input[4 * i + 3]);
            }
            return output;
        }

        public static byte[] IntArrToByte(int[] input)
        {
            int kaas = 0;
            byte[] result = new byte[input.Length*4];
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case < 256:
                        { 
                            result[i * 4] = Convert.ToByte(input[i]);
                            result[i * 4 + 1] = Convert.ToByte(0);
                            result[i * 4 + 2] = Convert.ToByte(0);
                            result[i * 4 + 3] = Convert.ToByte(0);
                        }
                        break;
                    case < 511:
                        {
                            result[i * 4] = Convert.ToByte(255);
                            result[i * 4 + 1] = Convert.ToByte(input[i] - 255);
                            result[i * 4 + 2] = Convert.ToByte(0);
                            result[i * 4 + 3] = Convert.ToByte(0);
                        }
                        break;
                    case < 766:
                        {
                            result[i * 4] = Convert.ToByte(255);
                            result[i * 4 + 1] = Convert.ToByte(255);
                            result[i * 4 + 2] = Convert.ToByte(input[i] - 510);
                            result[i * 4 + 3] = Convert.ToByte(0);
                        }
                        break;
                    default:
                        {
                            result[i * 4] = Convert.ToByte(255);
                            result[i * 4 + 1] = Convert.ToByte(255);
                            result[i * 4 + 2] = Convert.ToByte(255);
                            result[i * 4 + 3] = Convert.ToByte(input[i] - 765);
                        }
                        break;
                }
            }  
            return result;
        }

        public static int[] ServerBitmap;  //readonly en static constructors?
        static Server()
        {
            ServerBitmap = TerrainGen.ServerTerrain;    
        }
        public static Point ServerTankCords;
        public static string ServerProjectileShot;
        public static Point ServerMousePoint;

        public static string ClientTankCords = "";
        public static Point OtherTankCanonPoint;
        public static bool ProjectileShot = false;
        public static int counter = 0;
        public static bool Active = false;
        public static void start(string ip, int port, Point cords)
        {
            Active = true;
            ServerTankCords = cords;
            Socket Serverlistener = new Socket(AddressFamily
                .InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);

            Serverlistener.Bind(ep);
            Serverlistener.Listen(100);
            MessageBox.Show("Server is listening");

            Server s = new Server();
            Socket ClientSocket = default(Socket)!;
            
            while (true)
            {
                counter++;
                ClientSocket = Serverlistener.Accept();
                MessageBox.Show(counter.ToString() + " Clients connected");
                Thread UserThread = new Thread(new ThreadStart(() => s.User(ClientSocket)));  //verander
                                                                                              //Thread UserThread = new Thread(new ThreadStart(() => s.User(ClientSocket)));  // ou een
                UserThread.Start();
            }
        }
       
        public async void User(Socket client)
        {
            while (true)
            {
                byte[] msg = new byte[1024*1];   //1024  //2048 2048 * 2 = 4kilo bytes WAS *7
                int size = client.Receive(msg);
                string message = System.Text.Encoding.ASCII.GetString(msg, 0, size);
                
                if (message[0] == 'm') //Cords message
                {  
                    message = message.Substring(1);
                    ProjectileShot = false;
                    if (message[message.Length - 1] == 'Y')
                    {
                        //await Task.Delay(100); 
                        ProjectileShot = true;
                    }
                    message = message.Remove(message.Length-1);
                    
                    ClientTankCords = message.Substring(1, message.IndexOf("}"));
                    string sPoint = message.Substring(message.IndexOf(">") + 1, message.Length - 1 - message.IndexOf(">"));
                    OtherTankCanonPoint = new Point(Convert.ToInt32(sPoint.Substring(0, sPoint.IndexOf('|'))),
                        Convert.ToInt32(sPoint.Substring(sPoint.IndexOf('|') + 1)));
                       
                    msg = Encoding.Default.GetBytes(ServerTankCords.ToString() + //Stuur my eie cords vir client
                        ServerMousePoint.ToString() + ServerProjectileShot);    //Stuur CanonAngle en stuff
                    
                    client.Send(msg);
                    ServerProjectileShot = "N"; 
                }
                if (message == "Ready")  //TErrain message
                {
                    client.Send(IntArrToByte(ServerBitmap));
                }
                
            }
        }
    }

    class Client
    {
        public static string ServerCords = "";
        public static Point ServerMousePoint;
        public static bool ServerProjectileShot;
        public static bool connected = false;
        private static Socket ClientSocket = new Socket(AddressFamily
                .InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static void connect(string ip, int port)
        {        
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            ClientSocket.Connect(ep);
            MessageBox.Show("Client is connected");
            connected= true;
        }

        public static void Message(string messageFromClient)
        {
            //ClientSocket.Close();                       //nuut
            //Client.connect(_ip, Convert.ToInt32(_port));//nuut
                                                        //Message van server is so groot dat daar oorstaande is wat cords data aanstuur besoedel

            messageFromClient = "m" + messageFromClient;
            ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient), 0,
                messageFromClient.Length, SocketFlags.None);
            //ClientSocket.Send(Server.IntArrToByte(new int[1000]), 0, messageFromClient.Length, SocketFlags.None);

            byte[] msgFromServer = new byte[50];  //WAS 1024 * 1 (kan eintlik 26 wees)
            int size = ClientSocket.Receive(msgFromServer);
            string msg = Encoding.Default.GetString(msgFromServer);
            
            ServerProjectileShot = false;
            //MessageBox.Show(msg.Substring(msg.Length - 1));
            if (msg[26] == 'Y') { /*MessageBox.Show("Client also sees it");*/ ServerProjectileShot = true; }

            //ServerCords = msg;   
            ServerCords = msg.Substring(0, msg.IndexOf('}')+1);

            string t = msg.Substring(msg.IndexOf('}') + 1).Trim();       //Server se response, sy tank se cords
            int x = Convert.ToInt32(t.Substring(t.IndexOf('=') + 1, t.IndexOf(',') - t.IndexOf('=') - 1));
            //MessageBox.Show(x);
            int y = Convert.ToInt32(t.Substring(t.IndexOf('Y') + 2, t.IndexOf('}') - t.IndexOf('Y') - 2));

            ServerMousePoint = new Point(x,y);
        }
        public static int[] Ready()
        {         
            string msgToServer = "Ready";
            MessageBox.Show(Server.ServerBitmap[1].ToString());
            ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(msgToServer), 0,
                msgToServer.Length, SocketFlags.None);
            
            int[] ServerBitmapp = new int[4000];
            byte[] msgFromServer = new byte[8000 * 2];   //1024  2048 * 2 = 4kilo bytes  //WAS * 7
             
            int size = ClientSocket.Receive(msgFromServer);
            //MessageBox.Show("Server responds: " +

            ServerBitmapp = Server.ByteArrToInt(msgFromServer);
            return ServerBitmapp;
        }
    }
}