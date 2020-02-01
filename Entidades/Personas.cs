using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using RegistroInscripcion.Entidades;
using RegistroInscripcion.BLL;


namespace RegistroInscripcion.Entidades
{
    public class Personas
    {
        [Key]
        public int PersonaID { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public decimal Balance { get; set; }


        public Personas()
        {
            PersonaID = 0;
            Nombre = string.Empty;
            Cedula = string.Empty;
            Telefono = string.Empty;
            Direccion = string.Empty;
            FechaNacimiento = DateTime.Now;
            Balance = 0;
        }
    }
}
