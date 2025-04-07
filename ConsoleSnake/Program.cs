using System;


namespace ConsoleSnake
{
    class Program
    {
        // Параметры еды
        static int foodX;
        static int foodY;
        static int record = 0;


        static void SpawnFood()
        {
            Random rnd = new Random();
            foodX = rnd.Next(0, 120);
            if (foodX % 2 != 0) foodX += 1;

            foodY = rnd.Next(0, 40);
        }



        static void Main()
        {
            // Параметры программы
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = false;
            bool isGame = true;
            

            // Параметры змейки

            int head_x = 20;
            int head_y = 10;
            int dir = 0;
            int snakeLen = 10;
            int[] body_x = new int[100];
            int[] body_y = new int[100];

            // Стартовое значение змейки

            for(int i = 0; i < snakeLen; i++)
            {
                body_x[i] = head_x - (i * 2);
                body_y[i] = 10;
            }

            // Стартовое значение еды

            SpawnFood();

            // Игровой цикл
            while (isGame == true)
            {
                // 1. Очистка

                for (int i = 0; i < snakeLen; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("  ");
                }

                Console.SetCursorPosition(head_x, head_y);
                Console.Write("  ");

                Console.SetCursorPosition(foodX, foodY);
                Console.Write("  ");

                // 2. Расчёт

                if (snakeLen >= record) record = snakeLen - 10;

                

                // Движение змейки

                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key;
                    Console.SetCursorPosition(0, 0);
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.Write(" ");

                    if (key.Key == ConsoleKey.D && dir != 2) dir = 0;
                    if (key.Key == ConsoleKey.S && dir != 3) dir = 1;
                    if (key.Key == ConsoleKey.A && dir != 0) dir = 2;
                    if (key.Key == ConsoleKey.W && dir != 1) dir = 3;
                }

                if (dir == 0) head_x += 2;
                if (dir == 1) head_y += 1;
                if (dir == 2) head_x -= 2;
                if (dir == 3) head_y -= 1;

                if (head_x < 0) head_x = 118;
                if (head_x > 118) head_x = 0;
                if (head_y < 0) head_y = 39;
                if (head_y > 39) head_y = 0;

                for (int i = snakeLen; i > 0; i--)
                {
                    body_x[i] = body_x[i - 1];
                    body_y[i] = body_y[i - 1];
                }
                body_x[0] = head_x;
                body_y[0] = head_y;

                for(int i = 1; i < snakeLen; i++)
                {
                    if(body_x[i] == head_x && body_y[i] == head_y)
                    {
                        isGame = false;
                    }
                }

                // Бесконечное поле

                

                // Еда

                if (head_x == foodX && head_y == foodY)
                {
                    snakeLen++;
                    SpawnFood();
                }

                // 3. Отрисовка
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                for (int i = 0; i < snakeLen; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("██");
                }

                
                Console.SetCursorPosition(50, 0); // отоброжание счёта
                Console.WriteLine("Score: " + (snakeLen - 10));

               
                Console.SetCursorPosition(head_x, head_y);
                Console.Write("██");


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(foodX, foodY);
                Console.Write("██");

                

                // 4. Ожидание
                System.Threading.Thread.Sleep(50);
            }

            

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(50, 20);
            Console.Write("You loose");
            Console.SetCursorPosition(50, 25);
            Console.Write("You record: " + record);
            Console.ReadLine();
        }
    }
}
