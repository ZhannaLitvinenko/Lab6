using System;
using System.Collections.Generic;

namespace Lab6
{
	class Program
	{
		static double Start, End, h, y0;
		static string Function;

		static void Main(string[] args)
		{
			//Приймаємо вхідні дані:
			Input();

			//Обрахуємо значення функції при кроці, введеному користувачем:
			List<double> y1 = CalculateCorrectedEuler(Start, End, y0, h, Function);
			//Виводимо отримані значення:
			Console.WriteLine($"Точки, отриманi виправленим методом Ейлера при кроцi {h} на iнтервалi [{Start}; {End}]:\n");
			PrintMatlabPointList(y1);

			//Обрахуємо значення функції при кроці, рівному половині введеного:
			List<double> y2 = CalculateCorrectedEuler(Start, End, y0, h / 2, Function);
			//Виведемо отримані значення:
			Console.WriteLine($"{new string('-', 100)}\n\nТочки, отриманi виправленим методом Ейлера при половинi кроку h/2 =" +
				$" {h} на iнтервалi [{Start}; {End}]:\n");
			PrintMatlabPointList(y2);

			Console.ReadKey();
		}

		static void Input()
		{
			Console.WriteLine("Введiть функцiю y' = f(t, y):");
			Function = Console.ReadLine();
			Console.WriteLine("Введiть початок iнтервалу:");
			Start = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine("Введiть кiнець iнтервалу:");
			End = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine("Введiть крок iнтегрування:");
			h = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine($"Введiть початкове значення y({Start}):");
			y0 = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine($"{new string('-', 100)}\n");
		}

		static List<double> CalculateCorrectedEuler(double start, double end, double y0, double step, string function)
		{
			//список точок у, що отримуються:
			List<double> y = new List<double>();

			//поточні значення y (початок від y0) та t (початок інтервалу)
			double currentY = y0, currentT = start;
			//одразу записуємо перше значення точки в список:
			y.Add(currentY);

			//Кількість кроків, що буде виконана:
			double steps = (end - start) / step;
			for (int i = 1; i <= steps; i++)
			{
				//виправлена формула Ейлера:
				//y1 = y0 + 1/2 * (k1 + k2), де:
				//k1 = h * f(t0, y0), k2 = h * f(t0 + h, y0 + k1)
				double k1 = MathHelper.EvaluateFunction(currentT, y[i - 1], function) * step;
				double k2 = MathHelper.EvaluateFunction(currentT + step, y[i - 1] + k1, function) * step;
				double deltaY = 0.5 * (k1 + k2);

				//y1 = y0 + deltaY
				currentY += deltaY;
				//t1 = t0 + h
				currentT += step;

				//Записуємо отримане значення y
				y.Add(currentY);
			}

			//повертаємо отримані точки:
			return y;
		}

		static void PrintMatlabPointList(List<double> list)
		{
			//Формуємо рядок, щоб підставити його в Октаву:
			string points = "[";
			for (int i = 0; i < list.Count; i++)
			{
				points += $"{Math.Round(list[i], 4)}".Replace(',', '.');

				if (i != list.Count - 1)
					points += ", ";
			}
			points += "]";

			//Виводимо його:
			Console.WriteLine(points);
		}
	}
}
