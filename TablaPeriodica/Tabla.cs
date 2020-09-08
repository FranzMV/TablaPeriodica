using System;
using System.Collections.Generic;
/**
 * Clase con métodos útiles para mostrar información de los elementos agrupados
 * en años y siglos.
 */
namespace TablaPeriodica
{
	public class Tabla
	{
		//Obtiene cuántos número antiguos se ha descubierto
		private static int getNumElementosAC(List<Elemento> elementos)
		{
			int numElementos = 0;
			foreach(Elemento elemento in elementos)
			{
				if (elemento.ElementoAntiguo && (elemento.GetType() == typeof(Metal) ||
					elemento.GetType() == typeof(Metaloide) ||
					elemento.GetType() == typeof(NoMetal)))
					numElementos++;
			}
			return numElementos;
		}

		//Obtiene cuantos elementos se han descubierto en un año concreto
		private static int getNumElementosAnyo(List<Elemento> elementos, int anyo)
		{
			int numElementos = 0;
			foreach (Elemento elemento in elementos)
			{
				if (elemento.AnyoDescubrimiento.Year == anyo && !elemento.ElementoAntiguo &&
					(elemento.GetType() == typeof(Metal) || elemento.GetType() == typeof(Metaloide) ||
					elemento.GetType() == typeof(NoMetal)))
					numElementos++;
			}
			return numElementos;
		}

		//Método para determinar en qué color mostrar un año
		private static void mostrarColorElemnto(List<Elemento> elementos, int anyo)
		{
			int numMetales = 0;
			int numMetaloides = 0;
			int numNoMetales = 0;
			foreach(Elemento e in elementos)
			{
				if (e.AnyoDescubrimiento.Year == anyo && !e.ElementoAntiguo)
					if (e.GetType() == typeof(Metal))
						numMetales = 1;
					else if (e.GetType() == typeof(Metaloide))
						numMetaloides = 1;
					else if (e.GetType() == typeof(NoMetal))
						numNoMetales = 1;
			}
			//Color invertido si hay más de un tipo
			if (numMetales + numMetaloides + numNoMetales > 1)
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			else if (numNoMetales == 1)
				Console.ForegroundColor = ConsoleColor.Red;
			else if (numMetaloides == 1)
				Console.ForegroundColor = ConsoleColor.Blue;
			else if (numNoMetales == 1)
				Console.ForegroundColor = ConsoleColor.Green;

			Console.Write(anyo);


			//Restableceer colores
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}

		//Métodos para pedir los datos
		private static bool pedirBooleano(string mensaje)
		{
			string cadena;
			bool dato = false;

			do
			{
				Console.WriteLine(mensaje);
				Console.WriteLine("Introduce (SI/NO)");
				cadena = Console.ReadLine();
				if (cadena.ToUpper().Equals("SI") || cadena.ToUpper().Equals("S"))
					dato = true;
				else if (cadena.ToUpper().Equals("NO") || cadena.ToUpper().Equals("N"))
					dato = false;
				else
					Console.WriteLine("Debe introducir SI/S o NO/N.\n");

			} while (string.IsNullOrEmpty(cadena));

			return dato;
		}

		private static int pedirEntero(string mensaje)
		{
			int dato;
			do
			{
				Console.WriteLine(mensaje);

			} while (!int.TryParse(Console.ReadLine(),out dato ));

			return dato;
		}

		private static string pedirString(string mensaje)
		{
			String dato;
			do
			{
				Console.WriteLine(mensaje);
				dato = Console.ReadLine();

			} while (string.IsNullOrEmpty(dato));

			return dato;
		}

		//Muestra los datos de un elemento formateados en consola
		private static void mostrarElemento(Elemento elemento, int fila)
		{
			Console.SetCursorPosition(40, fila);
			Console.WriteLine(String.Format("Descubrimiento {0} {1}, {2}",
				elemento.Nombre, elemento.SimboloQuimico, elemento.NumeroAtomico));

			Console.SetCursorPosition(40, fila + 1);
			Console.Write(elemento);

			Console.SetCursorPosition(40, fila + 2);
			Console.Write("Pulse una tecla para continuar...");
			Console.ReadKey();
		}

		//Métodos auxiliares para Algunos cálculos parciales de los métodos previos

		//Obtiene cuántos elementos se han descubierto en un siglo determinado
		private static int getNumeroElementosSiglo(List<Elemento>  elementos, int siglo)
		{
			int anyoFin = siglo * 100;
			int anyoInicio = anyoFin - 100;
			int numeroElementos = 0;

			if (siglo == 0)
				numeroElementos = getNumElementosAC(elementos);
			else
				for(int anyoActual = anyoInicio; anyoActual < anyoFin; anyoActual++)
					numeroElementos += getNumElementosAnyo(elementos, anyoActual);

			return numeroElementos;
				
		}

		//Muestra los datos de los elementos descubiertos en un año determinado
		public static void DetalleAnyoElemento(List<Elemento> elementos, int anyo)
		{
			bool existe = false;
			int filaActual = 2;
			foreach(Elemento elemento in elementos)
			{
				if(elemento.AnyoDescubrimiento.Year == anyo && !elemento.ElementoAntiguo)
				{
					existe = true;
					mostrarElemento(elemento, filaActual);
					filaActual += 4;
				}
				else if(anyo == -1 && elemento.ElementoAntiguo)
				{
					existe = true;
					mostrarElemento(elemento, filaActual);
					filaActual += 4;
				}
			}
			if (!existe)
			{
				Console.Clear();
				Console.WriteLine("No hay elementos para el año seleccionado.");
				Console.WriteLine("Pulse una tecla para continuar...");
				Console.ReadKey();
			}
		}

		/**
		 * Añade un nuevo elemento a la coleccion, verificando que los datos que
		 * introduce el usuario son correctos
		 */

		public static Elemento PedirElemento()
		{
			Elemento elemento = null;
			String[] tipos = { "1. Metal", "2. No Metal", "3. Metaloide" };

			string opcion;

			Console.Clear();
			Console.WriteLine("1. Metal, 2. No Metal, 3. Metaloide\n");
			opcion = Console.ReadLine();

			if(opcion == "1" || opcion == "2" || opcion == "3")
			{
				string nombre = pedirString("Introduce el nombre: ");
				string simboloQuimico = pedirString("Introduce el símbolo químico: ");
				int numeroAtomico = pedirEntero("Introduce número atómico: ");
				// Generamos la fecha como el 1 de enero del año introducido (el dia y mes no importan)
				DateTime anyoDescubrimiento = new DateTime(
					pedirEntero("Intreoduce el año de descubrimiento: "), 1, 1);
				bool elementoAntiguo = pedirBooleano("¿Es un elemento antiguo?");
				//Variables necesarias para pedir los siguientes datos
				string color;
				bool esLiquido;
				bool esMetalNoble;
				bool esGasNoble;
				bool esMetalRefractario;

				switch (opcion)
				{
					case "1":
						{
							esLiquido = pedirBooleano("¿Es líquido?");
							color = pedirString("Introduce el color: ");
							esMetalNoble = pedirBooleano("¿Es metal noble?");
							//Añadimos un nuevo Elemento Metal
							elemento = new Metal(nombre, simboloQuimico, numeroAtomico,
								anyoDescubrimiento, elementoAntiguo, esLiquido,
								color, esMetalNoble);
							break;
						}

					case "2":
						{
							esGasNoble = pedirBooleano("¿Es gas noble?");
							//Añadimos un nuevo Elemento No Metal
							elemento = new NoMetal(nombre, simboloQuimico,
								numeroAtomico, anyoDescubrimiento, elementoAntiguo,
								esGasNoble);
							break;
						}

					case "3":
						{
							esMetalRefractario = pedirBooleano("¿Es metal refractario?");
							esMetalNoble = pedirBooleano("¿Es metal noble?");
							//Añadimos un nuevo elemento Metaloide
							elemento = new Metaloide(nombre, simboloQuimico,
								numeroAtomico, anyoDescubrimiento, elementoAntiguo,
								esMetalRefractario, esMetalNoble);
							break;
						}
				}//Final Switch
			}
			return elemento;
		}

		public static void MostrarTodosLosSiglos(List<Elemento> elementos)
		{
			Console.Clear();
			for (int siglo = 0; siglo <= 21; siglo++)
				Console.WriteLine((siglo == 0 ? "Edad Antigua" : "Siglo: " + siglo) +
					 ":" + getNumeroElementosSiglo(elementos, siglo) + " elementos");

		}

		public static void MostarSiglo(List<Elemento> elementos,int siglo)
		{
			int anyoFin = (siglo * 1000) - 1;
			int anyoInicio = anyoFin + 1 - 1000;
			DateTime hoy = DateTime.Now;

			if(anyoInicio <= hoy.Year && hoy.Year <= anyoFin)
				anyoFin = hoy.Year;

			Console.WriteLine("{0}\n", siglo == 0 ? "Edad Antigua" : "Siglo" + siglo);

			if(siglo != 0)
			{
				int contador = 0;
				for(int anyoActual = anyoInicio; anyoActual <= anyoFin; anyoActual++)
				{
					mostrarColorElemnto(elementos, anyoActual);
					contador++;
					if(contador == 5)
					{
						contador = 5;
						Console.WriteLine();
					}
				}
			}
			Console.WriteLine();
		}

		public static void MostrarSigloActual(List<Elemento> elementos)
		{
			Console.Clear();
			MostarSiglo(elementos, 21);
		}
	}
}
