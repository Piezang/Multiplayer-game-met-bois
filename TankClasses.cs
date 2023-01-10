using System;
using System.Diagnostics.Contracts;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Text;

class Rigidbody
{
	public Point position { get; set; }
	protected Point gravity { get; set; }
	protected Point force { get; set; }

    public Point CollisionAdjust = new Point(0,0);

    public Rigidbody()
	{

	}
    public Point TerrainInteraction()
    {
        Point NW = new Point(position.X - 1, position.Y - 1);
        Point SW = new Point(position.X - 1, position.Y + 10);
        Point NE = new Point(position.X + 10, position.Y - 1);
        Point SE = new Point(position.X + 10, position.Y + 10);

        return new Point(0,1);
    }

    protected int LocalCoordsX;
    protected int LocalCoordsY;

	protected void UpdatePos()
	{
        position = new Point(position.X + gravity.X + force.X,
            position.Y + gravity.Y + force.Y);

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
        string Direction = c.ToString().ToUpper();
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
        MovementForce = new Point(MovementForce.X + newForce.X + TerrainInteraction().X,
          MovementForce.Y + newForce.Y + TerrainInteraction().Y);
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

    public SharpShooterTank(Point pos, int mass, Point frce, float angl) 
	{ 
		position = pos; gravity = new Point(0, mass * 1);
		force = frce; 
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
        g = Graphics.FromImage(bitmap);
        g.DrawRectangle(Pens.Black, position.X, position.Y, 10, 10);
        g.FillRectangle(Brushes.Black, position.X, position.Y, 10, 10);
        //Wrywing
        //MovementForce = new Point((int)(MovementForce.X * 0.9), (int)(MovementForce.Y * 0.9));
        UpdatePos();
        g.DrawRectangle(Pens.White, position.X, position.Y, 10, 10);
        g.FillRectangle(Brushes.White, position.X, position.Y, 10, 10);
        //MessageBox.Show(position.X.ToString(), position.Y.ToString());
        //MessageBox.Show(MovementForce.ToString());
        return bitmap;
    }

	~SharpShooterTank()
	{
		
	}
}


