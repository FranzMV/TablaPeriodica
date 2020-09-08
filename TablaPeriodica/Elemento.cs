using System;

//Clase abstracta base que define las características generales de un elemento químico
namespace TablaPeriodica
{
	public abstract class Elemento
	{
		//Variables de instancia
		private string nombre;
		private string simboloQuimico;
		private int numeroAtomico;
		private DateTime anyoDescubrimiento;
		private bool elementoAntiguo;

		//Constructor con parámetros
		public Elemento(string nombre, string simboloQuimico,int numeroAtomico,
			DateTime anyoDescubrimiento, bool elementoAntiguo)
		{
			this.nombre = nombre;
			this.simboloQuimico = simboloQuimico;
			this.numeroAtomico = numeroAtomico;
			this.anyoDescubrimiento = anyoDescubrimiento;
			this.elementoAntiguo = elementoAntiguo;
		}

		//Métodos de acceso  get/set a las variables de instancia a través de Propiedades
		public string Nombre
		{
			get => nombre;
			set => nombre = value;
		}

		public string SimboloQuimico
		{
			get => simboloQuimico;
			set => simboloQuimico = value;
		}

		public int NumeroAtomico
		{
			get => numeroAtomico;
			set => numeroAtomico = value;
		}

		public DateTime AnyoDescubrimiento
		{
			get => anyoDescubrimiento;
			set => anyoDescubrimiento = value;
		}

		public bool ElementoAntiguo
		{
			get => elementoAntiguo;
			set => elementoAntiguo = value;
		}

		//Método para mostrar la información en el fichero
		public virtual string DatosFichero() => string.Format("{0};{1};{2};{3};{4}",
			GetType().ToString().Split('.')[1], nombre, simboloQuimico, numeroAtomico,
			elementoAntiguo? "A.C." : anyoDescubrimiento.ToString("yyyy"));
	}
}
