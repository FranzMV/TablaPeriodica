using System;

//Subclase de Elemento que hereda todos sus atributos  y define los suyos propios
namespace TablaPeriodica
{
	public class Metal: Elemento
	{ 
		//Variables de instancia
		private bool esLiquido;
		private string color;
		private bool esMetalNoble;

		//Constructor con parámetros
		public Metal(string nombre,string simboloQuimico,int numeroAtomico,
			DateTime anyoDescubrimiento, bool elementoAntiguo, bool esLiquido,
			string color, bool esMetalNoble)
			:base(nombre,simboloQuimico,numeroAtomico,anyoDescubrimiento,elementoAntiguo)
		{
			this.esLiquido = esLiquido;
			this.color = color;
			this.esMetalNoble = esMetalNoble;
		}

		//Métodos de acceso get/set a las variables de instancia
		public bool EsLiquido
		{
			get => esLiquido;
			set => esLiquido = value;
		}

		public string Color
		{
			get => color;
			set => color = value;
		}

		public bool EsMetalNoble
		{
			get => esMetalNoble;
			set => esMetalNoble = value;
		}

		//Método para mostrar la información en pantalla
		public override String ToString() => string.Format("{0}. {1}. {2};",
			esLiquido ? "Liquido" : "No Liquido",color,
			esMetalNoble ? "Metal Noble" : "Metal No Noble");

		//Sobreescribimos el método de la clase Padre para añadir al informacion al fichero
		public override string DatosFichero() => string.Format("{0};{1};{2};{3}",
			base.DatosFichero(), esLiquido ? "Liquido" : "No", color,
			esMetalNoble ? "Noble" : "No Noble");
		
	}
}
