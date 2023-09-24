using System;
using System.CodeDom;
using System.Runtime.InteropServices;

public class Projectile: Rigidbody
{
    public Image image = null!;
    int x; int y; //int mass; //Point inititalForce;
    public bool Destroyed = false;
    public bool NewP = true;
    public int plength;
    public int pheight;
    public int damage { get; set; }
    public Projectile(int _x, int _y, double _mass, Coordinate _inititalForce, int _damage)
	{
        x = _x;
        y = _y;
        cPosition = new Coordinate(x, y);
        Position= new Point(x,y); mass= _mass;
        gravity = new Point(0,Convert.ToInt32(_mass));
        force = _inititalForce; damage= _damage;
	}
    public Bitmap ImageChange(Bitmap bitmap, int length, int height)
    {
        if (Destroyed) return null!;                    
        bitmap = UpdateImage.updateImage(bitmap, null!, new Size(0,0), this, cPosition, length, height); // ignoreer die size dis net om die plek te full, dis nie relavant vir projectiles nie, net by die tank.
        plength = length;
        pheight = height;
        return bitmap;
    }
    ~Projectile()
    {
        //MessageBox.Show("Destroyed");
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
    public static Coordinate operator +(Coordinate a, Coordinate b)
    {    
        return new Coordinate(a.x + b.x, b.y + a.y);
    }
    public static Coordinate operator *(Coordinate a, double b)
    {
        return new Coordinate(a.x * b, a.y * b);
    }
}

public static class GraphicsHelper
{
    public static void GdiDrawImage(this Graphics graphics, Bitmap image, Rectangle rectangleDst, int nXSrc, int nYSrc, int nWidth, int nHeight)
    {
        IntPtr hdc = graphics.GetHdc();
        IntPtr memdc = GdiInterop.CreateCompatibleDC(hdc);
        IntPtr bmp = image.GetHbitmap();
        GdiInterop.SelectObject(memdc, bmp);
        GdiInterop.SetStretchBltMode(hdc, 0x04);
        GdiInterop.StretchBlt(hdc, rectangleDst.Left, rectangleDst.Top, rectangleDst.Width, rectangleDst.Height, memdc, nXSrc, nYSrc, nWidth, nHeight, GdiInterop.TernaryRasterOperations.SRCCOPY);
        //GdiInterop.BitBlt(..) put it here, if you did not mention stretching the source image
        GdiInterop.DeleteObject(bmp);
        GdiInterop.DeleteDC(memdc);
        graphics.ReleaseHdc(hdc);
    }
}

public class GdiInterop
{
    /// <summary>
    /// Enumeration for the raster operations used in BitBlt.
    /// In C++ these are actually #define. But to use these
    /// constants with C#, a new enumeration _type is defined.
    /// </summary>
    public enum TernaryRasterOperations
    {
        SRCCOPY = 0x00CC0020, // dest = source
        SRCPAINT = 0x00EE0086, // dest = source OR dest
        SRCAND = 0x008800C6, // dest = source AND dest
        SRCINVERT = 0x00660046, // dest = source XOR dest
        SRCERASE = 0x00440328, // dest = source AND (NOT dest)
        NOTSRCCOPY = 0x00330008, // dest = (NOT source)
        NOTSRCERASE = 0x001100A6, // dest = (NOT src) AND (NOT dest)
        MERGECOPY = 0x00C000CA, // dest = (source AND pattern)
        MERGEPAINT = 0x00BB0226, // dest = (NOT source) OR dest
        PATCOPY = 0x00F00021, // dest = pattern
        PATPAINT = 0x00FB0A09, // dest = DPSnoo
        PATINVERT = 0x005A0049, // dest = pattern XOR dest
        DSTINVERT = 0x00550009, // dest = (NOT dest)
        BLACKNESS = 0x7FFFFFFF, // dest = BLACK  //0x00000042
        WHITENESS = 0x00000062, // dest = WHITE
    };

    /// <summary>
    /// Enumeration to be used for those Win32 function 
    /// that return BOOL
    /// </summary>
    public enum Bool
    {
        False = 0,
        True
    };

    /// <summary>
    /// Sets the background color.
    /// </summary>
    /// <param name="hdc">The HDC.</param>
    /// <param name="crColor">Color of the cr.</param>
    /// <returns></returns>
    [DllImport("gdi32.dll")]
    public static extern int SetBkColor(IntPtr hdc, int crColor);  //crColor

    /// <summary>
    /// CreateCompatibleDC
    /// </summary>
    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    /// <summary>
    /// DeleteDC
    /// </summary>
    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern Bool DeleteDC(IntPtr hdc);

    /// <summary>
    /// SelectObject
    /// </summary>
    [DllImport("gdi32.dll", ExactSpelling = true)]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    /// <summary>
    /// DeleteObject
    /// </summary>
    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern Bool DeleteObject(IntPtr hObject);

    /// <summary>
    /// CreateCompatibleBitmap
    /// </summary>
    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hObject, int width, int height);

    /// <summary>
    /// BitBlt
    /// </summary>
    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern Bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

    /// <summary>
    /// StretchBlt
    /// </summary>
    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern Bool StretchBlt(IntPtr hObject, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hObjSource, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, TernaryRasterOperations dwRop);

    /// <summary>
    /// SetStretchBltMode
    /// </summary>
    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern Bool SetStretchBltMode(IntPtr hObject, int nStretchMode);
}