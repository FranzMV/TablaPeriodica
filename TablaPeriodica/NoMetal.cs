using System;
namespace TablaPeriodica
{
	//Subclase que hereada sus atributos de la clase padre Elemento y define uno propio
	public class NoMetal: Elemento
	{
		//Variable de instancia
		private bool esGasNoble;

		//Consotructor con parámetros
		public NoMetal(string nombre, string simboloQuimico, int numeroAtomico,
			DateTime anyoDescubrimiento, bool elementoAntiguo, bool esGasNoble) :
			base(nombre, simboloQuimico, numeroAtomico, anyoDescubrimiento, elementoAntiguo)
		{
			this.esGasNoble = esGasNoble;
		}

		//Método get/set de acceso a la variable de instancia
		public bool EsGasNoble
		{
			get => esGasNoble;
			set => esGasNoble = value;
		}

		//Método para mostrar la información en pantalla
		public override string ToString() => string.Format("{0}",
			(esGasNoble ? "Gas Noble" : "Gas No Noble"));

		//Sobreescribimos el método de la clase Padre para añadir al informacion al fichero
		public override string DatosFichero() => string.Format("{0};{1}.",
			base.DatosFichero(), esGasNoble ? "Gas Noble" : "No Noble");
	}
}
