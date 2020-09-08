using System;

//Subclase de Elemento que hereda todos sus atributos y define los suyos propios
namespace TablaPeriodica
{
	public class Metaloide : Elemento
	{
		//Variables de instancia
		private bool esMetalRefractario;
		private bool esMetalNoble;

		//Constructor con parámetros
		public Metaloide(string nombre, string simboloQuimico, int numeroAtomico,
			DateTime anyoDescubrimiento, bool elementoAntiguo, bool esMetalRefractario,
			bool esMetalNoble):
			base(nombre, simboloQuimico, numeroAtomico, anyoDescubrimiento, elementoAntiguo)
		{
			this.esMetalRefractario = esMetalRefractario;
			this.esMetalNoble = esMetalNoble;
		}

		//Métodos de acceso get/ser a las variables de instancia
		public bool EsMetalRefractario
		{
			get => esMetalRefractario;
			set => esMetalRefractario = value;
		}

		public bool EsMetalNoble
		{
			get => esMetalNoble;
			set => esMetalNoble = value;
		}

		//Método para mostrar la información en pantalla
		public override string ToString() => string.Format("{0}. {1}.",
			(esMetalRefractario ? "Refractario" : "No Refractario"),
			(esMetalNoble ? "Noble" : "No Noble"));

		//Sobreescribimos el método de la clase Padre para añadir al informacion al fichero
		public override string DatosFichero() => string.Format("{0};{1};{2}.",
			base.DatosFichero(), esMetalRefractario ? "Sí" : "No",
			esMetalNoble ? "Noble" : "No Noble");
		
	}
}
