
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

namespace Multiplayer_game_met_bois
{
    public partial class Form1 : Form   //Server class
    {
        public static Form1 form1Instance;
        public PictureBox Pic;
        bool DontUpdate = true;
        Stopwatch timer = new Stopwatch();
        static int elapsedTime = 0;
        int count = 0;
        double FixedDeltaTime; //'n poging om 'n variable te kry om 'n vinnige masjien uit te kanseleer

        public Form1()
        {
            InitializeComponent();
            form1Instance = this;
            Pic = pictureBox1;
            //tabPage = tabPage1;
            //tabControl = Tabs;

            //timer.Start();
            //while (true)
            //{
            //count++;
            //if (Convert.ToInt32(timer.ElapsedMilliseconds) > 100) break; 
            //}
            //FixedDeltaTime = 1/(count*1000/timer.ElapsedMilliseconds);
            //timer.Stop();
            //MessageBox.Show(FixedDeltaTime.ToString());
            timer1.Enabled = true;
            //timer1.Enabled = false;
            KeyPreview = true;
            this.MouseClick += Form1_MouseClicked!;

            bitmap = terrain.TerrainImage(bitmap);
            //bitmap = terrain.TerrainImage(bitmap);
            //Terrain = terrain.ServerTerrain;
        }  

        SharpShooterTank tank = new SharpShooterTank(new Point(200, 350), 1, new Coordinate(0,0), 180);
        SharpShooterTank ServerTank = new SharpShooterTank(new Point(-10, -10), 1, new Coordinate(0, 0), 0);
        //ServerTank tree eintlik net op as die ander tank in die konneksie. Nie noodwendig die server se tank nie.

        private void Form1_keyPress(object sender, KeyPressEventArgs e)
        {
            DontUpdate = false;
            //MessageBox.Show(e.KeyChar.ToString());
            if (e.KeyChar != 'z') tank.Move(Char.ToLower(e.KeyChar));
            
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
            if (e.KeyChar == 'z') 
            {
                player.SoundLocation = path + "boom.wav";
                //@"c:\mywavfile.wav"
                player.Play();  //"C:\Users\Alexander\Desk
                bitmap = tank.ShootUlt(bitmap); pictureBox1.Image = bitmap; 
            }
        }
        int lengthMoved = 0;
        int actualMoved = 0;
        private void MoveCameraView(Point pos, Graphics g)
        {                                           //4000                                                  //4000
            //g.DrawImage(bitmap, new Rectangle(0, 0, 4000, 497), new Rectangle(pos.X + lengthMoved, 0, 4000, 497), GraphicsUnit.Pixel);
            pictureBox1.Location = new Point(-lengthMoved, pictureBox1.Location.Y); 
            actualMoved += pos.X / 6;
            lengthMoved+= pos.X;  //onseker oor die * 4 ding moet eintlik iets anders wees
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            MessageBox.Show("gecall");
            //if (hoeveelheidGecall < 3000) return;
            //using (Bitmap bitmapp = new Bitmap(bitmap))
            //{          
            //e.Graphics.DrawImage
                Bitmap bitmapp = new Bitmap(pictureBox1.Image);
                e.Graphics.DrawImage(bitmapp, new Point(-100, 0));   //Kan Layers in terrain maak as daar baie is
                //pictureBox1.Image= bitmap;
                //bitmap.Dispose();
            //}
            //bitmap = new Bitmap(pictureBox1.Image);
            //Bitmap newbitmap = new Bitmap(bitmap);

            //e.Graphics.DrawImage(newbitmap, new Rectangle(10, 0, bitmap.Width, bitmap.Height));
            //new Rectangle(10, 0, bitmap.Width, bitmap.Height);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Server server = new Server();
            //server.start(txtHost.Text, Convert.ToInt32(txtPort.Text));

            Thread ServerThread = new Thread(() =>
            Server.start(txtHost.Text, Convert.ToInt32(txtPort.Text), tank.position));
            ServerThread.Start();

            //asyncVoorbeeld();
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

            /*int count = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                count++;
            }*/

            //output += "r";
            //MessageBox.Show(output);

            TrueFixedUpdate();
            //await Task.Run(FixedUpdate); // Wag totdat task klaar is
            //Wag vir daardie hoeveelheid millisekondes

            //return;
        }

        public void TrueFixedUpdate() //Word tussen 61 en 63 keer per sekonde gecall
        {
            count++;
            //MessageBox.Show(timer.ElapsedMilliseconds.ToString());
            //MessageBox.Show(elapsedTime.ToString());
            //txtOutput.Text = output;
        }

        TerrainGen terrain = new TerrainGen(4000 - 1);  
        bool TerrainGenerated = false;
        bool generated = false;
        Bitmap bitmap = new Bitmap(4000, 800);
        //int[] Terrain = new int[883];
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
            g.Clear(Color.Black);
            for (int i = 0; i < newTerrainFromServer.Length; i++)
            {
                Point pt1 = new Point(i, 497);
                Point pt2 = new Point(i, newTerrainFromServer[i]-10 + 10);
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

        private void TimerUpdate(object sender, EventArgs e)   //60 keer per sekonde
        {

            

            if (DontUpdate) return;
            if (!Server.Active)    //As hy nie die server is nie...
            {
                if (newTerrainFromServer == null) return;
                if (newTerrainFromServer[200 -2] == 0) return;
            }                           //pictureBox1.Width //nuut
            if (projectileList.Count != 0)
            {
                foreach (Projectile p in projectileList)
                { 
                    bitmap = p.ImageChange(bitmap, 1, 1);
                    if (p.cPosition.y > 500) { projectileList.Remove(p); break; }
                }           
                //MessageBox.Show(k.force.ToString());
            }
            Graphics g;
            int PanForce = (int)(tank.force.x * 3.5);
            //MessageBox.Show("Running");
            tank.position = new Point((int)tank.cPosition.x, (int)tank.cPosition.y);  //nuut
            Server.ServerTankCords = tank.position;  //Message na die client
                                                   
            g = Graphics.FromImage(bitmap);
            //g.Clear(Color.Black); 
            bitmap = terrain.TerrainImage(bitmap);
            if (((pictureBox1.Location.X < 0 && PanForce < 0) 
            || ( PanForce > 0)) &&
            ((pictureBox1.Location.X > -2200 && PanForce > 0) 
            || (PanForce < 0)))
            MoveCameraView(new Point(PanForce, 0), g);
            bitmap = tank.UpdateImage(bitmap, 100, 50);  //probeer om die ander een te gebruik
            pictureBox1.Image = bitmap;  //UpdateImage.updateImage(bitmap, tank, tank.cPosition); 

            if (Client.connected)   //As hy die client is gebeur die
            {
                //MessageBox.Show("Kaas2");
                tank.position = new Point((int)tank.cPosition.x, (int)tank.cPosition.y);  //nuut
                Client.Message(tank.position.ToString());   //Message na die server
                string t = Client.ServerCords.Trim();       //Server se response, sy tank se cords
                int x = Convert.ToInt32(t.Substring(t.IndexOf('=')+1, t.IndexOf(',') - t.IndexOf('=')-1));
                //MessageBox.Show(x);
                int y = Convert.ToInt32(t.Substring(t.IndexOf('Y') + 2, t.IndexOf('}') - t.IndexOf('Y')-2));
                //MessageBox.Show(y);
                
                g = Graphics.FromImage(bitmap);               
                g.FillRectangle(Brushes.Black, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                ServerTank.position = new Point(x, y);   //PanForce was nie daar nie
                ServerTank.cPosition = new Coordinate(x, y);
                
                g.FillRectangle(Brushes.White, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                pictureBox1.Image = bitmap;            
            } 
            if (Server.ClientTankCords!= "") //As hy die server is gebeur die
            {
                string t = Server.ClientTankCords.Trim();       //Client se response, sy tank se cords
                int x = Convert.ToInt32(t.Substring(t.IndexOf('=') + 1, t.IndexOf(',') - t.IndexOf('=') - 1));
                //MessageBox.Show(x);
                int y = Convert.ToInt32(t.Substring(t.IndexOf('Y') + 2, t.IndexOf('}') - t.IndexOf('Y') - 2));

                g = Graphics.FromImage(bitmap);
    
                g.FillRectangle(Brushes.Black, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                ServerTank.position = new Point(x , y);
                ServerTank.cPosition = new Coordinate(x , y);  //nuut
                g.FillRectangle(Brushes.White, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                pictureBox1.Image = bitmap;
            }
        }

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
            while (e.Button == MouseButtons.Left)
            {
                await SpawnProjectile();
                if (stop || e.Clicks > 2) { stop = false;  return; }
            }     
        }
        private async Task SpawnProjectile()
        {
            player.Stop();
            player.SoundLocation = path + "woosh.wav";
            //@"c:\mywavfile.wav"
            //player.Play();  //"C:\Users\Alexander\Desktop\Programming\2022\Desember\Multiplayer game met bois\assets\klanke\woosh.mp3"
            Projectile p = new Projectile(tank.MousePoint.X, tank.MousePoint.Y,
            3.5, new Coordinate((tank.MousePoint.X - tank.position.X) / 6,
            (tank.MousePoint.Y - tank.position.Y) / 6));
            projectileList.Add(p);
            await Task.Delay(200);
        }

       
    }

    class Server
    {
        public static int[] ByteArrToInt(byte[] input)
        {
            int[] output = new int[4000];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = Convert.ToInt32(input[2 * i] + input[2 * i + 1]);
            }
            return output;
        }

        public static byte[] IntArrToByte(int[] input)
        {
            int kaas = 0;
            byte[] result = new byte[input.Length*2];
            for (int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 0) { kaas = input[i / 2]; }

                if (kaas < 255 && i % 2 == 0)
                {
                    result[i] = Convert.ToByte(kaas);
                    result[i + 1] = Convert.ToByte(0); i++; continue;
                }

                if (kaas >= 255) 
                { result[i] = Convert.ToByte(254); kaas -= 254; continue; }
                result[i] = Convert.ToByte(kaas);
            }
            return result;
        }

        public static int[] ServerBitmap;  //readonly en static constructors?
        static Server()
        {
            ServerBitmap = TerrainGen.ServerTerrain;    
        }
        public static Point ServerTankCords; 
        public static string ClientTankCords = "";
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
       
        public void User(Socket client)
        {
            while (true)
            {
                byte[] msg = new byte[2048*4];   //1024  //2048 2048 * 2 = 4kilo bytes WAS *7
                int size = client.Receive(msg);
                string message = System.Text.Encoding.ASCII.GetString(msg, 0, size);
                //MessageBox.Show(message);
                if (message[0] == 'm') //Cords message
                {
                    message = message.Substring(1);
                    //MessageBox.Show(message);
                    //MessageBox.Show("Message from client: " + message);
                    ClientTankCords = message;
                    msg = Encoding.Default.GetBytes(ServerTankCords.ToString());    //Stuur my eie cords vir client
                    client.Send(msg);
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

            byte[] msgFromServer = new byte[2048 * 2];  //WAS * 2
            int size = ClientSocket.Receive(msgFromServer);
            
            //if (Encoding.Default.GetString(msgFromServer).IndexOf('}') < 0)   //As die boodskap nie '}' bevat nie stuur weer 'n message om die socket te clear
            //{
                //Message(messageFromClient.Substring(1));
            //}

            //MessageBox.Show("Server responds: " +
            //System.Text.Encoding.ASCII.GetString(msgFromServer, 0, size));
            string msg = Encoding.Default.GetString(msgFromServer);
            if (ServerCords == msg)
            {
                return;
            }
            ServerCords = msg;       
            //MessageBox.Show(ServerCords);
        }
        public static int[] Ready()
        {         
            string msgToServer = "Ready";
            MessageBox.Show(Server.ServerBitmap[1].ToString());
            ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(msgToServer), 0,
                msgToServer.Length, SocketFlags.None);
            
            int[] ServerBitmapp = new int[4000];
            byte[] msgFromServer = new byte[8000];   //1024  2048 * 2 = 4kilo bytes  //WAS * 7
             
            int size = ClientSocket.Receive(msgFromServer);
            //MessageBox.Show("Server responds: " +

            ServerBitmapp = Server.ByteArrToInt(msgFromServer);

            /*string msg = Encoding.Default.GetString(msgFromServer);
            //MessageBox.Show(msg);
            int count = 0;

            //if (msg[0] == 't')
            //{
                //msg = msg.Substring(1);
                foreach (char c in msg)
                {                       //Hierdie moet groter wees
                    if (c == ' ' && count < 4000  && msg.IndexOf(' ') > -1)
                    {
                        //MessageBox.Show(msg.Substring(0, msg.IndexOf(' ') ));
                        //MessageBox.Show(msg.Substring(0, msg.IndexOf(' ')));
                        ServerBitmapp[count] = Convert.ToInt32(msg.Substring(0, msg.IndexOf(' ')));
                        //MessageBox.Show(ServerBitmapp[count].ToString());
                        msg = msg.Remove(0, msg.IndexOf(' ') + 1);
                        //MessageBox.Show(msg);
                        count++;
                    }
                }
                MessageBox.Show(count.ToString() + " count");
            //}*/
            return ServerBitmapp;
        }
    }
}