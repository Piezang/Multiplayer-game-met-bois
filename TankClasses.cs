﻿using System;
using System.Diagnostics.Contracts;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Text;

class Rigidbody
{
	public Point position { get; set; }
	protected Point gravity { get; set; }
	protected Point force { get; set; }

    public Bitmap bmp;

    public Rigidbody()
	{

	}

    public String Direction;
    public Point TerrainInteraction()
    {
        Point CollisionAdjuster = new Point(1,1);
        Point NW = new Point(position.X - 1, position.Y - 1);
        Point SW = new Point(position.X - 1, position.Y + 11);
        Point NE = new Point(position.X + 11, position.Y - 1);
        Point SE = new Point(position.X + 11, position.Y + 11);

        for (int i = SW.X; i<= SE.X; i ++)
        {
            Color c = bmp.GetPixel(i, SW.Y);
            if (c.ToString() == "Color [A=255, R=139, G=69, B=19]")
            { if (force.Y >= 0) { { CollisionAdjuster.Y = 0; } } }
        }

        for (int i = NW.X; i <= NE.X; i++)
        {
            Color c = bmp.GetPixel(i, NW.Y);
            if (c.ToString() == "Color [A=255, R=139, G=69, B=19]")
            { if (force.Y <= 0) { if (Direction == "W") { CollisionAdjuster.Y = 0; } } }

        }

        for (int i = NE.Y + 1; i <= SE.Y - 1; i++)
        {
            Color c = bmp.GetPixel(NE.X, i);
            if (c.ToString() == "Color [A=255, R=139, G=69, B=19]")
            { if (force.X >= 0) { if (Direction == "D") { if (gravity.Y - CollisionAdjuster.Y == 0) { CollisionAdjuster = new Point(0, -1); } } } }

        }

        for (int i = NW.Y + 1; i <= SW.Y - 1; i++)
        {
            Color c = bmp.GetPixel(NW.X, i);
            if (c.ToString() == "Color [A=255, R=139, G=69, B=19]")
            { if (force.X <= 0) { if (Direction == "A") { CollisionAdjuster = new Point(0,- 1); } } }
        }

        return CollisionAdjuster;
       // CollisionAdjuster = new Point(0, 0);
    }

    protected int LocalCoordsX;
    protected int LocalCoordsY;

	protected void UpdatePos()
	{
        position = new Point(position.X + TerrainInteraction().X * (gravity.X + force.X),
            position.Y + TerrainInteraction().Y * (gravity.Y + force.Y));

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
        MovementForce = new Point(MovementForce.X + newForce.X,
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

    public SharpShooterTank(Point pos, int mass, Point frce, float angl) 
	{ 
		position = pos; gravity = new Point(0, 1 * 1);
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
        bmp = bitmap;
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


