using System.Collections;
using yazlab3;
using yazlab3proje;

Queue<Customer> customers = new Queue<Customer>();
Queue<Waiter> waiters= new Queue<Waiter>();
List<Table> tables= new List<Table>();

Console.WriteLine("[1]-Asama 1");
Console.WriteLine("[2]-Asama 2");

int secim = Convert.ToInt32(Console.ReadLine());

if (secim == 1)
{
    secim1();

}
else if (secim == 2)
{
    secim2();
}
void secim1()
{
    for(int i=0; i < 6; i++)
    {
        Table table= new Table();
        tables.Add(table);
    }
    Console.WriteLine("Adim sayisi girin");
    int adim = Convert.ToInt32(Console.ReadLine());

    for(int i = 0; i < 2; i++)
    {
        Waiter waiter = new Waiter();
        waiters.Enqueue(waiter);
    }

    for (int i = 0; i < adim; i++)
    {
        Console.WriteLine("Musteri sayisi girin");
        int mus = Convert.ToInt32(Console.ReadLine());
        int oncelikli = Convert.ToInt32(Console.ReadLine());
        MusteriOlustur(mus, oncelikli);
    }
    while (customers.Count > 0)
    {
        MusteriOturmasi();
        SiparisAlma();
        MusteriKalkmasi();
    }
    
}
void secim2()
{

}

void MusteriOlustur(int musteri, int oncelikli)
{
    for (int i = 0; i < oncelikli; i++)
    {
        Customer customer = new Customer(true);
        customers.Enqueue(customer);
    }
    for (int i = 0; i < musteri; i++)
    {
        Customer customer = new Customer(false);
        customers.Enqueue(customer);
    }
}

void MusteriOturmasi()
{
    foreach(Customer customer in customers)
    {
        foreach (Table table in tables)
        {
            if (!customer.atDesk)
            {
                if(table.isEmpty)
                {
                    Console.WriteLine(customer.name+" "+table.name+"e oturdu");
                    table.isEmpty = false;
                    customer.TableName= table.name;
                    customer.atDesk= true;
                }
            }
        }
    }
}


void MusteriKalkmasi()
{
    Customer customer=customers.Peek();
    if (customer.atDesk)
    {
        customer=customers.Dequeue();
        Thread.Sleep(3000);
        Console.WriteLine(customer.name + " " + customer.TableName + "den kalkti");
        customer.atDesk = false;
    }
   
    foreach (Table table in tables)
    {
            if(customer.TableName==table.name)
            {
            table.isEmpty = true;
            }
    }
    
}

void SiparisAlma()
{
    if (waiters.Count>0)
    {
        Waiter waiter = waiters.Dequeue();

        if (waiter.isAvaliable)
        {
            Customer customer = customers.Dequeue();
            if (customer.atDesk && customer.canOrder)
            {
                waiter.isAvaliable = false;
                customer.canOrder = false;
                waiter.SetThread(customer);
                waiter.thread.Start();
            }
            else
            {
                customers.Enqueue(customer);
            }
        }
        else
        {
            waiters.Enqueue(waiter);
        }
    }
}


void AsciyaIlet()
{

}