using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ulesane_tikets
{
    class Program
    {
        static int Saali_suurus()//выбор размера зала
        {
            Console.WriteLine("Vali saali suurus: 1,2,3");
            int suurus = int.Parse(Console.ReadLine());//считывание выбора пользователя
            return suurus;//возращение значения suurus

        }
        static int[,] saal = new int[,] { };
        static int[] ost = new int[] { };//ost числовой массив, который используеться в функции Muuk заполняя массив количеством билетов, запрошенных пользователем
        static int kohad, read, mitu, mitu_veel;//места,ряды,кол-во билетов 
        static void Saali_taitmine(int suurus)//создание зала по выбраному размеру
        {
            Random rnd = new Random();
            if (suurus == 1)//если выбран первый зал :20 мест и 10 рядов
            { kohad = 20; read = 10; }
            else if (suurus == 2)//если выбран второй зал :20 мест и 20 рядов
            { kohad = 20; read = 20; }
            else//если выбран третий зал: 30 мест и 20 рядов
            { kohad = 30; read = 20; }
            saal = new int[read, kohad];//заполнение двухмернного массива зала
            for (int rida = 0; rida < read; rida++)//заполнение мест через цикл и рандомное число от 0-1
            {
                for (int koht = 0; koht < kohad; koht++)
                {
                    saal[rida, koht] = rnd.Next(0, 2);
                }
            }
        }
        static void Muuk_Ise()//покупка одного билета
        {
            Console.WriteLine("rida:");//ряд
            int pileti_rida = int.Parse(Console.ReadLine());
            Console.WriteLine("koht:");//местo
            int pileti_koht = int.Parse(Console.ReadLine());
            if (saal[pileti_rida,pileti_koht]==0)
            {
                Console.WriteLine("koht {0} on vaba", pileti_koht);
                saal[pileti_rida, pileti_koht] = 1;//изменение статуса места после покупки/брони билета
            }
        }
        static void Saal_ekraanile()//экран зала
        {
            Console.Write("     ");
            for (int koht = 0; koht < kohad; koht++)
            {
                if (koht.ToString().Length == 2)//
                { Console.Write(" {0}", koht + 1); }
                //вывидение на экран номера мест
                else
                { Console.Write("  {0}", koht + 1); }
            }

            Console.WriteLine();
            for (int rida = 0; rida < read; rida++)//вывидение на экран номера рядов
            {
                Console.Write("Rida " + (rida + 1).ToString() + ":");
                for (int koht = 0; koht < kohad; koht++)
                {

                    Console.Write(saal[rida, koht] + "  ");
                }
                Console.WriteLine();
            }
        }
        static bool Muuk()//покупка нескольких билетов
        {
                Console.WriteLine("Rida:");//выбор ряда и кол-во  билетов
                int pileti_rida = int.Parse(Console.ReadLine());
                Console.WriteLine("Mitu piletid:");
                mitu = int.Parse(Console.ReadLine());
                ost = new int[mitu];
                int p = (kohad - mitu) / 2;//половина общих мест с вычитом введенных билетов пользователем(возможные месте для брони/покупки)
                bool t = false;//элемент с логическим типом данных для проверки купили ли билет или нет
                int k = 0;//счетчик 
                do
                {
                    if (saal[pileti_rida, p] == 0)//
                    {
                        ost[k] = p;
                        Console.WriteLine("koht {0} on vaba", p);//если место свободно, выводим на экран информацию об этом пользователю и t равно true
                        t = true;
                    }
                    else
                    {
                        Console.WriteLine("koht {0} on kinni", p);//если место занято, выводим на экран информацию об этом пользователю и t равно false(также обнуляем счетчик и возращяем p(возможные места?) к исходному значению
                        t = false;
                        ost = new int[mitu];//места в ряду
                        k = 0;
                        p = (kohad - mitu) / 2;
                        break;
                    }
                    p = p + 1;
                    k++;
                } while (mitu != k);
                if (t == true)
                {
                    Console.WriteLine("Sinu kohad on:");
                    foreach (var koh in ost)//выведение на экран свободного места, которое забронировали
                {
                        Console.WriteLine("{0}\n", koh);
                    }
                }
                else
                {
                    Console.WriteLine("Selles reas ei ole vabu kohti. Kas tahad teises reas otsida?");
                }
                return t;
        }
        static void Main(string[] args)
        {
            int suurus = Saali_suurus();
            Saali_taitmine(suurus);
            while (true)
            {
                Saal_ekraanile();//выведение зала на экран
                Console.WriteLine("1-Ise valik 2- masina valik");
                int valik = int.Parse(Console.ReadLine());
                if (valik == 1)
                {
                    Muuk_Ise();
                }
                else
                {
                    bool muuk = false;
                    while (muuk!=true)//цикл который будет повторяться до того момента когда мы сможем купить билет
                    {
                        muuk=Muuk();
                    }
                    
                }
                

            }

        }
    }
}
