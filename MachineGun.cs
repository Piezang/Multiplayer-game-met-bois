using Microsoft.VisualBasic.Devices;
using System;
class MachineGun: BaseTank
{
    Projectile bulletType = new Projectile(0,0,1, new Coordinate(0,0), 7);
    public Image MachineGunTankimg = Image.FromFile("image.png");
    Bitmap MachineGunTankbmp = new Bitmap(100, 50);  //Image.FromFile("image.png");  
    Graphics g;
    Graphics tank;

    public static readonly int MaxAmmo = 120;
    private int health = 150;
    public bool destroyed = false;

    public MachineGun(Point pos, int _mass, Coordinate frce)//
    {
        Position = pos; base.gravity = new Point(0, _mass);
        force = frce; mass = _mass;
        cPosition = new Coordinate(Position.X, Position.Y);
    }

    public Bitmap ImageChange(Bitmap bitmap, int length, int height)
    {
        //if (Destroyed) return null!;
        bitmap = UpdateImage.updateImage(bitmap, null!, new Size(0, 0), this, cPosition, length, height); // ignoreer die size dis net om die plek te full, dis nie relavant vir projectiles nie, net by die tank.
        //plength = length;
        //pheight = height;
        return bitmap;
    }

    public void Damage(int damage)
    {
        health = TakeDamage(damage, health, 100);
        if (health <= 0) Destroy();    
    }
    private void Destroy()
    {
        MessageBox.Show("Tank is Destroyed");
    }
    ~MachineGun(){

    }
}
