using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;


namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaApiPruebaContext context = new DL.FlunaApiPruebaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}' ,'{usuario.Apellido}' , '{usuario.Email}' , '{usuario.UserName}' , '{usuario.Password}´");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesaage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesaage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaApiPruebaContext context = new DL.FlunaApiPruebaContext())
                {
                    var query = context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();
                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario()
                            {
                                IdUsuario = obj.IdUsuario,
                                Nombre = obj.Nombre,
                                Apellido = obj.Apellido,
                                Email = obj.Email,
                                UserName = obj.UserName,
                                Password = obj.Password
                            };
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesaage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesaage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaApiPruebaContext context = new DL.FlunaApiPruebaContext())
                {
                    var usuario = context.Usuarios.FirstOrDefault(e => e.IdUsuario == IdUsuario);
                    if(usuario == null)
                    {
                        context.Usuarios.Remove(usuario);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesaage = "No se encontró el empleado para eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesaage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaApiPruebaContext context = new DL.FlunaApiPruebaContext())
                {
                    var objquery = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    if(objquery != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objquery.IdUsuario;
                        usuario.Nombre = objquery.Nombre;
                        usuario.Apellido = objquery.Apellido;
                        usuario.Email = objquery.Email;
                        usuario.UserName = objquery.UserName;
                        usuario.Password = objquery.Password;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesaage = "No se pudo completar los registros de la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesaage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaApiPruebaContext context = new DL.FlunaApiPruebaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario } , '{usuario.Nombre}' ,'{usuario.Apellido}' , '{usuario.Email}' , '{usuario.UserName}' , '{usuario.Password}´");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesaage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesaage = ex.Message;
            }
            return result;
        }
    }
}
