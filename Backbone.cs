using Multiplayer_game_met_bois;
using System;
using System.Diagnostics;

public abstract class Backbone1
{    
    protected static int iterations = 0;
    public Backbone1() 
    {
        Thread start = new Thread(asyncVoorbeeld);
        start.Start();
    }
    //static Backbone1()
	//{
        //Backbone b = new Backbone();
        //Backbone1.asyncVoorbeeld();
	//}
    //abstract protected void TrueFixedUpdate();
    async void asyncVoorbeeld()
    {
        while (true)
        {   
           await FixedUpdate();
        }
    }

    async Task FixedUpdate()
    {
        await Task.Delay(14);  //Baie weird moet eintlik 17 wees maar ok
        //TrueFixedUpdate();
        iterations++;
    }

    public abstract void TrueFixedUpdate(); //Word tussen 61 en 63 keer per sekonde gecall
}

public class FormKaas
{
    private Form1 _form;
    protected int count = 0;
    Stopwatch timerr = new Stopwatch();
    Thread previousThread;
    public FormKaas(Form1 form)
    {
        _form = form;
        previousThread = Thread.CurrentThread;
        Thread diekaas = new Thread(Kaas);
        diekaas.Start();
    }
    void Kaas()
    {
        timerr.Start();
        Stopwatch stopwatch = new Stopwatch();
        //float kaas = 0;
        // Set the fixed delta time to 1/60 of a second (60 fps)
        TimeSpan sixtieth = TimeSpan.FromSeconds(1.0 / 60.0);
        while (true)
        {
            stopwatch.Start();
            // Update game logic here
            FixedUpdate(_form);
            //Backbone1.

            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            //MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString());
            stopwatch.Reset();
            
            //kaas += elapsed.Milliseconds;

            // Sleep for the remaining time to reach the fixed delta time
            if (elapsed < sixtieth)
            {
                Thread.Sleep(sixtieth - elapsed);
                //kaas += (int)sixtieth.TotalMilliseconds;
            }
            //if (timerr.ElapsedMilliseconds >= 1000f)
            //{
                //timerr.Stop();
                //MessageBox.Show(count.ToString());
                 //MessageBox.Show(timerr.ElapsedMilliseconds.ToString());
                //break;
            //}
        }
    } 

    int IterationCount = 0;
    Stopwatch timer = new Stopwatch();
    /*public void begin()
    {
        timer.Start();
        while (true)
        {
            //Thread instance = new Thread(TrueFixedUpdate);
            if ((int)timer.ElapsedMilliseconds >= 1000)
            {
                timer.Stop();
                break;
            }
        }
        MessageBox.Show(count.ToString());
    }*/
    //public override void TrueFixedUpdate()
    //{
        //if (iterations == IterationCount + 1)
        //{
            //IterationCount++;
            //count++;
        //}   
        //throw new NotImplementedException();
    //}
    protected virtual void FixedUpdate(Form1 form)
    {
        //count++;
    }
}
