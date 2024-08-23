using System;
using System.Collections.Generic;


struct Product  // --> Mahsulotlar uchun struct.
{
    public string Name;
    public int Price;

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }
}


struct CartItem  // --> Savatdagi mahsulotlar uchun struct.
{
    public Product Product;
    public int Quantity;

    public CartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}

class Program
{

    static void Main(string[] args)
    {
        List<Product> products = new List<Product>
        {
            new Product("Olma", 10000),
            new Product("Banan", 15000),
            new Product("Apelsin", 6000)
        };

        List<CartItem> cart = new List<CartItem>();
        bool isRunning = true;

        while (isRunning) //--> isRunning - dastur tsiklini boshqarish uchun ishlatilatilinadi.
        {
            Console.WriteLine("\nSavdo Tizimi:");
            Console.WriteLine("1. Mahsulot qo'shish");
            Console.WriteLine("2. Savatni ko'rish");
            Console.WriteLine("3. Mahsulot miqdorini yangilash");
            Console.WriteLine("4. Mahsulotni olib tashlash");
            Console.WriteLine("5. Umumiy narxni hisoblash");
            Console.WriteLine("6. Chiqish");
            Console.Write("Tanlovni kiriting: ");
            int choice = int.Parse(Console.ReadLine()!);

            switch (choice)
            {
                case 1:
                    AddProductToCart(products, cart);
                    break;
                case 2:
                    ViewCart(cart);
                    break;
                case 3:
                    UpdateProductQuantity(cart);
                    break;
                case 4:
                    RemoveProductFromCart(cart);
                    break;
                case 5:
                    CalculateTotalPrice(cart);
                    break;
                case 6:
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Noto'g'ri tanlov!");
                    break;
            }
        }
    }

    static void AddProductToCart(List<Product> products, List<CartItem> cart)
    {
        Console.WriteLine("\nMahsulotlar:");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price } so'm");
        }

        Console.Write("Mahsulotni tanlang: ");
        int productIndex = int.Parse(Console.ReadLine()!) - 1;
        /*Foydalanuvchi kiritgan raqam int tipiga o'zgartiriladi va indeks sifatida ishlatilishi uchun 1 ga kamaytiriladi */

        if (productIndex >= 0 && productIndex < products.Count) //-->  Foydalanuvchi kiritgan indeks mahsulotlar ro'yxatida bor-yo'qligi tekshiriladi.
        {
            Console.Write("Miqdorini kiriting: ");
            int quantity = int.Parse(Console.ReadLine()!);

            int cartIndex = cart.FindIndex(c => c.Product.Name == products[productIndex].Name);
            /*Savatdagi mahsulotlar ro'yxatida tanlangan mahsulot bor-yo'qligi tekshiriladi.*/
            if (cartIndex >= 0)
            {
                //--> Agar mahsulot savatda allaqachon mavjud bo'lsa, uning miqdori yangilanadi. 
                cart[cartIndex] = new CartItem(cart[cartIndex].Product, cart[cartIndex].Quantity + quantity);
            }
            else
            {
                //--> Agar mahsulot savatda bo'lmasa, yangi CartItem yaratiladi va savatga qo'shiladi.
                cart.Add(new CartItem(products[productIndex], quantity));
            }

            Console.WriteLine("Mahsulot savatga qo'shildi.");
        }
        else
        {
            Console.WriteLine("Noto'g'ri mahsulot tanlandi!");
        }
    }

    static void ViewCart(List<CartItem> cart)
    {
        Console.WriteLine("\nSavat:");
        if (cart.Count == 0)
        {
            Console.WriteLine("Savat bo'sh.");
        }
        else
        {
            foreach (var item in cart)
            {
                Console.WriteLine($"{item.Product.Name} - {item.Quantity} dona - {item.Product.Price * item.Quantity } so'm");
            }
        }
    }

    static void UpdateProductQuantity(List<CartItem> cart)
    {
        Console.WriteLine("\nSavatdagi mahsulotlar:");
        for (int i = 0; i < cart.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cart[i].Product.Name} - {cart[i].Quantity} dona");
        }

        Console.Write("Yangilash uchun mahsulotni tanlang: ");
        int cartIndex = int.Parse(Console.ReadLine()!) - 1;
        //--> Foydalanuvchi kiritgan indeks savatdagi mahsulotlar ro'yxatida bor-yo'qligi tekshiriladi.
        if (cartIndex >= 0 && cartIndex < cart.Count)
        {
            Console.Write("Yangi miqdorni kiriting: ");
            int newQuantity = int.Parse(Console.ReadLine()!);
            //--> Agar kiritilgan miqdor 0 dan katta bo'lsa savatdagi tegishli mahsulotning miqdori yangi miqdorga yangilanadi.
            if (newQuantity > 0)
            {
                cart[cartIndex] = new CartItem(cart[cartIndex].Product, newQuantity);
                Console.WriteLine("Miqdor yangilandi.");
            }
            else
            {
                Console.WriteLine("Miqdor noto'g'ri!");
            }
        }
        else
        {
            Console.WriteLine("Noto'g'ri mahsulot tanlandi!");
        }
    }

    static void RemoveProductFromCart(List<CartItem> cart)
    {
        Console.WriteLine("\nSavatdagi mahsulotlar:");
        for (int i = 0; i < cart.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cart[i].Product.Name} - {cart[i].Quantity} dona");
        }

        Console.Write("Olib tashlash uchun mahsulotni tanlang: ");
        int cartIndex = int.Parse(Console.ReadLine()!) - 1;

        if (cartIndex >= 0 && cartIndex < cart.Count)
        {
            cart.RemoveAt(cartIndex);
            Console.WriteLine("Mahsulot savatdan olib tashlandi.");
        }
        else
        {
            Console.WriteLine("Noto'g'ri mahsulot tanlandi!");
        }
    }

    static void CalculateTotalPrice(List<CartItem> cart)
    {
        int total = 0;
        foreach (var item in cart)
        {
            total += item.Product.Price * item.Quantity;
        }
        Console.WriteLine($"\nUmumiy narx: {total} so'm");
    }
}
