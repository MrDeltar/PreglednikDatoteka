using System;
using System.Collections.Generic;
using System.IO;

namespace DatotecniSustav01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*****************************************************************\n***Ispisivanje svih diskova prisutnih (i spremnih) na Racunalu***");
            DriveInfo[] diskovi = DriveInfo.GetDrives();
            try
            {
                foreach (DriveInfo d in diskovi)
                {
                    if (d.IsReady == true)      // provjeravamo ako je disk spreman za koristenje
                    {
                        Console.WriteLine("Disk {0} ukupno {1}GB(slobodno {2}GB, zauzeto {4}GB); Format: {3}", d.Name, d.TotalSize / (1024 * 1024 * 1024), d.AvailableFreeSpace / (1024 * 1024 * 1024), d.DriveFormat, (d.TotalSize - d.AvailableFreeSpace) / (1024 * 1024 * 1024));
                    }
                }
            }
            catch (IOException e)       // za dohvat moguce greske
            {
                Console.WriteLine("Greska u dohvatu diska: {0}", e);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Pristup disku je zabranjen: {0}", e);
            }
            Console.WriteLine("*****************************************************************");
            @Console.WriteLine("Odaberi disk(npr. 'C:\\'):");
            string odabir = Console.ReadLine();     // uneseni string MORA BITI ISTI kao ime zeljenog diska/particije, tj. odabir == d.Name
            foreach (DriveInfo d in diskovi)
            {
                if (d.IsReady == true)
                {
                    try
                    {
                        if (d.ToString() == odabir)
                        {
                            string disk = d.ToString();
                            DirectoryInfo dirInfo = new DirectoryInfo(disk);
                            DirectoryInfo[] dir = dirInfo.GetDirectories();

                            var datoteke = dirInfo.GetFiles();
                            long velicina = 0;

                            Console.WriteLine("+------------------+-------------+---------+------------------------------------------+");
                            Console.WriteLine("| Veličina       B |          KB |      MB | Nazivi datoteka                          |");
                            Console.WriteLine("+------------------+-------------+---------+------------------------------------------+");
                            foreach (DirectoryInfo f in dir)
                            {
                                try
                                {
                                    Console.WriteLine("| {0 ,10} |", f.Name);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Greska: {0}", e);
                                }
                            }
                            foreach (FileInfo e in datoteke)
                            {
                                velicina += e.Length;
                                Console.WriteLine("|{0, 15} B | {1, 8} KB | {2, 4} MB | {3,10} |",
                                    e.Length,
                                    e.Length / 1024,
                                    e.Length / (1024 * 1024),
                                    e.FullName);
                            }
                            Console.WriteLine("+------------------+-------------+---------+------------------------------------------+");
                            Console.WriteLine("|{0, 15} B | {1, 8} KB | {2, 4} MB |                                          |",
                                velicina,
                                velicina / 1024,
                                velicina / (1024 * 1024));
                            Console.WriteLine("+------------------+-------------+---------+------------------------------------------+");

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Doslo je do greske: {0}", e);
                    }
                }
            }
            Console.WriteLine("*****************************************************************");

            Console.ReadLine();

            /*
            Console.SetCursorPosition(1, 3);
            Console.Write(">");
            int brojRedova = datoteke.Length + 6;

            int cekanjeTreperenje = 500;
            Console.CursorVisible = false;
            int pokazivacY = 3;
            while (true)
            {
                System.Threading.Thread.Sleep(cekanjeTreperenje);
                Console.SetCursorPosition(1, pokazivacY);
                Console.Write(" ");
                System.Threading.Thread.Sleep(cekanjeTreperenje);
                Console.SetCursorPosition(1, pokazivacY);
                Console.Write(">");

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pritisnutaTipka = Console.ReadKey(true);
                    if (pritisnutaTipka.Key == ConsoleKey.DownArrow)
                    {
                        pokazivacY++;
                    }
                }
            }
            */
            // Console.SetCursorPosition(0, brojRedova);
        } //Main
    }
}
