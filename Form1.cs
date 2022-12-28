using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace Multiplayer_game_met_bois
{
    public partial class Form1 : Form   //Server class
    {
        Stopwatch timer = new Stopwatch();
        static int elapsedTime = 0;
        int count = 0;
        double FixedDeltaTime; //'n poging om 'n variable te kry om 'n vinnige masjien uit te kanseleer

        public Form1()
        {
            InitializeComponent();
            //timer.Start();
            //while (true)
            //{
                //count++;
                //if (Convert.ToInt32(timer.ElapsedMilliseconds) > 100) break; 
            //}
            //FixedDeltaTime = 1/(count*1000/timer.ElapsedMilliseconds);
            //timer.Stop();
            //MessageBox.Show(FixedDeltaTime.ToString());
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Server server = new Server();
            //server.start(txtHost.Text, Convert.ToInt32(txtPort.Text));
            Thread ServerThread = new Thread(() => 
            Server.start(txtHost.Text, Convert.ToInt32(txtPort.Text)));
            ServerThread.Start();
            //Server.start(txtHost.Text, Convert.ToInt32(txtPort.Text));
            asyncVoorbeeld();
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

        void TrueFixedUpdate() //Word tussen 61 en 63 keer per sekonde gecall
        {
            count++;
            //MessageBox.Show(timer.ElapsedMilliseconds.ToString());
            //MessageBox.Show(elapsedTime.ToString());
            //txtOutput.Text = output;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Client.connect(txtHostClient.Text, Convert.ToInt32(txtPortClient.Text));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Client.Message(txtMessage.Text);
        }

        private void btnSendServer_Click(object sender, EventArgs e)
        {
            //Server.Message(txtOutput.Text, default(Socket)!);
        }
    }

    class Server
    {
        public static void start(string ip, int port)
        {
            Socket Serverlistener = new Socket(AddressFamily
                .InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);

            Serverlistener.Bind(ep);
            Serverlistener.Listen(100);
            //rtServer.Text += "Server is listening";
            MessageBox.Show("Server is listening");

            Server s = new Server();
            Socket ClientSocket = default(Socket)!;
            int counter = 0;
            while (true)
            {
                counter++;
                ClientSocket = Serverlistener.Accept();
                //rtServer.Text += (counter.ToString() + " Clients connected");
                MessageBox.Show(counter.ToString() + " Clients connected");
                Thread UserThread = new Thread(new ThreadStart(() =>  s.User(ClientSocket)));  //verander
                //Thread UserThread = new Thread(new ThreadStart(() => s.User(ClientSocket)));  // ou een
                UserThread.Start();
                //await Task.Delay(100); 
            }
        }
        public void User(Socket client)  //verander
        {
            while (true)
            {
                byte[] msg = new byte[1024];
                //int size = client.Receive(msg);
                if (Encoding.Default.GetString(msg).Length > 3)
                {
                    MessageBox.Show("Message from client: " + Encoding.Default.GetString(msg));
                    msg = Encoding.Default.GetBytes("Server has received your message");
                    client.Send(msg);
                }                   
                //client.Send(msg, 0, size, SocketFlags.None);   
                //break;                                  //nuut
                //await Task.Run(() => User(client)); //nuut
            }
        }   
        //public static void Message(string input, Socket client) //Hierdie gaan nie werk nie ook onnodig
        //{
            //byte[] msg = new byte[1024];
            //msg = Encoding.Default.GetBytes(input);
            //client.Send(msg);
        //}
    }

    class Client
    {
        private static Socket ClientSocket = new Socket(AddressFamily
                .InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static void connect(string ip, int port)
        {
            //string ip = "127.0.0.1";//txtHostClient.Text;
            //int port = 8910;//Convert.ToInt32(txtPort.Text);          
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);

            ClientSocket.Connect(ep);
            //rt.Lines.Append("Server is listening");
            MessageBox.Show("Client is connected");
        }

        public static void Message(string messageFromClient)
        {
            ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient), 0,
                messageFromClient.Length, SocketFlags.None);

            byte[] msgFromServer = new byte[1024];
            int size = ClientSocket.Receive(msgFromServer);
            MessageBox.Show("Server responds: " +
                System.Text.Encoding.ASCII.GetString(msgFromServer, 0, size));
        } 
    }

}