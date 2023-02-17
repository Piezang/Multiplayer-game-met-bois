using Multiplayer_game_met_bois;
using System;
using System.Diagnostics.Contracts;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;

public class Rigidbody
{
    public Point position { get; set; }
    protected Point gravity { get; set; }
    public Coordinate force { get; set; }
    public Coordinate cPosition { get; set; }
    protected double mass;

    public Bitmap bmp;

    public int gravityTimer;

    public Point Force;

    public bool colided;

    public Point TerrainInteractionV;

    public Rigidbody()
    {
        gravityTimer = 0;
        grav = 0;
        colided = false;
        //mass = gravity.Y;
    }

    public string Direction;

    public Point[] GetPixelCoords(Point position, int length, int height) 
    {
        Point[] PixelCoords = new Point[length * height];
        int count = 0;
        for (int x = position.X; x < position.X + length; x++)
        {
            for (int y = position.Y; y < position.Y + height; y++)
            {
                PixelCoords[count] = new Point(x, y);
                count ++;
            }
        }
        return PixelCoords;
    }

    public Color[] GetPixelColor(Point[] pixelcoords, Bitmap bmp)
    {
        Color[] c = new Color[pixelcoords.Length];

        for (int i = 0; i < c.Length; i ++)
        {
            c[i] = bmp.GetPixel(pixelcoords[i].X, pixelcoords[i].Y);
           // c[i] = Color.Red;
        }
        return c;
    }

    bool hitboundry;
    private Point TerrainInteraction(Bitmap bmp, int length, int height)
    {
        hitboundry = false;

        Point CollisionAdjuster = new Point(1, 1);
        Point NW = new Point(position.X - 1, position.Y - 1);
        Point SW = new Point(position.X - 1, position.Y + height);
        Point NE = new Point(position.X + length, position.Y - 1);
        Point SE = new Point(position.X + length, position.Y + height);

        switch (position.Y)
        {
            case <= 20: if (force.y + gravity.Y < 0) { { CollisionAdjuster = new Point(CollisionAdjuster.X, 0); hitboundry = true; } } break;
            case >= 725: if (force.y + gravity.Y > 0) { { CollisionAdjuster = new Point(CollisionAdjuster.X, 0); } } break;
        }
        switch (position.X)
        {
            case <= 20: if (force.x < 0) { CollisionAdjuster = new Point(0, CollisionAdjuster.Y); } break;
            case >= 3925 : if (force.x > 0) { CollisionAdjuster = new Point(0, CollisionAdjuster.Y); } break;
        }
        
        for (int i = SW.X; i <= SE.X; i++)
        {
            Color c = bmp.GetPixel(i, SW.Y);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.y + gravity.Y >= 0) { { CollisionAdjuster = new Point(CollisionAdjuster.X, 0); } } break;
            }
        }

        for (int i = NW.X; i <= NE.X; i++)
        {
            Color c = bmp.GetPixel(i, NW.Y);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.y + gravity.Y < 0) { { CollisionAdjuster = new Point(CollisionAdjuster.X, 0); } } break;
            }
        }

        if (colided == true)
        {
            force = new Coordinate(force.x, force.y + 1);
            colided = false;
        }

        for (int i = NE.Y + 1; i <= SE.Y - 1; i++)
        {
            Color c = bmp.GetPixel(NE.X, i);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.x > 0)
                    {
                        CollisionAdjuster = new Point(0, CollisionAdjuster.Y);
                        if (force.y + gravity.Y == 0)
                        { CollisionAdjuster = new Point(0, 1); force = new Coordinate(force.x, force.y - 1); colided = true; }
                        if (force.y + gravity.Y > 0)
                        { CollisionAdjuster = new Point(0, 0); }
                    }
                    break;
            }
        }

        for (int i = NW.Y + 1; i <= SW.Y - 1; i++)
        {
            Color c = bmp.GetPixel(NW.X, i);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.x < 0)
                    {
                        CollisionAdjuster = new Point(0, CollisionAdjuster.Y);
                        if (force.y + gravity.Y == 0)
                        { CollisionAdjuster = new Point(0, 1); force = new Coordinate(force.x, force.y- 1); colided = true; }
                        if (force.y + gravity.Y > 0)
                        { CollisionAdjuster = new Point(0, 0); }
                    }
                    break;
            }
        }
        
        return CollisionAdjuster;
        // CollisionAdjuster = new Point(0, 0);
    }

    private Point TerrainInteraction2(Bitmap bitmap, Point force, Point Gravity, Point projectedpos)
    {
       

        return new Point(0,0);
    }

    double grav;
    int impactForce = 0;
    public bool called = false;
    public bool vall = false;

    public bool UpdatePos(Bitmap bitmap, int length, int height)    //
    {
        //if (called) MessageBox.Show("Kaas");
        grav += (mass * 0.03);
        if (gravity.Y < 8)
        {
            gravity = new Point(0,
                Convert.ToInt32(grav));
        }
        if (TerrainInteraction(bitmap, length, height).Y == 0 && hitboundry == false)  //|| TerrainInteraction(bitmap, 10, 10).X == 0
        {  
            gravity = new Point(0, 0); grav = 0;
            if (called)
            { 
                force = new Coordinate(0,0); vall = true;
                //Graphics g = Graphics.FromImage(bitmap);
                //g.FillEllipse(Brushes.Black, position.X-4, position.X + 4, 8, 8);
                return true; 
            } //MessageBox.Show(force.x.ToString());
        }
        /*cPosition = new Coordinate(cPosition.x + TerrainInteraction(bitmap, length, height).X * (force.x)
            , cPosition.y + TerrainInteraction(bitmap, length, height).Y * (grav + force.y));
        position = new Point((int)cPosition.x, (int)cPosition.y);*/
        TerrainInteractionV = TerrainInteraction(bitmap, length, height);
        cPosition = new Coordinate(cPosition.x + TerrainInteractionV.X * (force.x)
           , cPosition.y + TerrainInteraction(bitmap, length, height).Y * (grav + force.y));
        position = new Point((int)cPosition.x, (int)cPosition.y);
        return false;
    }
}

class BaseTank : Rigidbody
{   
	private Coordinate newForce;
	public Coordinate MovementForce { get; set; }
    public Point MousePoint;
	public double AimAngle;
    public int LocalCoordsX = 3;
    public int LocalCoordsY = 3;
	protected double CanonAngle
	{
		get { return AimAngle; }
		set 
		{
            if ((value >= 0) && (value < 360))
            AimAngle = value; 
		}
	}

    public void ChangeMouseCoords(int XInput, int YInput, Bitmap bitmap, Point oldPos)
    {
        DrawCannon(bitmap,true, oldPos);  //oldPos   
        LocalCoordsX = XInput;
        LocalCoordsY = YInput;
        DrawCannon(bitmap,false, position);
    }
    //Hieronder sal jy bron vind van "nee andor jou mors van suurstof en spasie jou harlekyn van die dieretuin wat maak jy daai moet nie so werk nie

    protected void DrawCannon(Bitmap bitmap, bool DeleteLine, Point position)
    {
        float CanonCentreX = position.X + 25;
        float CanonCentreY = position.Y + 25;
        int val = 1;
        float CanonMouseDiffX = (LocalCoordsX - CanonCentreX);
        float CanonMouseDiffY = (LocalCoordsY - CanonCentreY);
        if (CanonMouseDiffX < 0) val = -1; 

        double canonLength = 60;
        double Difference = ((CanonMouseDiffY))/((CanonMouseDiffX));
        //MessageBox.Show(Difference.ToString());
        double angle = Math.Atan(Difference); //- Math.PI/2; /// Math.PI * 180;
        AimAngle = angle * val;
        //MessageBox.Show(angle.ToString());
        double x = canonLength * Math.Cos(angle) * val; 
        double y = canonLength * Math.Sin(angle) * val;
        MousePoint = new Point((int)(x + CanonCentreX), (int)(y + CanonCentreY));

        Graphics g = Graphics.FromImage(bitmap);
        
        if (DeleteLine)
        {
            g.DrawLine(new Pen(Brushes.Pink), CanonCentreX, CanonCentreY, (CanonCentreX + (float)x) , (CanonCentreY + (float)y) );
            return;
        }
        
        //nee andor jou stuk nonsens
        //Pen goldPen = new Pen(Color.Gold, 1);
        g.DrawLine(new Pen(Brushes.Gold),CanonCentreX,CanonCentreY, (CanonCentreX + (float)x), (CanonCentreY + (float)y));
    }

    Bitmap hpboxBitmap = new Bitmap(230, 59);
    protected int TakeDamage(float damage, float health, float maxhp)
    {
        health -= damage;
        Graphics g = Graphics.FromImage(hpboxBitmap); 
        if (((health / maxhp) > 0.66) && ((health / maxhp) < 1.01)) g.Clear(Color.Lime);
        else if (((health / maxhp) > 0.33f) && ((health / maxhp) <= 0.66f)) g.Clear(Color.Yellow);
        else if ((health / maxhp) <= 0.33f) g.Clear(Color.Red);

        g.FillRectangle(Brushes.Gray,
            229 - (int)((maxhp-health)*2.3), 0, 300, 59);
        
        Form1.hpbox.Image = hpboxBitmap;
        if (health <= 0)
        {
            return 0; //was -100
        }
        return (int)health;
    }
    
    public void Move(char c)
    {
        Direction = c.ToString().ToUpper();
        
        switch (Direction)
        {
            case "W": newForce = new Coordinate(0, -0.8); //MessageBox.Show(c.ToString());
                break;
            case "S": 
                newForce = new Coordinate(0, 0.6); //MessageBox.Show(c.ToString());
                if (force.y > -1) { newForce = new Coordinate(0, -force.y); }
                break;
            case "A": newForce = new Coordinate(-0.3, 0); //MessageBox.Show(c.ToString());
                break;
            case "D": newForce = new Coordinate(0.3, 0); //MessageBox.Show(c.ToString());
                break;
            //default : newForce = new Coordinate(0,0);
                //break;
            //MovementForce = newForce;                
        }

        /* while (Direction == "W")
         {
            newForce = new Point(0, -1); //MessageBox.Show(c.ToString());            
         }
         while (Direction == "A")
         {
             newForce = new Point(-1, 0); //MessageBox.Show(c.ToString());            
         }
         while (Direction == "S")
         {
             newForce = new Point(0, 1); //MessageBox.Show(c.ToString());            
         }
         while (Direction == "D")
         {
             newForce = new Point(1, 0); //MessageBox.Show(c.ToString());           
         }
        */

        if (MovementForce.x > 2.5)
        {
            MovementForce = new Coordinate(2.5, MovementForce.y);
        }
        if (MovementForce.x < -2.5)
        {
            MovementForce = new Coordinate(-2.5, MovementForce.y);
        }
        if (MovementForce.y > 3)
        {
            MovementForce = new Coordinate(MovementForce.x, 3);
        }
        if (MovementForce.y < -2)
        {
            MovementForce = new Coordinate(MovementForce.x, -2);
        }

        MovementForce = new Coordinate(newForce.x + MovementForce.x ,
            newForce.y + MovementForce.y);

        force = new Coordinate(MovementForce.x, MovementForce.y);
        //UpdatePos();
    }

    public void UnMove(char c)
    {
        Direction = c.ToString().ToUpper();

        switch (Direction)
        {
            case "W":
                newForce = new Coordinate(0, 0.8); //MessageBox.Show(c.ToString());
                break;
            case "S":
                newForce = new Coordinate(0, -0.6); //MessageBox.Show(c.ToString());
                if (force.y > -1) { newForce = new Coordinate(0, -force.y); }
                break;
            case "A":
                newForce = new Coordinate(0.3, 0); //MessageBox.Show(c.ToString());
                break;
            case "D":
                newForce = new Coordinate(-0.3, 0); //MessageBox.Show(c.ToString());
                break;
                //default : newForce = new Coordinate(0,0);
                //break;
                //MovementForce = newForce;                
        }

        /* while (Direction == "W")
         {
            newForce = new Point(0, -1); //MessageBox.Show(c.ToString());            
         }
         while (Direction == "A")
         {
             newForce = new Point(-1, 0); //MessageBox.Show(c.ToString());            
         }
         while (Direction == "S")
         {
             newForce = new Point(0, 1); //MessageBox.Show(c.ToString());            
         }
         while (Direction == "D")
         {
             newForce = new Point(1, 0); //MessageBox.Show(c.ToString());           
         }
        */

        if (MovementForce.x > 2.5)
        {
            MovementForce = new Coordinate(2.5, MovementForce.y);
        }
        if (MovementForce.x < -2.5)
        {
            MovementForce = new Coordinate(-2.5, MovementForce.y);
        }
        if (MovementForce.y > 3)
        {
            MovementForce = new Coordinate(MovementForce.x, 3);
        }
        if (MovementForce.y < -2)
        {
            MovementForce = new Coordinate(MovementForce.x, -2);
        }

        MovementForce = new Coordinate(newForce.x + MovementForce.x,
            newForce.y + MovementForce.y);

        force = new Coordinate(MovementForce.x, MovementForce.y);
        //UpdatePos();
    }

    public BaseTank() 
	{  
        //MovementForce = new Point(MovementForce.X + newForce.X,
        //MovementForce.Y + newForce.Y);
    }
    int hitboxsize = 25;
    public bool CircleCollided(Coordinate input)
    {
        if (Math.Sqrt(Math.Pow(cPosition.x + 25 - input.x, 2) + Math.Pow(cPosition.y + 25 - input.y, 2)) < hitboxsize)
        {
            return true;
        }
        return false;
    }
}

class SharpShooterTank : BaseTank
{
    public Image SharpShooterTankimg = Image.FromFile("image.png");
    Bitmap SharpShooterTankbmp = new Bitmap(100,50);  //Image.FromFile("image.png");  
    //Bitmap bitmap = new Bitmap(4000, 800);
    Graphics g;
    Graphics tank;

    public static readonly int MaxAmmo = 100;
    private int health = 100;
    public bool destroyed = false;
    public Point[] PixelCoords = new Point[5000];
    public Color[] PixelColor = new Color[5000];

    public SharpShooterTank(Point pos, int _mass, Coordinate frce, float angl) 
	{ 
		position = pos; gravity = new Point(0, _mass * 1);
		force = frce;   mass = _mass;
		CanonAngle = angl; cPosition = new Coordinate(position.X, position.Y);
    }
    public void Damage(int damage)
    {
        health = TakeDamage(damage, health, 100);
        if (health <= 0) Destroy(); //was -100      
    }
   
    //private void GiveDamage(object sender, KeyPressEventArgs)

	private void Destroy()
	{
        MessageBox.Show("Tank is Destroyed");
		//SharpShooterTankimg.Dispose();   //Ek wil he dit moet clear
        //destroyed = true;
    }
    public bool MouseMoved = false;
    Point oldPos;
    public Bitmap UpdateImage(Bitmap bitmap, int length, int height)
    {
        for (int i = 0; i < 4; i++)
        {
            
            //position = new Point((int)cPosition.x, (int)cPosition.y);
            g = Graphics.FromImage(bitmap);
            g.FillEllipse(Brushes.Pink, position.X-2, position.Y-2, length, height);  //FillRectangle
            //for (int l = 0; l < 5000; l++)
            //{
              //  bitmap.SetPixel(PixelCoords[l].X ,PixelCoords[l].Y, PixelColor[l]);
            //}
            //Wrywing
            //MovementForce = new Point((int)(MovementForce.X * 0.9), (int)(MovementForce.Y * 0.9));
            oldPos = new Point (position.X, position.Y);
   
            UpdatePos(bitmap, length, height);
            position = new Point((int)cPosition.x, (int)cPosition.y);
            ChangeMouseCoords(Form1.X, Form1.Y, bitmap, oldPos);
            MouseMoved = false;

            //g.Clip = new Region(new Rectangle(position, new Size(length, height)));
            
            //PixelCoords = GetPixelCoords(position, length, height);
            //PixelColor = GetPixelColor(PixelCoords, bitmap);

            g.DrawImage(SharpShooterTankimg, position.X, position.Y);
            //g.DrawRectangle(Pens.White, position.X, position.Y, 100, 100);
            //g.FillRectangle(Brushes.White, position.X, position.Y, length, height);
            //MessageBox.Show(position.X.ToString(), position.Y.ToString());
            //MessageBox.Show(MovementForce.ToString());
        }
        return bitmap;
    }

    public Bitmap ShootUlt(Bitmap bitmap)
    {
        Graphics g = Graphics.FromImage(bitmap);
        g.DrawLine(new Pen(Brushes.Navy), new Point(position.X + 2, position.Y + 2), new Point(Form1.X - 3, Form1.Y - 3));
        g.DrawLine(new Pen(Brushes.DarkBlue), new Point(position.X+3, position.Y+3), new Point(Form1.X-2, Form1.Y-2));
        g.DrawLine(new Pen(Brushes.Blue), new Point(position.X+4,position.Y+4), new Point(Form1.X-1, Form1.Y - 1));
        g.DrawLine(new Pen(Brushes.LightBlue), new Point(position.X + 5, position.Y +5), new Point(Form1.X, Form1.Y));
        g.DrawLine(new Pen(Brushes.Blue), new Point(position.X +6, position.Y + 6), new Point(Form1.X+1, Form1.Y+1));
        g.DrawLine(new Pen(Brushes.DarkBlue), new Point(position.X + 7, position.Y + 7), new Point(Form1.X +2, Form1.Y + 2));
        g.DrawLine(new Pen(Brushes.Navy), new Point(position.X + 8, position.Y + 8), new Point(Form1.X + 3, Form1.Y + 3));
        Thread.Sleep(200);
        return bitmap;
    }

	~SharpShooterTank()
	{
		
	}
}

public class UpdateImage
{
    public static Point MousePoint;
    public static bool MouseMoved = false;
    static Graphics g = null!;
    public static Bitmap updateImage(Bitmap bitmap, object caller, Coordinate cPosition, int length, int height)
    {     
        g = Graphics.FromImage(bitmap);
        Point oldPos;
        Point position;
        bool val = false;
        switch (caller)
        {
           case SharpShooterTank tank:
                position = new Point((int)cPosition.x, (int)cPosition.y);
                g.DrawRectangle(Pens.Black, position.X, position.Y, length, height);
                g.FillRectangle(Brushes.Black, position.X, position.Y, length, height);
                tank.UpdatePos(bitmap, length, height);
                g.DrawRectangle(Pens.White, position.X, position.Y, length, height);
                g.FillRectangle(Brushes.White, position.X, position.Y, length, height);
                break;
            case Projectile p:
                if (p.vall) { return bitmap; } // wat op dees aarde doen jy hier Alex, hierdie kry dit reg om alles te breek en ek weet nie hoekom nie 
                position = new Point((int)cPosition.x, (int)cPosition.y);
                g.FillRectangle(Brushes.Pink, position.X, position.Y, length, height);
                p.called = true;
                val = p.UpdatePos(bitmap, length, height);
                if (p.force.x == 0) 
                {      
                    return bitmap; 
                }
                // position.X = (int)(cPosition.x);
                //position.Y = (int)(cPosition.y);
                position = new Point((int)cPosition.x, (int)cPosition.y);   //Convert.ToInt32

                g.DrawRectangle(Pens.Red, position.X, position.Y, length, height);    //position
                g.FillRectangle(Brushes.Red, position.X, position.Y, length, height);
                break;
        }
        return bitmap;
        //MessageBox.Show(position.X.ToString(), position.Y.ToString());
        //MessageBox.Show(MovementForce.ToString());
        //return bitmap;
    }
    public static int LocalCoordsX = 3;
    public static int LocalCoordsY = 3;

    public static void ChangeMouseCoords(int XInput, int YInput, Bitmap bitmap, Point oldPos, Point position)
    {
        DrawCannon(bitmap, true, oldPos);  //oldPos   
        LocalCoordsX = XInput;
        LocalCoordsY = YInput;
        DrawCannon(bitmap, false, position);
    }
    //Hieronder sal jy bron vind van "nee andor jou mors van suurstof en spasie jou harlekyn van die dieretuin wat maak jy daai moet nie so werk nie

    protected static void DrawCannon(Bitmap bitmap, bool DeleteLine, Point position)
    {
        float CanonCentreX = position.X + 5;
        float CanonCentreY = position.Y + 5;
        int val = 1;
        float CanonMouseDiffX = (LocalCoordsX - CanonCentreX);
        float CanonMouseDiffY = (LocalCoordsY - CanonCentreY);
        if (CanonMouseDiffX < 0) val = -1;

        double canonLength = 60;
        double Difference = ((CanonMouseDiffY)) / ((CanonMouseDiffX));
        //MessageBox.Show(Difference.ToString());
        double angle = Math.Atan(Difference); //- Math.PI/2; /// Math.PI * 180;
        //MessageBox.Show(angle.ToString());
        double x = canonLength * Math.Cos(angle) * val;
        double y = canonLength * Math.Sin(angle) * val;
        MousePoint = new Point((int)(x + CanonCentreX), (int)(y + CanonCentreY));

        Graphics g = Graphics.FromImage(bitmap);

        if (DeleteLine)
        {
            g.DrawLine(new Pen(Brushes.Pink), CanonCentreX, CanonCentreY, (CanonCentreX + (float)x), (CanonCentreY + (float)y));
            return;
        }

        //nee andor jou stuk nonsens
        //Pen goldPen = new Pen(Color.Gold, 1);
        g.DrawLine(new Pen(Brushes.Gold), CanonCentreX, CanonCentreY, (CanonCentreX + (float)x), (CanonCentreY + (float)y));
    }
}
/*public class UpdateImage    //ou een
{
    public static Point MousePoint;
    public static bool MouseMoved = false;
    static Graphics g = null!;
    public static Bitmap updateImage(Bitmap bitmap, object caller, Coordinate cPosition)
    {
        g = Graphics.FromImage(bitmap);
        Point oldPos;
        bool val = false;
        switch (caller)
        {
            case SharpShooterTank tank:
                for (int i = 0; i < 6; i++)
                {
                    Point positionn = new Point((int)tank.cPosition.x, (int)tank.cPosition.y);
                    //g.DrawRectangle(Pens.Black, position.X, position.Y, 10, 10);
                    g.FillRectangle(Brushes.Black, positionn.X, positionn.Y, 10, 10);
                    oldPos = new Point(positionn.X, positionn.Y);

                    tank.UpdatePos(bitmap);
                    positionn = new Point((int)tank.cPosition.x, (int)tank.cPosition.y);
                    ChangeMouseCoords(Form1.X, Form1.Y, bitmap, oldPos, positionn);
                    MouseMoved = false;

                    //g.DrawRectangle(Pens.White, position.X, position.Y, 10, 10);
                    g.FillRectangle(Brushes.White, positionn.X, positionn.Y, 10, 10);
                }
                break;
            case Projectile p:
                if (p.vall) { MessageBox.Show("Kaas"); return bitmap; }
                Point position = new Point((int)cPosition.x, (int)cPosition.y);
                g.FillRectangle(Brushes.Black, position.X, position.Y, 10, 10);
                p.called = true;
                val = p.UpdatePos(bitmap);
                if (p.force.x == 0)
                {
                    //g = Graphics.FromImage(bitmap);
                    //g.FillEllipse(Brushes.Black, position.X - 8, position.X + 8, 30, 30);
                    //using (Graphics grD = Graphics.FromImage(Form1.t))
                    //{
                    //grD.DrawImage(bitmap, new Rectangle(0, 0, 4000, 497), new Rectangle(0, 0, 4000, 497), GraphicsUnit.Pixel);
                    //}             
                    return bitmap;
                }
                // position.X = (int)(cPosition.x);
                //position.Y = (int)(cPosition.y);
                position = new Point((int)cPosition.x, (int)cPosition.y);   //Convert.ToInt32
                g.FillRectangle(Brushes.Red, position.X, position.Y, 10, 10);
                break;
        }
        //MessageBox.Show(position.X.ToString(), position.Y.ToString());
        //MessageBox.Show(MovementForce.ToString());
        return bitmap;
    }
    public static int LocalCoordsX = 3;
    public static int LocalCoordsY = 3;

    public static void ChangeMouseCoords(int XInput, int YInput, Bitmap bitmap, Point oldPos, Point position)
    {
        DrawCannon(bitmap, true, oldPos);  //oldPos   
        LocalCoordsX = XInput;
        LocalCoordsY = YInput;
        DrawCannon(bitmap, false, position);
    }
    //Hieronder sal jy bron vind van "nee andor jou mors van suurstof en spasie jou harlekyn van die dieretuin wat maak jy daai moet nie so werk nie

    protected static void DrawCannon(Bitmap bitmap, bool DeleteLine, Point position)
    {
        float CanonCentreX = position.X + 5;
        float CanonCentreY = position.Y + 5;
        int val = 1;
        float CanonMouseDiffX = (LocalCoordsX - CanonCentreX);
        float CanonMouseDiffY = (LocalCoordsY - CanonCentreY);
        if (CanonMouseDiffX < 0) val = -1;

        double canonLength = 60;
        double Difference = ((CanonMouseDiffY)) / ((CanonMouseDiffX));
        //MessageBox.Show(Difference.ToString());
        double angle = Math.Atan(Difference); //- Math.PI/2; /// Math.PI * 180;
        //MessageBox.Show(angle.ToString());
        double x = canonLength * Math.Cos(angle) * val;
        double y = canonLength * Math.Sin(angle) * val;
        MousePoint = new Point((int)(x + CanonCentreX), (int)(y + CanonCentreY));

        Graphics g = Graphics.FromImage(bitmap);

        if (DeleteLine)
        {
            g.DrawLine(new Pen(Brushes.Black), CanonCentreX, CanonCentreY, (CanonCentreX + (float)x), (CanonCentreY + (float)y));
            return;
        }

        //nee andor jou stuk nonsens
        //Pen goldPen = new Pen(Color.Gold, 1);
        g.DrawLine(new Pen(Brushes.Gold), CanonCentreX, CanonCentreY, (CanonCentreX + (float)x), (CanonCentreY + (float)y));
    }

}*/
