using System;
using System.IO;

namespace LectorEventos
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = "";
            string archivo = "prueba.csv";
            if (args.Length > 0)
                archivo = args[0];

            string evento = "", fecha = "";

            try
            {
                using (StreamReader rdr = new StreamReader(archivo))
                {
                    string linea;
                    while (null != (linea = rdr.ReadLine()))
                    {
                        string[] columnas = linea.Split(new char[] { ',' });

                        if (columnas.Length >= 2)
                        {
                            evento = columnas[0];
                            fecha = columnas[1];
                            msg += "\n El evento " + evento + ObtenerTiempoPalabras(fecha);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }

            Console.WriteLine(msg);
        }

        public static string ObtenerTiempoPalabras(string fecha)
        {
            string v = "";
            DateTime dt;
            if (DateTime.TryParse(fecha, out dt))
                v =String.Format("  Converted '{0}' to {1} ({2}).", fecha, dt, dt.Kind);
            else
                v = String.Format("  Fecha invalida '{0}'.", fecha);



            DateTime ahora = DateTime.Now;


            if ((dt - ahora).TotalSeconds > 0)
            {
                if ((dt - ahora).TotalDays >= 30)
                {
                    v = " ocurrira en " + Math.Floor(Math.Abs(((dt - ahora).TotalDays / 30))).ToString() + " meses";
                }
                else
                {
                    v = " ocurrira en " + Math.Floor(Math.Abs((dt - ahora).TotalDays)).ToString() + " dias";
                }
                if ((dt - ahora).TotalDays < 1)
                    v = " ocurrira en " + Math.Ceiling(Math.Abs((dt - ahora).TotalHours)).ToString() + " horas";
                if ((dt - ahora).TotalHours < 1)
                    v = " ocurrira en " + Math.Ceiling(Math.Abs((dt - ahora).TotalMinutes)).ToString() + " minutos";
            } else
            {
                if ((ahora - dt).TotalDays >= 30)
                {
                    v = " ocurrio hace " + Math.Floor(Math.Abs(((dt - ahora).TotalDays / 30))).ToString() + " meses";
                }
                else
                {
                    v = " ocurrio hace " + Math.Floor(Math.Abs((dt - ahora).TotalDays)).ToString() + " dias";
                }
                if ((ahora - dt).TotalDays < 1)
                    v = " ocurrio hace " + Math.Floor(Math.Abs((dt - ahora).TotalHours)).ToString() + " horas";
                if ((ahora - dt).TotalHours < 1)
                    v = " ocurrio hace " + Math.Floor(Math.Abs((dt - ahora).TotalMinutes)).ToString() + " minutos";
            }
            return v;
        }
    }
}
