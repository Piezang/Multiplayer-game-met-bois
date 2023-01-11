using System;

public class Projectile: Rigidbody
{
    int x; int y; //int mass; //Point inititalForce;
    public Projectile(int _x, int _y, int _mass, Point _inititalForce)
	{
        x = _x;
        y = _y;
        position= new Point(x,y);
        gravity = new Point(0,_mass);
        force = _inititalForce;
	}
    public Bitmap ImageChange(Bitmap bitmap)
    {
        bitmap = UpdateImage.updateImage(bitmap, position, this);
        return bitmap;
    }
    ~Projectile()
    {
        MessageBox.Show("Destroyed");
    }
}
