using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Sebastian_Suma2Numeros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SebastianApesaController : ControllerBase
    {
        [HttpGet]
        public int Sumar(int num1, int num2)
        {
            return num1 + num2;
        }

        [HttpPost] 
        public IActionResult GuardarDatos(string nombreCompleto, string fechaNacimiento, string correoElectronico)
        {
            // Validaciones
            if (string.IsNullOrEmpty(nombreCompleto) || string.IsNullOrEmpty(fechaNacimiento) || string.IsNullOrEmpty(correoElectronico))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            if (Regex.IsMatch(nombreCompleto, @"\d"))
            {
                return BadRequest("El nombre completo no debe contener números.");
            }

            string[] formatosFecha = { "dd-MM-yyyy" };
            DateTime fechaNacimientoDateTime;
            if (!DateTime.TryParseExact(fechaNacimiento, formatosFecha, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNacimientoDateTime))
            {
                return BadRequest("Formato de fecha de nacimiento inválido. Debe ser dd-MM-yyyy.");
            }

            if (!EsCorreoElectronicoValido(correoElectronico))
            {
                return BadRequest("Formato de correo electrónico inválido.");
            }

            string datos = $"{nombreCompleto},{fechaNacimiento},{correoElectronico}{Environment.NewLine}";

            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string path = Path.Combine(escritorio, "Sebastian_DatosPersonales.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(path, append: true))
                {
                    writer.WriteLine(datos);
                }

                return Ok("Datos guardados correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al guardar los datos: " + ex.Message);
            }
     
        }
        private bool EsCorreoElectronicoValido(string correo)
        {
            string patronCorreoElectronico = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(correo, patronCorreoElectronico);
        }
    }
}
