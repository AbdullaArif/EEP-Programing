namespace EsitlemeAlgoritm
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Kadınların isimlerini virgülle ayırarak girin:");
            string kadinlarInput = Console.ReadLine();
            List<string> kadinlar = new List<string>(kadinlarInput.Split(','));

            Console.WriteLine("Erkeklerin isilerini virgülle ayırarak girin:");
            string erkeklerInput = Console.ReadLine();
            List<string> erkekler = new List<string>(erkeklerInput.Split(','));

            Dictionary<string, List<string>> kadinTercihleri = TercihListesiniOlustur(kadinlar, erkekler, "kadın");
            Dictionary<string, List<string>> erkekTercihleri = TercihListesiniOlustur(erkekler, kadinlar, "erkek");

            Dictionary<string, string> eslesmeler = GaleShapley(kadinlar, kadinTercihleri, erkekTercihleri);

            Console.WriteLine("Kararlı Eşleşmeler:");
            foreach (var eslesme in eslesmeler)
            {
                Console.WriteLine($"{eslesme.Key} ile {eslesme.Value}");
            }
        }

        static Dictionary<string, List<string>> TercihListesiniOlustur(List<string> kisiler, List<string> digerKisiler, string cinsiyet)
        {
            Dictionary<string, List<string>> tercihListeleri = new Dictionary<string, List<string>>();

            foreach (string kisi in kisiler)
            {
                Console.WriteLine($"Lütfen {kisi}'nin tecih listesini belirtin (virgülle ayıraak). Örn: {string.Join(",", digerKisiler)}");
                string tercihlerInput = Console.ReadLine();
                List<string> tercihler = new List<string>(tercihlerInput.Split(','));

                
                for (int i = 0; i < digerKisiler.Count; i++)
                {
                    if (!tercihler.Contains(digerKisiler[i]))
                    {
                        tercihler.Add(digerKisiler[i]);
                    }
                }

                tercihListeleri[kisi] = tercihler;
            }

            return tercihListeleri;
        }

        static Dictionary<string, string> GaleShapley(List<string> kadinlar, Dictionary<string, List<string>> kadinTercihleri, Dictionary<string, List<string>> erkekTercihleri)
        {
            Dictionary<string, string> eslesmeler = new Dictionary<string, string>();
            Queue<string> bekarKadinlar = new Queue<string>(kadinlar);

            while (bekarKadinlar.Count > 0)
            {
                string kadin = bekarKadinlar.Dequeue();
                List<string> tercihListesi = kadinTercihleri[kadin];

                foreach (string erkek in tercihListesi)
                {
                    if (!eslesmeler.ContainsValue(erkek))
                    {
                        eslesmeler[kadin] = erkek;
                        break;
                    }
                    else
                    {
                        string eskiEse = GetEskiEse(erkek, eslesmeler);
                        List<string> erkekTercihListesi = erkekTercihleri[erkek];

                        if (erkekTercihListesi.IndexOf(kadin) < erkekTercihListesi.IndexOf(eskiEse))
                        {
                            eslesmeler[kadin] = erkek;
                            bekarKadinlar.Enqueue(eskiEse);
                            break;
                        }
                    }
                }
            }

            return eslesmeler;
        }

        static string GetEskiEse(string erkek, Dictionary<string, string> eslesmeler)
        {
            foreach (var eslesme in eslesmeler)
            {
                if (eslesme.Value == erkek)
                    return eslesme.Key;
            }
            return null;
        }
    }

}