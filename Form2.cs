using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiplayer_game_met_bois
{
    public partial class frmTerrain : Form
    {
        public frmTerrain()
        {
            InitializeComponent();
            
            this.tabPage1.Controls.Add(TerrainPicBox);
            TerrainPicBox.Parent = tabPage1;
            TerrainPicBox.Location = new Point(0,0);
            TerrainPicBox.Size = new Size(4000, 800);
             
        }
        public PictureBox TerrainPicBox = new PictureBox();
        private void Form2_Load(object sender, EventArgs e)
        {
            //this.Location = 
        }
    }
}
