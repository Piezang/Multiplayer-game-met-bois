using System;
using System.Diagnostics.Contracts;
using System.Drawing;

class Rigidbody
{
	protected Point position { get; set; }
	protected Point gravity { get; set; }
	protected Point force { get; set; }

	public Rigidbody()
	{

	}

	protected void UpdatePos()
	{
        position = new Point(position.X + gravity.X + force.X,
            position.Y + gravity.Y + force.Y);
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
        
        switch (c)
        {
            case 'w': newForce = new Point(0, -3); //MessageBox.Show(c.ToString());
                break;
            case 's': newForce = new Point(0, 3); //MessageBox.Show(c.ToString());
                break;
            case 'a': newForce = new Point(-3, 0); //MessageBox.Show(c.ToString());
                break;
            case 'd': newForce = new Point(3, 0); //MessageBox.Show(c.ToString());
                break;
            default : return;
        }
        MovementForce = new Point(MovementForce.X + newForce.X,
           MovementForce.Y + newForce.Y);
        force = MovementForce;  UpdatePos();
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
    Bitmap bitmap = new Bitmap(4000, 4000);
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

    public Bitmap UpdateImage()
    {
        g = Graphics.FromImage(bitmap);
        g.Clear(Color.Black);
        //Wrywing
        //MovementForce = new Point((int)(MovementForce.X * 0.9), (int)(MovementForce.Y * 0.9));
        UpdatePos();
        g.DrawRectangle(Pens.White, position.X, position.Y, 10, 10);
        g.FillRectangle(Brushes.White, position.X, position.Y, 10, 10);
        return bitmap;
    }

	~SharpShooterTank()
	{
		
	}
}
