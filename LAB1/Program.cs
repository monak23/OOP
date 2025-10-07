using System;
using System.Collections.Generic;

namespace VendingMachineApp
{
    class Program
    {
        static void Main()
        {
            VendingMachine machine = new VendingMachine();
            machine.Run();
        }
    }

    class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, int price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    class VendingMachine
    {
        private List<Product> products = new List<Product>();
        private int balance = 0;
        private int totalMoney = 0;

        public VendingMachine()
        {
            products.Add(new Product("Odens", 700, 5));
            products.Add(new Product("ICEBERG", 300, 3));
            products.Add(new Product("SIBERIA", 700, 4));
            products.Add(new Product("LYFT", 400, 6));
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ВЕНДИНГОВЫЙ АВТОМАТ ===");
                Console.WriteLine("1. Посмотреть список товаров");
                Console.WriteLine("2. Внести монету");
                Console.WriteLine("3. Купить товар");
                Console.WriteLine("4. Отменить и вернуть деньги");
                Console.WriteLine("5. Режим администратора");
                Console.WriteLine("0. Выход");
                Console.WriteLine($"\nБаланс: {balance} руб.");
                Console.Write("\nВыберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowProducts(); break;
                    case "2": InsertMoney(); break;
                    case "3": BuyProduct(); break;
                    case "4": CancelOperation(); break;
                    case "5": AdminMode(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }

                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        private void ShowProducts()
        {
            Console.WriteLine("\n=== Список товаров ===");
            for (int i = 0; i < products.Count; i++)
            {
                Product p = products[i];
                Console.WriteLine($"{i + 1}. {p.Name} — {p.Price} руб. (осталось {p.Quantity})");
            }
        }

        private void InsertMoney()
        {
            Console.WriteLine("\nДоступные номиналы: 100, 200, 500, 1000");
            Console.Write("Введите номинал монеты: ");
            if (int.TryParse(Console.ReadLine(), out int coin) && (coin == 100 || coin == 200 || coin == 500 || coin == 1000))
            {
                balance += coin;
                Console.WriteLine($"Монета {coin} руб. принята. Баланс: {balance} руб.");
            }
            else
            {
                Console.WriteLine("Неверный номинал!");
            }
        }

        private void BuyProduct()
{
    ShowProducts();
    Console.Write("\nВведите номер товара: ");
    if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= products.Count)
    {
        Product product = products[choice - 1];

        if (product.Quantity == 0)
        {
            Console.WriteLine("Товар закончился!");
            return;
        }

        if (balance >= product.Price)
        {
            balance -= product.Price;
            totalMoney += product.Price;
            product.Quantity--;
            Console.WriteLine($"Вы купили {product.Name}!");
        }
        else
        {
            Console.WriteLine($"Недостаточно средств. Нужно {product.Price - balance} руб. больше.");
        }
    }
    else
    {
        Console.WriteLine("Неверный выбор!");
    }
}


private void CancelOperation()
        {
            if (balance > 0)
            {
                Console.WriteLine($"Возврат {balance} руб.");
                balance = 0;
            }
            else
            {
                Console.WriteLine("Баланс нулевой, возвращать нечего.");
            }
        }

        private void AdminMode()
        {
            Console.Write("\nВведите пароль администратора: "); // пароль: 1234
            string pass = Console.ReadLine();

            if (pass != "1234")
            {
                Console.WriteLine("Неверный пароль!");
                return;
            }

            Console.WriteLine("\n=== Режим администратора ===");
            Console.WriteLine("1. Пополнить товар");
            Console.WriteLine("2. Собрать выручку");
            Console.WriteLine("0. Выход");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": RefillProducts(); break;
                case "2": CollectMoney(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор!"); break;
            }
        }

        private void RefillProducts()
        {
            ShowProducts();
            Console.Write("\nВведите номер товара для пополнения: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= products.Count)
            {
                Console.Write("Введите кол-во для добавления: ");
                if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
                {
                    products[choice - 1].Quantity += amount;
                    Console.WriteLine("Товар успешно пополнен!");
                }
                else Console.WriteLine("Неверное кол-во!");
            }
            else Console.WriteLine("Неверный выбор!");
        }

        private void CollectMoney()
        {
            Console.WriteLine($"Вы собрали {totalMoney} руб. из автомата.");
            totalMoney = 0;
        }
    }
}