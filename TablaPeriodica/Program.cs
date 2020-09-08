using System;
using System.Collections.Generic;

namespace TablaPeriodica
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			byte opcionMenu;
			bool salir;
			bool nuevoElemento = false;

			//Cargamos los datos en el fichero
			List<Elemento> elementos = GestorElementos.CargarDatos();
			
			do
			{
				Console.Clear();
				//Tabla.MostrarSigloActual(elementos);
				MostrarMenu();

				opcionMenu = Convert.ToByte(Console.ReadLine());

				switch (opcionMenu)
				{
					case 1:
						{
							salir = false;
							Tabla.MostrarTodosLosSiglos(elementos);
							Console.WriteLine("Pulsa una tecla para continuar...");
							Console.ReadKey();
							break;
						}
					case 2:
						{
							salir = false;
							int siglo;
							do
							{
								Console.SetCursorPosition(40, 6);
								Console.Write("Indica el siglo: ");

							} while (int.TryParse(Console.ReadLine(),out siglo)
							|| siglo <0 || siglo >21);

							Console.Clear();
							Tabla.MostarSiglo(elementos, siglo);

							Console.SetCursorPosition(40, 0);
							if(siglo == 0)
							{
								Tabla.DetalleAnyoElemento(elementos, -1);
							}
							else
							{
								Console.Write("Elige un año: ");
								if (int.TryParse(Console.ReadLine(), out int anyo))
									Tabla.DetalleAnyoElemento(elementos, anyo);
							}
							break;
						}
					case 3:
						{
							salir = false;
							elementos.Add(Tabla.PedirElemento());
							nuevoElemento = true;
							break;
						}
					case 4:
						{
							if (nuevoElemento)
								GestorElementos.GuardarDatos(elementos);
							
							salir = true;
							break;
						}

					default: salir = false;
						Console.WriteLine("Opción no permitida.");
						break;
				}

			} while (!salir);
			Console.WriteLine("Hasta pronto!");
		}

		private static void MostrarMenu()
		{
			string[] menu = {
							"1. Ver todos los siglos\n",
							"2. Ver siglo\n",
							"3. Nuevo Elemento\n",
							"4. Salir\n",
							"",
							"Elige una opción: "
			};

			for (int i = 0; i < menu.Length; i++)
			{
				Console.SetCursorPosition(40, i);
				Console.Write(menu[i]);
			}
		}
	}
}
