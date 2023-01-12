
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

namespace Multiplayer_game_met_bois
{
    public partial class Form1 : Form   //Server class
    {
        public static Form1 form1Instance;
        public PictureBox Pic;
    
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
            //Terrain = terrain.ServerTerrain;
            pictureBox1.Image = bitmap;
        }  

        SharpShooterTank tank = new SharpShooterTank(new Point(100, 100), 1, new Point(0,0), 180);
        SharpShooterTank ServerTank = new SharpShooterTank(new Point(-10, -10), 1, new Point(0, 0), 0);
        //ServerTank tree eintlik net op as die ander tank in die konneksie. Nie noodwendig die server se tank nie.

        private void Form1_keyPress(object sender, KeyPressEventArgs e)
        {    
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

        TerrainGen terrain = new TerrainGen(883 - 1);
        bool TerrainGenerated = false;
        bool generated = false;
        Bitmap bitmap = new Bitmap(883, 497);
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
            g.Clear(Color.Black);
            for (int i = 0; i < newTerrainFromServer.Length; i++)
            {
                Point pt1 = new Point(i, 497);
                Point pt2 = new Point(i, newTerrainFromServer[i]);
                g.DrawLine(pen, pt1, pt2);
            }
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
            if (!Server.Active)    //As hy nie die server is nie...
            {
                if (newTerrainFromServer == null) return;
                if (newTerrainFromServer[pictureBox1.Width - 2] == 0) return;
            }
            if (k != null)
            {
                bitmap = k.ImageChange(bitmap); //MessageBox.Show("Kaas");
                //MessageBox.Show(k.force.ToString());
            }                        

            //MessageBox.Show("Running");
            Server.ServerTankCords = tank.position;  //Message na die client
            //txtOutput.Text += "K";
             
            bitmap = tank.UpdateImage(bitmap);
            pictureBox1.Image = bitmap;
      
            Graphics g;

            if (Client.connected)   //As hy die client is gebeur die
            {           
                Client.Message(tank.position.ToString());   //Message na die server
                string t = Client.ServerCords.Trim();       //Server se response, sy tank se cords
                int x = Convert.ToInt32(t.Substring(t.IndexOf('=')+1, t.IndexOf(',') - t.IndexOf('=')-1));
                //MessageBox.Show(x);
                int y = Convert.ToInt32(t.Substring(t.IndexOf('Y') + 2, t.IndexOf('}') - t.IndexOf('Y')-2));
                //MessageBox.Show(y);
                
                g = Graphics.FromImage(bitmap);
                g.DrawRectangle(Pens.Black, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                g.FillRectangle(Brushes.Black, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                ServerTank.position = new Point(x, y);
                
                g.DrawRectangle(Pens.White, ServerTank.position.X, ServerTank.position.Y, 10, 10);
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
                g.DrawRectangle(Pens.Black, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                g.FillRectangle(Brushes.Black, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                ServerTank.position = new Point(x, y);

                g.DrawRectangle(Pens.White, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                g.FillRectangle(Brushes.White, ServerTank.position.X, ServerTank.position.Y, 10, 10);
                pictureBox1.Image = bitmap;
            }
        }
        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {         
            tank.ChangeMouseCoords(e.X,e.Y,bitmap);     
        }

        Projectile k;
        bool projectileCreated = false;
        private void Form1_MouseClicked(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("kaas");
            projectileCreated= true;
            Projectile p = new Projectile(e.X, e.Y, 3.5, new Point(5, -6));
            k = p;
            //bitmap = p.ImageChange(bitmap);
            //for (int i = 0; i < 100; i++)
            //{
                
                //Thread.Sleep(10);
            //}
        }

        private void Form1_keyDown(object sender, KeyEventArgs e)
        {

        }
    }

    class Server
    {
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
                byte[] msg = new byte[2048*2];   //1024  //2048 2048 * 2 = 4kilo bytes
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
                    //MessageBox.Show("Map not synced");
                    //MessageBox.Show(ServerBitmap[1].ToString());
                    string pieceOfMap = "t";
                    for (int i = 0; i < ServerBitmap.Length; i++)
                    {
                        //MessageBox.Show(numbers[i].ToString()); 
                        pieceOfMap += ServerBitmap[i].ToString() + " ";
                    }
                    msg = Encoding.Default.GetBytes(pieceOfMap);
                    client.Send(msg);   
                    // Jy moet in die client maaak dat hy die map data lees en map skep
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
            messageFromClient = "m" + messageFromClient;
            ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient), 0,
                messageFromClient.Length, SocketFlags.None);

            byte[] msgFromServer = new byte[1024];
            int size = ClientSocket.Receive(msgFromServer);
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
            
            int[] ServerBitmapp = new int[883];
            byte[] msgFromServer = new byte[2048*2];   //1024  2048 * 2 = 4kilo bytes
            int size = ClientSocket.Receive(msgFromServer);
            //MessageBox.Show("Server responds: " +
            string msg = Encoding.Default.GetString(msgFromServer);
            //MessageBox.Show(msg);
            int count = 0;

            if (msg[0] == 't')
            {
                msg = msg.Substring(1);
                foreach (char c in msg)
                {      
                    if (c == ' ' && count < 882 && msg.IndexOf(' ') > -1)
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
            }
            return ServerBitmapp;
        }
    }

}