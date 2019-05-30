using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingGame
{
    static class Menu
    {
       public static void Start()
        {
            Console.WriteLine("Виберіть число: ");
            Console.WriteLine("1. Іти на риболовну базу");
            Console.WriteLine("2. Ловити рибу");
            Console.WriteLine("3. Дані персонажа");
            Console.WriteLine("0. Вихід");
        } 
        public static void Base()
        {
            Console.WriteLine("1. Продати всю рибу");
            Console.WriteLine("2. Продати зачотну рибу");

        }
    }

    class Fish
    {
        private double minWeight;
        private double maxWeight;
        public double fishWeight { get; private set; }
        public double enoughWeight { get; private set; }
        string type;
        public double prisePerKg { get; private set; }

        public Fish(double minWeight, double maxWeight, double enoughWeight, string type, double prisePerKg)
        {
            this.minWeight = minWeight;
            this.maxWeight = maxWeight;
            this.enoughWeight = enoughWeight;
            this.type = type;
            this.prisePerKg = prisePerKg;
        }

        public bool Catch()
        {
            Random random = new Random();
            this.fishWeight = minWeight + (maxWeight - minWeight) * random.NextDouble();
            Console.Write($"\nВи зловили {type}. Вага риби: {fishWeight}. ");
            if (this.fishWeight < enoughWeight)
            {
                Console.WriteLine("Вона не зачотна.\n");
                return false;
            }
            else {
                Console.WriteLine("Вона зачотна!\n");
                return true; }
        }
        public override string ToString()
        {
            return $"{type} : {fishWeight} кг";
        }
    }

    class Fishpond
    {
        public List<Fish> fish = new List<Fish>();
        static List<Fish> fishList = new List<Fish>();

        static Fishpond()
        {
            fishList.Add(new Fish(0.04, 2.5, 1.0, "Окунь", 100));
            fishList.Add(new Fish(0.1, 3.5, 1.5, "Карась", 80));
            fishList.Add(new Fish(0.5, 35.0, 4.5, "Сазан", 110));
            fishList.Add(new Fish(0.3, 25, 4.0, "Щука", 60));
            fishList.Add(new Fish(0.03, 1.2, 0.6, "Плотва", 40));
        }
        
        public void addFish()
        {
            Random random = new Random();
            fish.Add(fishList[random.Next(fishList.Count)]);
            fish.Last().Catch();            
        }

        public override string ToString()
        {
            string infoFishpond = "";
            for (int i = 0; i < fish.Count; i++)
            {
                infoFishpond += fish[i] + "\n";
            }
            return infoFishpond;
        }

    }

    class Character
    {
        public string name = "Невідомий";
        public double money = 0;
        Fishpond myFish = new Fishpond();
        
        public bool Fishing()
        {
            Console.WriteLine("Ви закинули вудочку...");
            System.Threading.Thread.Sleep(2000);
            myFish.addFish();
            return true;
        }

        public override string ToString()
        {
            return $"У Вас {money}грн. Ваша риба: \n" + myFish.ToString();
        }

        public void SellAllFish()
        {
            double myMoney = Base.SellFish(ref this.myFish.fish);
            Console.WriteLine($"Ви отримали {myMoney} грн");
            money += myMoney;
        }
        public void SellGoodFish()
        {
            double myMoney = Base.SellGoodFish(ref this.myFish.fish);
            Console.WriteLine($"Ви отримали {myMoney} грн");
            money += myMoney;
        }

    }

    static class Base
    {
        public static double SellFish(ref List<Fish> fish)
        {
            double money = 0;
            foreach (var f in fish)
            {
                if (f.fishWeight < f.enoughWeight)
                {
                    money += (f.fishWeight * f.prisePerKg)/3;
                }
                else
                {
                    money += f.fishWeight * f.prisePerKg;
                }
            }
            fish = new List<Fish>();
            return money; 
        }
        public static double SellGoodFish(ref List<Fish> fish)
        {
            double money = 0;
            for (int i = 0; i < fish.Count; i++) { 
           
                if (fish[i].fishWeight >= fish[i].enoughWeight)
                {
                    money += (fish[i].fishWeight * fish[i].prisePerKg);
                    fish.Remove(fish[i]);
                    i--;
                }
            }
            return money;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            bool exit = true;
            Character person = new Character();
            while (exit == true)
            {
                Menu.Start();
                string key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        Menu.Base();
                        string key1 = Console.ReadLine();
                       switch (key1)
                        {
                            case "1":
                                person.SellAllFish();
                                break;
                            case "2":
                                person.SellGoodFish();
                                break;
                        }
                        break;
                    case "2":
                        person.Fishing();
                        break;
                    case "3":
                        Console.WriteLine(person);
                        break;
                    case "0":
                        exit = false;
                        break;
                }



            }
        }
    }
}
