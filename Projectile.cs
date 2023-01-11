using System;

public class Projectile: Rigidbody
{
    int x; int y; //int mass; //Point inititalForce;
    public Projectile(int _x, int _y, double _mass, Point _inititalForce)
	{
        x = _x;
        y = _y;
        position= new Point(x,y); mass= _mass;
        gravity = new Point(0,Convert.ToInt32(_mass));
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
