
using System;
using System.Threading;

public class BoundedBuffer
{
    private static final int BUFFER_SIZE=5;
    private Object[]buffer;
    private int in, out ;
    private Semaphore mutex;
    private Semaphore empty;
    private Semaphore full;

    public BoundedBuffer(){
        // buffer is initially empty
        in = 0;
        out = 0;
        buffer = new Object[BUFFER_SIZE];

        mutex = new Semaphore(1);
        empty = new Semaphore(BUFFER_SIZE);
        full = new Semaphore(0);
    }
    public void insert(Object item){
        empty.acquire( ) ;
        mutex.acquire();
        buffer[in] = item;
        in = (in+1) % BUFFER_SIZE;
        mutex.release();
        full.release()
    }
    public Object remove(){
        full.acquire();
        mutex.acquire();
        item = buffer[out];
        out = (out+1) % BUFFER_SIZE;
        mutex.release();
        empty.release() ;
        return item;
    }
}


class MyProducer
{
    private Random rand = new Random(1);
    private BoundedBuffer boundedBuffer;
    private int totalIters;

    public MyProducer(BoundedBuffer boundedBuffer, int iterations)
    {
        this.boundedBuffer = boundedBuffer;
        totalIters = iterations;
    }

    public Thread CreateProducerThread()
    {
        return new Thread(new ThreadStart(this.calculate));
    }
    private void calculate()
    {
        int iters = 0;
        do
        {
            iters += 1;
            Thread.Sleep(rand.Next(2000));
            boundedBuffer.WriteData(iters * 4);
        } while (iters < totalIters);
    }
}

class MyConsumer
{
    private Random rand = new Random(2);
    private BoundedBuffer boundedBuffer;
    private int totalIters;


    public MyConsumer(BoundedBuffer boundedBuffer, int iterations)
    {
        this.boundedBuffer = boundedBuffer;
        totalIters = iterations;
    }

    public Thread CreateConsumerThread()
    {
        return new Thread(new ThreadStart(this.printValues));
    }

    private void printValues()
    {
        int iters = 0;
        double pi;
        do
        {
            Thread.Sleep(rand.Next(2000));
            boundedBuffer.ReadData(out pi);
            System.Console.WriteLine("Value {0} is: {1}", iters, pi.ToString());
            iters++;
        } while (iters < totalIters);
        System.Console.WriteLine("Done");
    }
}

class MainClass
{
    static void Main(string[] args)
    {
        BoundedBuffer boundedBuffer = new BoundedBuffer();

        MyProducer prod = new MyProducer(boundedBuffer, 20);
        Thread producerThread = prod.CreateProducerThread();

        MyConsumer cons = new MyConsumer(boundedBuffer, 20);
        Thread consumerThread = cons.CreateConsumerThread();

        producerThread.Start();
        consumerThread.Start();

        Console.ReadLine();
    }
}


