﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marcas> ListadodeMarcas()
        {
            List<Marcas> Lista = new List<Marcas>();
            Acceso_a_datos datos = new Acceso_a_datos();


            try
            {
                datos.Setearconsulta ("select Id,Descripcion from MARCAS");
                datos.Ejecutarlectura();

                while(datos.Lector.Read())
                {
                    Marcas aux = new Marcas();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Descripcion = (String)datos.Lector["descripcion"];

                    Lista.Add(aux);
                }
                    
                return Lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarconexion();
            }

        }

    }
}
