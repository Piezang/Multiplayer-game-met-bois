using System;

public class Projectile: Rigidbody
{
    int x; int y; //int mass; //Point inititalForce;
    public Projectile(int _x, int _y, double _mass, Point _inititalForce)
	{
        x = _x;
        y = _y;
        cPosition = new Coordinate(x, y);
        position= new Point(x,y); mass= _mass;
        gravity = new Point(0,Convert.ToInt32(_mass));
        force = _inititalForce;
	}
    public Bitmap ImageChange(Bitmap bitmap)
    {
        bitmap = UpdateImage.updateImage(bitmap, position, this, cPosition);
        return bitmap;
    }
    ~Projectile()
    {
        MessageBox.Show("Destroyed");
    }
}

public struct Coordinate
{
    public double x;
    public double y;
    public Coordinate(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}
