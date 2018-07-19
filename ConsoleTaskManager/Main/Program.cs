using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Main
{
	class Program
	{
		public static List<TaskModel> list = new List<TaskModel>();

		static void Main(string[] args)
		{
			ConsoleEx.WriteLine("CONSOLE TASK MANAGER".PadLeft(20), ConsoleColor.Yellow);
			string command = "";
			do
			{
				
				
				Console.WriteLine("Podaj komende: ");
				Console.WriteLine("Dostępne opcje to: add, remove, clear, show, save, load, ");
				command = Console.ReadLine();
				switch (command)
				{
					case "add":
						list.Add(AddTask());
						Console.Clear();
						break;

					case "remove":
						RemoveTask();
						Console.Clear();
						break;

					case "show":
						ShowTasks(); 
						break;

					case "clear":
						Console.Clear();
						break;

					case "save":
						SaveTasks();
						Console.Clear();
						break;

					case "load":
						LoadTasks();
						Console.Clear();
						break;
				 
					default:
						Console.WriteLine("nie rozpoznano polecenia");
						break;
				}
				
			}
			while (command != "exit");
			
		}


		public static TaskModel AddTask()
		{
			Console.Write("Podaj opis zadania: ");										    //opis
			string description = Console.ReadLine();


			Console.Write("Czy jest to zadanie wazne?: [t/n]");							  //czy wazne
			string importantTaskstr = Console.ReadLine();
			bool? importantTask = null;
			
			switch (importantTaskstr)
			{
				case "t":
					importantTask = true;
					break;
				case "n":
					importantTask = false;
					break;
				default:
					importantTask = false;
					break;
			}


			Console.Write("Podaj date rozpoczęcia: dzien/miesiac/rok ");                    //start
			string startstr = Console.ReadLine().ToString(CultureInfo.InvariantCulture);
			DateTime start;
			try
			{
				
				if (startstr == "")
				{
					start = DateTime.Now;
				}
				else
				{
					start = Convert.ToDateTime(startstr);
				}
			}
			catch (System.FormatException)
			{
				Console.WriteLine("Problem z formatem daty");
				Console.ReadKey();
				return null;
			}
			

			

			Console.Write("Czy jest to zadanie na jeden dzień: [t/n] ");        //zadanie jednodniowe
			DateTime? end = null;
			string allDayTaskstr = Console.ReadLine();
			bool? allDayTask = null;

			if (allDayTaskstr == "t")
			{
				allDayTask = true;
				end = start;
			}
			if (allDayTaskstr == "n")
			{
				allDayTask = false;
				Console.Write("Podaj date zakonczenia: dzien/miesiac/rok ");                //end
				string endstr = Console.ReadLine();	

				try
				{
					end = Convert.ToDateTime(startstr);
				}
				catch (System.FormatException)
				{
					Console.WriteLine("Problem z formatem daty");
					Console.ReadKey();
				}
			}

			


			return new TaskModel(description, start , end , allDayTask, importantTask); 
		}
		

		public static void RemoveTask()
		{
			Console.Clear();
			ShowTasks();
			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Podaj ID zadania do usuniecia: ");
			int id = int.Parse(Console.ReadLine());
			list.RemoveAt(id-1);
			Console.Clear();
			ShowTasks();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine($"Usunięto zadanie z numerem id {id}");
			Console.ReadKey();
			Console.ReadKey(); 
		}

		public static void ShowTasks()
		{
			ConsoleColor a = ConsoleColor.DarkRed;
			ConsoleEx.WriteLine("Aktualna lista wszystkich zadan:", a );
			ConsoleEx.Write($"ID |".PadLeft(10), a);
			ConsoleEx.Write($" Opis zadania |".PadLeft(30), a);
			ConsoleEx.Write($" Data startu |".PadLeft(25), a);
			ConsoleEx.Write($" Data zakończenia |".PadLeft(25), a);
			ConsoleEx.Write($" Czy ważne? |".PadLeft(15), a);
			ConsoleEx.Write($" Całodniowe? |".PadLeft(15), a);
			Console.WriteLine();
			ConsoleEx.WriteLine("".PadLeft(114, '-'), a);
			int i = 1;
			foreach (TaskModel task in list)
			{
				ConsoleColor b = ConsoleColor.Green;
				ConsoleEx.Write($"{i} |".PadLeft(10), b);
				ConsoleEx.Write($"{task.Description} |".PadLeft(30), b);
				ConsoleEx.Write($"{task.Start}|".PadLeft(25), b);
				ConsoleEx.Write($"{task.End} |".PadLeft(25), b);
				ConsoleEx.Write($"{task.ImportantTask} |".PadLeft(15), b);
				ConsoleEx.Write($"{task.AlldayTask} |".PadLeft(15), b);
				
				i++;
			}
			Console.WriteLine();

		}

		public static void SaveTasks()
		{
			Console.WriteLine("Podaj nazwe pliku z rozszerzeniem (zalecany format *.csv)");
			string path = Console.ReadLine();

			List<string> listsave = new List<string>();
			foreach (TaskModel tosave in list)
			{
				listsave.Add($"{tosave.Description},{tosave.Start},{tosave.End},{tosave.ImportantTask},{tosave.AlldayTask}"); 
			}
			File.WriteAllLines(path, listsave);

			Console.WriteLine($"Lista zadań została zapisana do pliku {path} w folderze projektu");
			Console.ReadKey();


		}

		public static void LoadTasks()
		{  
		
			Console.WriteLine("Podaj nazwe pliku z rozszerzeniem .csv");
			string path = Console.ReadLine();
			string[] tableload = File.ReadAllLines(path);
			Console.WriteLine(tableload[0]);
			foreach (string line in tableload)
			{
				string[] linesplit = line.Split(',');
				string des = linesplit[0];
				DateTime st = DateTime.Parse(linesplit[1]);
				DateTime en = DateTime.Parse(linesplit[2]);
				bool imp;
				if (linesplit[3] == "true")
				{
					imp = true;
				}
				else
				{
					imp = false;
				}
				bool day;
				if (linesplit[4] == "true")
				{
					day = true;
				}
				else
				{
					day = false;
				}

				list.Add(new TaskModel(des, st, en, imp, day));


			}
			Console.Clear();
			ShowTasks();



					Console.WriteLine($"Lista zadań została załadowana z pliku {path} z foldera projektu");
			Console.ReadKey();
			




		}
	}
}
	
