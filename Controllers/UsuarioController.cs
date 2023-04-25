using Microsoft.AspNetCore.Mvc;

namespace Taller4.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public static List<Usuario> ListUsers = new List<Usuario>()
        {
           new Usuario
           {
               Id = 1,
               Nombre = "Ana",
               Apellido = "Pérez",
               FechaNacimiento = new DateTime(1990, 5, 15),
               Telefono = "+34 123456789",
               Direccion = "Calle Falsa 123, Madrid, España"
           },

           new Usuario
           {
               Id = 2,
               Nombre = "Pedro",
               Apellido = "García",
               FechaNacimiento = new DateTime(1985, 9, 18),
               Telefono = "+52 5555555555",
               Direccion = "Av. Siempre Viva 742, Ciudad de México, México"
           }
        };


        [HttpGet("Listar")]
        public List<Usuario> ListarUsuarios()
        {
            return ListUsers;
        }

        [HttpGet("Item")]
        public Usuario Item(int ID)
        { 
            return ListUsers[ID]; 
        }

        [HttpGet("Detalle")]
        public dynamic Detail(int ID)
        {
            var hdr_key = Request.Headers.Where(x => x.Key.Equals("key_app")).FirstOrDefault();

            if (hdr_key.Value.Count == 0)
            {
                return new
                {
                    code = "API ERROR",
                    message = "NO ESTÁ AUTORIZADO",
                    Detail = "N/A"

                };
            }
            else
            {
                if (hdr_key.Value != "x1234")
                {
                    return new
                    {
                        code = "API ERROR",
                        message = "KEY INVALIDO",
                        Detail = "N/A"

                    };

                }
            }
            var item = ListUsers.Where(x => x.Id == ID).ToList();
            if(item.Count > 0)
            {
                if (ID == 0)
                {
                    return new
                    {
                        code = "OK",
                        message = "SE ENCONTRO EL USUARIO",
                        Detail = "N/A"
                    };
                }
                else
                {
                    return item;
                }
            }
            else
            {
                return new
                {
                    code = "API COUNT",
                    message = "NO HAY USUARIO REGISTRADO CON ESE ID",
                    Detail = "N/A"
                };
            }
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] Usuario item)
        {
            //var hdr_key = Request.Headers["key_app"];
            var hdr_key = Request.Headers.Where(x => x.Key.Equals("key_app")).FirstOrDefault();

            if (hdr_key.Value.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "API001",
                    message = "Falta Parametro",
                    Detail = ""
                });


            }
            else
            {
                if (hdr_key.Value != "x1234")
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        code = "API002",
                        message = "No Autorizado",
                        Detail = ""
                    });
                }


                ListUsers.Add(item);
                return StatusCode(StatusCodes.Status201Created, new
                {
                    code = "OK",
                    message = "Datos Almacenados",
                    Detail = item
                });

            }

        }




        [HttpPut("update")]
        public IActionResult update([FromBody] Usuario item)
        {
            //var hdr_key = Request.Headers["key_app"];
            var hdr_key = Request.Headers.Where(x => x.Key.Equals("key_app")).FirstOrDefault();

            if (hdr_key.Value.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "API001",
                    message = "Falta Parametro",
                    Detail = ""
                });


            }
            else
            {
                if (hdr_key.Value != "x1234")
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        code = "API002",
                        message = "No Autorizado",
                        Detail = ""
                    });
                }



                foreach (var det in ListUsers.Where(x => x.Id == item.Id).ToList())
                {
                    det.Nombre = item.Nombre;
                }


                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Datos Modificados",
                    Detail = item
                });


            }

        }



        [HttpDelete("Delete")]
        public IActionResult delete(int id)
        {
            //var hdr_key = Request.Headers["key_app"];
            var hdr_key = Request.Headers.Where(x => x.Key.Equals("key_app")).FirstOrDefault();

            if (hdr_key.Value.Count == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "API001",
                    message = "Falta Parametro",
                    Detail = ""
                });


            }
            else
            {
                if (hdr_key.Value != "x1234")
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new
                    {
                        code = "API002",
                        message = "No Autorizado",
                        Detail = ""
                    });
                }


                if (id.Equals(0))
                {
                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        code = "OK",
                        message = "ID invalido",
                        Detail = "0"
                    });
                }
                else
                {

                    Usuario objprueba = (Usuario)ListUsers.Where(x => x.Id == id).First();
                    if (objprueba != null)
                        ListUsers.Remove(objprueba);

                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        code = "OK",
                        message = "Datos Eliminados",
                        Detail = id
                    });
                }






            }

        }




    }




}


