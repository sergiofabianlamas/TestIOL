/*
 * Refactorear la clase para respetar principios de programación orientada a objetos. Qué pasa si debemos soportar un nuevo idioma para los reportes, o
 * agregar más formas geométricas?
 *
 * Se puede hacer cualquier cambio que se crea necesario tanto en el código como en los tests. La única condición es que los tests pasen OK.
 *
 * TODO: Implementar Trapecio/Rectangulo, agregar otro idioma a reporting.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingChallenge.Data.Classes
{
    /// <summary>
    /// Clase que permite leer los mensajes usados por el sistema. Se resolvio de esta manera el seteo de la mensajeria. En el caso de necesitar más idiomas, se agregaría una columna más en el array, con la traducción correspondiente
    /// </summary>
    public class Mensaje
    {
        #region Mensajeria

        /// <summary>
        /// Array de mensajes que usa el sistema. En la 1era columna el mensaje esta en español; en la 2da columna el mensaje esta en inglés
        /// </summary>
        public string[,] ListaMensajes = new string[6, 2]
        {
            {"<h1>Lista vacía de formas!</h1>","<h1>Empty list of shapes!</h1>"},
            {"<h1>Reporte de Formas</h1>","<h1>Shapes report</h1>"},
            {"<h1>Alguno de los valores es 0</h1>","<h1>one of the values is 0</h1>"},
            {" Trapecio "," Trapezoid "},
            {" Area "," Area "},
            {" Perimetro "," Perimeter "}
        };

        #endregion
    }


    public class FormaGeometrica
    {     
     
        #region Formas

        public const int Cuadrado = 1;
        public const int TrianguloEquilatero = 2;
        public const int Circulo = 3;
        public const int Trapecio = 4;

        #endregion

        #region Idiomas

        public const int Castellano = 1;
        public const int Ingles = 2;

        #endregion

        private readonly decimal _lado;

        public int Tipo { get; set; }

        public FormaGeometrica(int tipo, decimal ancho)
        {
            Tipo = tipo;
            _lado = ancho;
        }

        
        public static string Imprimir(List<FormaGeometrica> formas, int idioma)
        {
            var sb = new StringBuilder();
            //nueva instancia de la clase mensaje
            var prueba = new Mensaje();

            if (!formas.Any())
            {
                //if (idioma == Castellano)
                //    sb.Append("<h1>Lista vacía de formas!</h1>");

                //else
                //    sb.Append("<h1>Empty list of shapes!</h1>");

                //SFL 27/09/2023: Se reemplaza el uso de texto del mensaje, por el uso de la clase mensajes. Se resta en uno el valor del idioma, para no tener que alterar los valores del TEST.
                sb.Append(prueba.ListaMensajes[0, idioma - 1]);
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                //if (idioma == Castellano)
                //    sb.Append("<h1>Reporte de Formas</h1>");
                //else
                //    // default es inglés
                //    sb.Append("<h1>Shapes report</h1>");
                //SFL 27/09/2023: Se reemplaza el uso de texto del mensaje, por el uso de la clase mensajes. Se resta en uno el valor del idioma, para no tener que alterar los valores del TEST.
                sb.Append(prueba.ListaMensajes[1, idioma - 1]);

                var numeroCuadrados = 0;
                var numeroCirculos = 0;
                var numeroTriangulos = 0;

                var areaCuadrados = 0m;
                var areaCirculos = 0m;
                var areaTriangulos = 0m;

                var perimetroCuadrados = 0m;
                var perimetroCirculos = 0m;
                var perimetroTriangulos = 0m;

                for (var i = 0; i < formas.Count; i++)
                {
                    if (formas[i].Tipo == Cuadrado)
                    {
                        numeroCuadrados++;
                        areaCuadrados += formas[i].CalcularArea();
                        perimetroCuadrados += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == Circulo)
                    {
                        numeroCirculos++;
                        areaCirculos += formas[i].CalcularArea();
                        perimetroCirculos += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == TrianguloEquilatero)
                    {
                        numeroTriangulos++;
                        areaTriangulos += formas[i].CalcularArea();
                        perimetroTriangulos += formas[i].CalcularPerimetro();
                    }
                }
                
                sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, Cuadrado, idioma));
                sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, Circulo, idioma));
                sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, TrianguloEquilatero, idioma));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + " " + (idioma == Castellano ? "formas" : "shapes") + " ");
                sb.Append((idioma == Castellano ? "Perimetro " : "Perimeter ") + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos).ToString("#.##") + " ");
                sb.Append("Area " + (areaCuadrados + areaCirculos + areaTriangulos).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, int tipo, int idioma)
        {
            if (cantidad > 0)
            {
                if (idioma == Castellano)
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>";

                return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";
            }

            return string.Empty;
        }

        private static string TraducirForma(int tipo, int cantidad, int idioma)
        {
            switch (tipo)
            {
                case Cuadrado:
                    if (idioma == Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
                    else return cantidad == 1 ? "Square" : "Squares";
                case Circulo:
                    if (idioma == Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
                    else return cantidad == 1 ? "Circle" : "Circles";
                case TrianguloEquilatero:
                    if (idioma == Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
                    else return cantidad == 1 ? "Triangle" : "Triangles";
            }

            return string.Empty;
        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case Cuadrado: return _lado * _lado;
                case Circulo: return (decimal)Math.PI * (_lado / 2) * (_lado / 2);
                case TrianguloEquilatero: return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }

        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case Cuadrado: return _lado * 4;
                case Circulo: return (decimal)Math.PI * _lado;
                case TrianguloEquilatero: return _lado * 3;
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }

    }


    /// <summary>
    /// Se crea una clase trapecio, donde exclusivamente se calculan los valores para esta forma geometrica. La idea es que por cada figura geometrica particular o irrgular posea su propia clase, ya que el calculo varia bastante de una figura regular, y necesita mucha mas información que esta última.
    /// </summary>
    public class Trapecio
    {

        #region Idiomas

        public const int Castellano = 1;
        public const int Ingles = 2;

        #endregion

        #region Valores

        private decimal _LadoBaseA = 0;
        private decimal _LadoBaseB = 0;
        private decimal _LadoAltura = 0;
        private decimal _Altura = 0;

        public decimal LadoBaseA
        {
            get => _LadoBaseA;
            set => _LadoBaseA = value;
        }

        public decimal LadoBaseB
        {
            get => _LadoBaseB;
            set => _LadoBaseB = value;
        }

        public decimal LadoAltura
        {
            get => _LadoAltura;
            set => _LadoAltura = value;
        }

        public decimal Altura
        {
            get => _Altura;
            set => _Altura = value;
        }

        #endregion

        public int Tipo { get; set; }

        public string Imprimir(int idioma)
        {
            var sb = new StringBuilder();
            var prueba = new Mensaje();

            if (_LadoBaseA == 0 ^ _LadoBaseB == 0 ^_LadoAltura == 0 ^ Altura == 0)
            {
                sb.Append(prueba.ListaMensajes[2, idioma - 1]);
            }
            else
            {
                sb.Append(prueba.ListaMensajes[1, idioma - 1]);

                var perimetro = (_LadoAltura*2) + _LadoBaseA + _LadoBaseB;
                var area = (_LadoBaseA+_LadoBaseB)/2*_Altura;

                sb.Append(prueba.ListaMensajes[3, idioma - 1] + " | " + prueba.ListaMensajes[4, idioma - 1] + area + " | " + prueba.ListaMensajes[5, idioma - 1] + perimetro);
            }

            return sb.ToString();
        }

    }

}
