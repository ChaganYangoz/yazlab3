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
    Console.Write("Müşteri sayısını girin: ");
    int musteriSayisi = Convert.ToInt32(Console.ReadLine());

    Console.Write("Müşteri ayrılma oranını girin: ");
    int ayrilanMusteri = Convert.ToInt32(Console.ReadLine());

    Console.Write("Restoranın kaç saniye çalışacağını girin: ");
    int calismaSuresi = Convert.ToInt32(Console.ReadLine());

    // Süreler
    int yemekSuresi = 3;
    int siparisAlmaSuresi = 2;
    int yemeSuresi = 3;
    int odemeSuresi = 1;
    int maksimumBeklemeSuresi = 20;

    // Başlangıç değerleri
    int enIyiKazanc = 0;
    int enIyiMasalar = 0;
    int enIyiGarsonlar = 0;
    int enIyiAscilar = 0;

    for (int masalar = 1; masalar <= musteriSayisi; masalar++)
    {
        for (int garsonlar = 1; garsonlar <= musteriSayisi; garsonlar++)
        {
            for (int ascilar = 1; ascilar <= musteriSayisi; ascilar++)
            {
                int toplamKazanc = 0;
                for (int i = 0; i < musteriSayisi; i++)
                {
                    if (i % ayrilanMusteri == 0 && i != 0)
                    {
                        // Müşteri ayrılıyor
                        masalar--;
                    }

                    int beklemeSuresi = masalar <= 0 ? Math.Min(maksimumBeklemeSuresi, i * odemeSuresi) : 0;

                    toplamKazanc++; // Müşteri gelirinden 1 birim kazanç

                    // Süre hesaplama
                    int toplamSure = (masalar * yemeSuresi) + (masalar * odemeSuresi) + beklemeSuresi;
                    toplamSure += (garsonlar * siparisAlmaSuresi) + (ascilar * yemekSuresi);

                    if (toplamSure <= calismaSuresi)
                    {
                        toplamKazanc -= (masalar + garsonlar + ascilar); // Toplam maliyet
                        if (toplamKazanc > enIyiKazanc)
                        {
                            enIyiKazanc = toplamKazanc;
                            enIyiMasalar = masalar;
                            enIyiGarsonlar = garsonlar;
                            enIyiAscilar = ascilar;
                        }
                    }
                }
            }
        }
    }

    Console.WriteLine($"En karlı durum: {enIyiKazanc} birim kazanç ile {enIyiMasalar} masa, {enIyiGarsonlar} garson, {enIyiAscilar} aşçı ile elde edilir.");

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