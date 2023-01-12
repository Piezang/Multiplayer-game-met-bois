using System;
using System.Diagnostics.Contracts;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Text;

public class Rigidbody
{
	public Point position { get; set; }
	protected Point gravity { get; set; }
	protected Point force { get; set; }
    protected double mass;

    public Bitmap bmp;

    public int gravityTimer;

    public Rigidbody()
	{
        gravityTimer = 0;
        grav = 0;
        //mass = gravity.Y;
    }

    public String Direction;
    private Point TerrainInteraction(Bitmap bmp)
    {
        Boolean bNorth = true;
        Boolean bSouth = true;
        Boolean bEast = true;
        Boolean bWest = true;
        Point CollisionAdjuster = new Point(1,1);
        Point NW = new Point(position.X - 1, position.Y - 1);
        Point SW = new Point(position.X - 1, position.Y + 11);
        Point NE = new Point(position.X + 11, position.Y - 1);
        Point SE = new Point(position.X + 11, position.Y + 11);

        for (int i = SW.X; i<= SE.X; i ++)
        {
            Color c = bmp.GetPixel(i, SW.Y);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.Y + gravity.Y >= 0) { { CollisionAdjuster = new Point(1, 0); bSouth = false; } }break;
            }
        }

        for (int i = NW.X; i <= NE.X; i++)
        {
            Color c = bmp.GetPixel(i, NW.Y);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.Y + gravity.Y < 0) { { CollisionAdjuster = new Point(1, 0); bNorth = false; } }break;
            }
        }

        for (int i = NE.Y + 1; i <= SE.Y - 1; i++)
        {
            Color c = bmp.GetPixel(NE.X, i);
            switch (c.ToString())
            {
                case "Color [A=255, R=139, G=69, B=19]":
                case "Color [A=255, R=0, G=100, B=0]":
                    if (force.X >= 0)
                    {
                        if (force.Y + gravity.Y < 0)
                        { CollisionAdjuster = new Point(0, 1); }
                        else { CollisionAdjuster = new Point(0, 0); }
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
                    if (force.X <= 0)
                    { 
                        if (force.Y + gravity.Y < 0)
                        { CollisionAdjuster = new Point(0, 1); }
                        else { CollisionAdjuster = new Point(0, 0); }
                    }
                    break;
            }
        }

        return CollisionAdjuster;
       // CollisionAdjuster = new Point(0, 0);
    }

    protected int LocalCoordsX;
    protected int LocalCoordsY;
    double grav;
    int impactForce = 0;

    public void UpdatePos(Bitmap bitmap)    //
    {
        grav += (mass * 0.03);
        if (gravity.Y < 8)
        {
            gravity = new Point(0,
                Convert.ToInt32(grav));
        }

        if (TerrainInteraction(bitmap).Y == 0) { gravity = new Point(0, 0); grav = 0; } 
        for (int i = 1; i <= gravity.Y + mass; i++)
        {
            impactForce = (i) - (TerrainInteraction(bitmap).Y * i);

            position = new Point(position.X + TerrainInteraction(bitmap).X * (0 + force.X),
            position.Y + TerrainInteraction(bitmap).Y * (i + force.Y));
            gravity = new Point(0, gravity.Y * TerrainInteraction(bitmap).Y);
            if (TerrainInteraction(bitmap).Y == 0) break;
        }
    
        if (LocalCoordsX != 0)
        {
            //MessageBox.Show(LocalCoordsX.ToString());
        }

        if (LocalCoordsY != 0)
        {
            //MessageBox.Show(LocalCoordsY.ToString());
        }
    }
}

class BaseTank : Rigidbody
{   
	private Point newForce;
	public Point MovementForce { get; set; }
	float AimAngle;
	protected float CanonAngle
	{
		get { return AimAngle; }
		set 
		{
            if ((value >= 0) && (value < 360))
            AimAngle = value; 
		}
	}

    public void ChangeMouseCoords(int XInput, int YInput)
    {
        LocalCoordsX = XInput;
        LocalCoordsY = YInput;       
    }

    /*public void Form1_keyPress(object sender, KeyPressEventArgs e)
    {
		MessageBox.Show(e.KeyChar.ToString());
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
    }*/

    protected int TakeDamage(int damage, int health)
    {
        health -= damage;
        if (health <= 0)
        {
            return -100;
        }
        return health;
    }
    
    public void Move(char c)
    {
        Direction = c.ToString().ToUpper();
        switch (Direction)
        {
            case "W": newForce = new Point(0, -1); //MessageBox.Show(c.ToString());
                break;
            case "S": newForce = new Point(0, 1); //MessageBox.Show(c.ToString());
                break;
            case "A": newForce = new Point(-1, 0); //MessageBox.Show(c.ToString());
                break;
            case "D": newForce = new Point(1, 0); //MessageBox.Show(c.ToString());
                break;
            default : return;
        }
        MovementForce = new Point( MovementForce.X + newForce.X,
            MovementForce.Y + newForce.Y);
        force = MovementForce;  //UpdatePos();
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

    public SharpShooterTank(Point pos, int _mass, Point frce, float angl) 
	{ 
		position = pos; gravity = new Point(0, _mass * 1);
		force = frce;   mass = _mass;
		CanonAngle = angl;

        //bitmap = new Bitmap(SharpShooterTankimg); 
        g = Graphics.FromImage(bitmap);
        g.DrawRectangle(Pens.White, pos.X, pos.Y, 10, 10);
        g.FillRectangle(Brushes.White, pos.X, pos.Y, 10, 10);
        //Start();
    }
    public void Damage(int damage)
    {
        health = TakeDamage(damage, health);
        if (health == -100) Destroy();
    }
   
	private void Destroy()
	{
		SharpShooterTankimg.Dispose();   //Ek wil he dit moet clear
        destroyed = true;
    }

    public Bitmap UpdateImage(Bitmap bitmap)
    {
        for (int i = 0; i < 6; i++)
        {
            g = Graphics.FromImage(bitmap);
            g.DrawRectangle(Pens.Black, position.X, position.Y, 10, 10);
            g.FillRectangle(Brushes.Black, position.X, position.Y, 10, 10);
            //Wrywing
            //MovementForce = new Point((int)(MovementForce.X * 0.9), (int)(MovementForce.Y * 0.9));
            UpdatePos(bitmap);
            g.DrawRectangle(Pens.White, position.X, position.Y, 10, 10);
            g.FillRectangle(Brushes.White, position.X, position.Y, 10, 10);
            //MessageBox.Show(position.X.ToString(), position.Y.ToString());
            //MessageBox.Show(MovementForce.ToString());
        }
        return bitmap;
    }

	~SharpShooterTank()
	{
		
	}
}

public class UpdateImage
{
    static Graphics g = null!;
    public static Bitmap updateImage(Bitmap bitmap, Point position, object caller)
    {     
        g = Graphics.FromImage(bitmap);       
       
        switch (caller)
        {
            case SharpShooterTank tank:
                g.DrawRectangle(Pens.Black, position.X, position.Y, 10, 10);
                g.FillRectangle(Brushes.Black, position.X, position.Y, 10, 10);
                tank.UpdatePos(bitmap);
                g.DrawRectangle(Pens.White, position.X, position.Y, 10, 10);
                g.FillRectangle(Brushes.White, position.X, position.Y, 10, 10);
                break;
            case Projectile p:
                //MessageBox.Show(p.position.ToString());
                //g.DrawRectangle(Pens.Black, position.X, position.Y, 10, 10);
                //g.FillRectangle(Brushes.Black, position.X, position.Y, 10, 10);
                p.UpdatePos(bitmap);
                g.DrawRectangle(Pens.Red, position.X, position.Y, 10, 10);
                g.FillRectangle(Brushes.Red, position.X, position.Y, 10, 10);
                break;
        }      
        //MessageBox.Show(position.X.ToString(), position.Y.ToString());
        //MessageBox.Show(MovementForce.ToString());
        return bitmap;
    }
}


