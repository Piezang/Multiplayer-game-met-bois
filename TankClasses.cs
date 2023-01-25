using Multiplayer_game_met_bois;
using System;
using System.Diagnostics.Contracts;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Text;
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
    private Point TerrainInteraction(Bitmap bmp, int length, int height)
    {
        Point CollisionAdjuster = new Point(1, 1);
        Point NW = new Point(position.X - 1, position.Y - 1);
        Point SW = new Point(position.X - 1, position.Y + height);
        Point NE = new Point(position.X + length, position.Y - 1);
        Point SE = new Point(position.X + length, position.Y + height);

        for (int i = SW.X; i <= SE.X; i++)
        {
            Color c = bmp.GetPixel(i, SW.Y);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.y + gravity.Y >= 0) { { CollisionAdjuster = new Point(1, 0); } } break;
            }
        }

        for (int i = NW.X; i <= NE.X; i++)
        {
            Color c = bmp.GetPixel(i, NW.Y);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.y + gravity.Y < 0) { { CollisionAdjuster = new Point(1, 0); } } break;
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
                        //if (Force.Y + gravity.Y < 0)
                        CollisionAdjuster = new Point(0, 1);
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
                        //if (Force.Y + gravity.Y < 0)
                        CollisionAdjuster = new Point(0, 1);
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

    double grav;
    int impactForce = 0;
    bool called = false;

    public void UpdatePos(Bitmap bitmap)    //
    {     
        grav += (mass * 0.03);
        if (gravity.Y < 8)
        {
            gravity = new Point(0,
                Convert.ToInt32(grav));
        }

        if (TerrainInteraction(bitmap, 10, 10).Y == 0) { gravity = new Point(0, 0); grav = 0; }
        /*for (int i = 0; i <= gravity.Y + mass; i++)   //1 of 0 by begin???
        {
            impactForce = (i) - (TerrainInteraction(bitmap).Y * i);

            position = new Point(position.X + TerrainInteraction(bitmap).X * (0 + force.X),
            position.Y + TerrainInteraction(bitmap).Y * (i + force.Y));       

            gravity = new Point(0, gravity.Y * TerrainInteraction(bitmap).Y);
            if (TerrainInteraction(bitmap).Y == 0) break;
        }*/
        cPosition = new Coordinate(cPosition.x + TerrainInteraction(bitmap, 10, 10).X * (force.x)
            , cPosition.y + TerrainInteraction(bitmap, 10, 10).Y * (grav + force.y));
        //if (TerrainInteraction(bitmap).Y == 0) break;
        //}
        //MessageBox.Show(force.ToString());
    }
}

class BaseTank : Rigidbody
{   
	private Coordinate newForce;
	public Coordinate MovementForce { get; set; }
    public Point MousePoint;
	float AimAngle;
    public int LocalCoordsX = 3;
    public int LocalCoordsY = 3;
	protected float CanonAngle
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
        float CanonCentreX = position.X + 5;
        float CanonCentreY = position.Y + 5;
        int val = 1;
        float CanonMouseDiffX = (LocalCoordsX - CanonCentreX);
        float CanonMouseDiffY = (LocalCoordsY - CanonCentreY);
        if (CanonMouseDiffX < 0) val = -1; 

        double canonLength = 60;
        double Difference = ((CanonMouseDiffY))/((CanonMouseDiffX));
        //MessageBox.Show(Difference.ToString());
        double angle = Math.Atan(Difference); //- Math.PI/2; /// Math.PI * 180;
        //MessageBox.Show(angle.ToString());
        double x = canonLength * Math.Cos(angle) * val; 
        double y = canonLength * Math.Sin(angle) * val;
        MousePoint = new Point((int)(x + CanonCentreX), (int)(y + CanonCentreY));

        Graphics g = Graphics.FromImage(bitmap);
        
        if (DeleteLine)
        {
            g.DrawLine(new Pen(Brushes.Black), CanonCentreX, CanonCentreY, (CanonCentreX + (float)x) , (CanonCentreY + (float)y) );
            return;
        }
        
        //nee andor jou stuk nonsens
        //Pen goldPen = new Pen(Color.Gold, 1);
        g.DrawLine(new Pen(Brushes.Gold),CanonCentreX,CanonCentreY, (CanonCentreX + (float)x), (CanonCentreY + (float)y));
    }

    protected int TakeDamage(int damage, int health)
    {
        health -= damage;
        if (health <= 0)
        {
            return 0; //was -100
        }
        return health;
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
}

class SharpShooterTank : BaseTank
{
    Image SharpShooterTankimg = null!;  //Image.FromFile("image.png");  
    Bitmap bitmap = new Bitmap(883, 497);
    Graphics g; 

    private int health = 100;
    public bool destroyed = false;
    public Point[] PixelCoords = new Point[100];
    public Color[] PixelColor = new Color[100];

    public SharpShooterTank(Point pos, int _mass, Coordinate frce, float angl) 
	{ 
		position = pos; gravity = new Point(0, _mass * 1);
		force = frce;   mass = _mass;
		CanonAngle = angl; cPosition = new Coordinate(position.X, position.Y);

        //bitmap = new Bitmap(SharpShooterTankimg);
        int count = 0;
        for (int x = pos.X; x < pos.X + 10; x++)
        {
            for (int y = pos.Y; y < pos.Y + 10; y++)
            {
                PixelCoords[count] = new Point(x, y);
                PixelColor[count] = Color.Black;  
                count++;
            }
        }

        g = Graphics.FromImage(bitmap);
       // g.DrawRectangle(Pens.White, pos.X, pos.Y, 10, 10);
        g.FillRectangle(Brushes.White, pos.X, pos.Y, 10, 10);
        //Start();

    }
    public void Damage(int damage)
    {
        health = TakeDamage(damage, health);
        if (health <= 0) Destroy(); //was -100
        
        
    }
   
    //private void GiveDamage(object sender, KeyPressEventArgs)

	private void Destroy()
	{
        //SharpShooterTankimg.Dispose(); //Ek wil he dit moet clear
        MessageBox.Show("Its destroyed (Source: trust me bro)");
        destroyed = true;
       
    }
    public bool MouseMoved = false;
    Point oldPos;
    public Bitmap UpdateImage(Bitmap bitmap)
    {
        for (int i = 0; i < 6; i++)
        {
            position = new Point((int)cPosition.x, (int)cPosition.y);
            g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.Black, position.X, position.Y, 10, 10);

            //Wrywing
            //MovementForce = new Point((int)(MovementForce.X * 0.9), (int)(MovementForce.Y * 0.9));
            oldPos = new Point (position.X, position.Y);
   
            UpdatePos(bitmap);
            position = new Point((int)cPosition.x, (int)cPosition.y);
            ChangeMouseCoords(Form1.X, Form1.Y, bitmap, oldPos);
            MouseMoved = false;

            //PixelCoords = GetPixelCoords(position, 10, 10);
            //PixelColor = GetPixelColor(PixelCoords, bitmap);

            g.FillRectangle(Brushes.White, position.X, position.Y, 10, 10);
            //MessageBox.Show(position.X.ToString(), position.Y.ToString());
            //MessageBox.Show(MovementForce.ToString());
        }
        return bitmap;
    }

    public Bitmap ShootUlt(Bitmap bitmap)
    {
        Graphics g = Graphics.FromImage(bitmap);
        g.DrawLine(new Pen(Brushes.DarkBlue),position, new Point(Form1.X+1, Form1.Y));
        g.DrawLine(new Pen(Brushes.LightBlue), position, new Point(Form1.X, Form1.Y+1));
        g.DrawLine(new Pen(Brushes.White), position, new Point(Form1.X, Form1.Y));
        g.DrawLine(new Pen(Brushes.LightBlue), position, new Point(Form1.X-1, Form1.Y));
        g.DrawLine(new Pen(Brushes.DarkBlue), position, new Point(Form1.X, Form1.Y-1));
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
    public static Bitmap updateImage(Bitmap bitmap, object caller, Coordinate cPosition)
    {     
        g = Graphics.FromImage(bitmap);
        Point oldPos;
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
                Point position = new Point((int)cPosition.x, (int)cPosition.y);
                g.FillRectangle(Brushes.Black, position.X, position.Y, 10, 10);
                p.UpdatePos(bitmap);

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

}


