using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TablaPeriodica
{
	//Clase encargada de gestionar la carga y almacenamiento de elementos en
	//los correspondientes ficheros

	public static class GestorElementos
	{
		private const string NOMBREFICHERO = "elementos.txt";
		private const char DELIMITADOR = ';';

		//Método encargado de alamacenar los datos de la Lista de elementos en un fichero
		public static List<Elemento> CargarDatos()
		{
			//Colección donde almacenaremos la información de los elementos
			List<Elemento> elementos = new List<Elemento>();

			//Comprobamos si existe el fichero
			if (File.Exists(NOMBREFICHERO)) {

				StreamReader fichero = File.OpenText(NOMBREFICHERO);
			
				using(StreamReader lectura = new StreamReader(NOMBREFICHERO, Encoding.Default))
				{
					string linea;
					do
					{
						linea = lectura.ReadLine();
						if(linea != null)
						{
							try
							{
								string[] datosSeparados = linea.Split(DELIMITADOR);
								string nombre = datosSeparados[1];
								string simboloQuimico = datosSeparados[2];
								Int32.TryParse(datosSeparados[3], out int numeroAtomico);

								DateTime anyoDescubrimiento = new DateTime();
								bool elementoAntiguo = datosSeparados[4] ==
									"A.C." ? true : false;
								if (!elementoAntiguo)
								{
									if (Int32.TryParse(datosSeparados[4],
										out int anyo))
										anyoDescubrimiento = anyoDescubrimiento.
											AddYears(anyo - 1);
								}

								bool esLiquido;
								string color;
								bool esMetalNoble;
								bool esMetalRefractario;
								bool esGasNoble;

								switch (datosSeparados[0])
								{
									case "Metal":
										{
											esLiquido = datosSeparados[5]
												== "Liquido" ? true : false;
											color = datosSeparados[6];
											esMetalNoble = datosSeparados[7]
												== " Metal Noble" ? true : false;
											//Añadimos un nuevo Metal
											elementos.Add(new Metal(nombre, simboloQuimico,
												numeroAtomico, anyoDescubrimiento,
												elementoAntiguo, esLiquido, color,
												esMetalNoble));
											break;
										}

									case "Metaloide":
										{
											esMetalRefractario = datosSeparados[5]
												== "Si" ? true : false;
											esMetalNoble = datosSeparados[6]
												== "Metal Noble" ? true : false;
											//Añadimos un nuevo Elemento Metaloide
											elementos.Add(new Metaloide(nombre,
												simboloQuimico, numeroAtomico,
												anyoDescubrimiento, elementoAntiguo,
												esMetalRefractario, esMetalNoble));
											break;
										}

									case "NoMetal":
										{
											esGasNoble = datosSeparados[5]
												== "Gas Noble" ? true : false;
											//Añadimos un nuevo Elemento NoMetal
											elementos.Add(new NoMetal(nombre,
												simboloQuimico, numeroAtomico,
												anyoDescubrimiento, elementoAntiguo,
												esGasNoble));
											break;
										}
								}//Final Switch

							}//Capturamos posible excepciones
							catch (IOException ex)
							{
								Console.Clear();
								Console.WriteLine("Error al leer el fichero:{0}",
									ex.Message);
								Console.ReadKey();
							}
						}

					} while (linea!=null);
					Console.WriteLine("Datos leídos correctamente.");
				} //Cerramos el fichero de forma automática	
			}
			return elementos;
		}

		//Método para guardar los datos de los elementos en el fichero
		public static void GuardarDatos(List<Elemento> elementos)
		{
			try
			{
				//Abrimos el fichero en modo Edición y almacemos los datos de la
				//Lista de Elementos
				using(StreamWriter fichero = new StreamWriter(NOMBREFICHERO))
				{
					foreach (Elemento e in elementos)
						fichero.WriteLine(e.DatosFichero());
				}//Cerramos el fichero de forma automática
			}
			catch (IOException ex)
			{
				Console.WriteLine("Error {0}", ex.Message);
			}
			catch(Exception ex)
			{
				Console.WriteLine("Error {0}", ex.Message);
			}
		}
	}
}
