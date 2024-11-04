using System;
using System.Collections.Generic;

// Banka müşterisini temsil eden sınıf
class BankaMüşteri
{
    public string İsim { get; set; } // Müşterinin adı
    public int Öncelik { get; set; } // Müşterinin işlem önceliği (1: Yüksek, 2: Orta, 3: Düşük)

    public BankaMüşteri(string isim, int öncelik)
    {
        İsim = isim; // Müşterinin adını ayarlıyoruz
        Öncelik = öncelik; // Müşterinin önceliğini ayarlıyoruz
    }
}

// Banka kuyruğunu yöneten sınıf
class BankaKuyruğu
{
    // Üç farklı öncelik için ayrı kuyruklar tanımlıyoruz
    private Queue<BankaMüşteri> yüksekÖncelik = new Queue<BankaMüşteri>();
    private Queue<BankaMüşteri> ortaÖncelik = new Queue<BankaMüşteri>();
    private Queue<BankaMüşteri> düşükÖncelik = new Queue<BankaMüşteri>();

    // Kuyruğa yeni bir müşteri ekleyen metot
    public void Enqueue(BankaMüşteri müşteri)
    {
        switch (müşteri.Öncelik)
        {
            case 1:
                yüksekÖncelik.Enqueue(müşteri); // Yüksek öncelikli müşteri ekleniyor
                break;
            case 2:
                ortaÖncelik.Enqueue(müşteri); // Orta öncelikli müşteri ekleniyor
                break;
            case 3:
                düşükÖncelik.Enqueue(müşteri); // Düşük öncelikli müşteri ekleniyor
                break;
            default:
                Console.WriteLine("Geçersiz öncelik."); // Geçersiz öncelik uyarısı
                break;
        }
    }

    // Kuyruktan en yüksek öncelikli müşteriyi çıkaran metot
    public BankaMüşteri Dequeue()
    {
        if (yüksekÖncelik.Count > 0)
        {
            return yüksekÖncelik.Dequeue(); // Yüksek öncelikli müşteri çıkarılıyor
        }
        else if (ortaÖncelik.Count > 0)
        {
            return ortaÖncelik.Dequeue(); // Orta öncelikli müşteri çıkarılıyor
        }
        else if (düşükÖncelik.Count > 0)
        {
            return düşükÖncelik.Dequeue(); // Düşük öncelikli müşteri çıkarılıyor
        }
        else
        {
            Console.WriteLine("Kuyruk boş."); // Kuyrukta müşteri kalmadığında uyarı
            return null;
        }
    }

    // Kuyruktaki tüm müşterileri ekrana yazdıran metot
    public void Göster()
    {
        Console.WriteLine("Yüksek Öncelik Kuyruğu:");
        foreach (var müşteri in yüksekÖncelik)
        {
            Console.WriteLine($"- {müşteri.İsim}"); // Yüksek öncelikli müşterileri yazdırıyoruz
        }

        Console.WriteLine("\nOrta Öncelik Kuyruğu:");
        foreach (var müşteri in ortaÖncelik)
        {
            Console.WriteLine($"- {müşteri.İsim}"); // Orta öncelikli müşterileri yazdırıyoruz
        }

        Console.WriteLine("\nDüşük Öncelik Kuyruğu:");
        foreach (var müşteri in düşükÖncelik)
        {
            Console.WriteLine($"- {müşteri.İsim}"); // Düşük öncelikli müşterileri yazdırıyoruz
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankaKuyruğu kuyruk = new BankaKuyruğu(); // Yeni bir banka kuyruğu oluşturuyoruz

        // Kullanıcıdan müşteri bilgilerini alıyoruz
        while (true)
        {
            Console.Write("Müşterinin adını girin (çıkmak için 'q' basın): ");
            string isim = Console.ReadLine();
            if (isim.ToLower() == "q") break; // Çıkmak için 'q' tuşuna basılıyor

            Console.Write("Müşterinin önceliğini girin (1: Yüksek, 2: Orta, 3: Düşük): ");
            if (!int.TryParse(Console.ReadLine(), out int öncelik) || öncelik < 1 || öncelik > 3)
            {
                Console.WriteLine("Geçersiz öncelik. Lütfen 1, 2 veya 3 girin.");
                continue; // Geçersiz öncelikte tekrar denemek için
            }

            // Müşteri bilgilerini kuyruğa ekliyoruz
            kuyruk.Enqueue(new BankaMüşteri(isim, öncelik));
        }

        // Kuyruktaki durumu gösteriyoruz
        kuyruk.Göster();

        // İlk işlem yapılıyor
        Console.WriteLine("\nİlk işlem yapılıyor...");
        BankaMüşteri işlemYapılacak = kuyruk.Dequeue(); // En yüksek öncelikli müşteri çıkarılıyor
        if (işlemYapılacak != null)
        {
            Console.WriteLine($"İşlem yapılan müşteri: {işlemYapılacak.İsim}"); // İşlem yapılan müşteri yazdırılıyor
        }

        // Kuyruktaki durumu tekrar gösteriyoruz
        kuyruk.Göster();

        Console.ReadLine(); // Program sonlanmadan önce kullanıcıdan giriş bekliyoruz
    }
}
