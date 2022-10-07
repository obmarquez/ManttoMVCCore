using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ManttoMVCCore.Models;
using System.Threading.Tasks;

namespace ManttoMVCCore.Data
{
    public class DBOperaciones
    {
        //consultando equipos
        public IEnumerable<T> Get<T>(string sp)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, null, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        //consultando con dos parametros enteros
        public IEnumerable<T> Getdosparam<T>(string sp, int opcion, int id)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, new { @opcion = opcion, @id = id }, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        //para ejecutar procedimientos almacenados con modelos o n parametros
        public IEnumerable<T> Getdosparam1<T>(string sp, object param)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, param, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        public IEnumerable<T> Getunparam<T>(string sp, int opcion)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, new { @opcion = opcion }, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }

        #region Marcas
        public string AgregaActualizaMarca(MarcasViewModel marcasModel)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;

            try
            {
                cnn = new DBConnection().Open();
                cnn.Execute("sp_agrega_actualiza_marcas", marcasModel, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        #region Areas
        public string AgregaActualizaArea(AreaViewModel areaModel)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;

            try
            {
                cnn = new DBConnection().Open();
                cnn.Execute("sp_agrega_actualiza_areas", areaModel, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        #region Resguardante
        public string AgregaActualizaResguardante(ResguardanteViewModel resguardanteModel)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;

            try
            {
                cnn = new DBConnection().Open();
                cnn.Execute("sp_agrega_actualiza_resguardante", resguardanteModel, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        #region Testigos
        public string AgregaActualizaTestigos(TestigoViewModel testigoModel)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;

            try
            {
                cnn = new DBConnection().Open();
                cnn.Execute("sp_agrega_actualiza_testigos", testigoModel, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        #region Tipoequipo
        public string AgregaActualizaTipoEquipo(TipoequipoViewModel tipoModel)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;

            try
            {
                cnn = new DBConnection().Open();
                cnn.Execute("sp_agrega_actualiza_tipoequipo", tipoModel, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        #region Servicios
        public string AgregaActualizaServicios(Servicio servicioModel)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;

            try
            {
                cnn = new DBConnection().Open();
                cnn.Execute("sp_agrega_actualiza_servicios", servicioModel, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch(Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        #region Resguardo
        public string DesactivaResguardo(int opcion, int id)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;
            try
            {
                cnn = new DBConnection().Open();
                cnn.Execute("sp_resguardo_desactiva_id", new { @id = opcion }, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        #region Mantenimiento
        public string AgregaActualizaMantenimiento(MantenimientoViewModel mantenimiento)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;

            try
            {
                cnn = new DBConnection().Open();
                //mensaje = cnn.Execute("sp_agrega_actualiza_marca", marcasModel, null, true, null, CommandType.StoredProcedure);
                cnn.Execute("sp_agrega_actualiza_manteninimiento", mantenimiento, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        #region Soporte
        public string AgregaActualizaSoporte(SoporteModel soporte)
        {
            IDbConnection cnn = null;
            string mensaje = string.Empty;

            try
            {
                cnn = new DBConnection().Open();
                //mensaje = cnn.Execute("sp_agrega_actualiza_marca", marcasModel, null, true, null, CommandType.StoredProcedure);
                cnn.Execute("sp_agrega_actualiza_soporte", soporte, commandType: CommandType.StoredProcedure);
                mensaje = "Ok";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return mensaje;
        }
        #endregion

        //#region SetEncabezado
        //public int SetEncabezado(ResguardoViewModel resguardoViewModel)
        //{
        //    IDbConnection cnn = null;
        //    int newID = 0;

        //    try
        //    {
        //        cnn = new DBConnection().Open();

        //        var p = new DynamicParameters();
        //        p.Add("id", resguardoViewModel.id);
        //        p.Add("fecha", resguardoViewModel.fecha);
        //        p.Add("activo", 1);
        //        p.Add("recibe", resguardoViewModel.recibe);
        //        p.Add("testigo", resguardoViewModel.testigo);
        //        p.Add("idequipo", resguardoViewModel.idequipo);
        //        p.Add("observaciones", resguardoViewModel.observaciones);
        //        p.Add("nextResguardo", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //        cnn.Query<int>("sp_OBM_AgregaResguardo", p, commandType: CommandType.StoredProcedure);
        //        newID = p.Get<int>("nextResguardo");

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        CloseConnection(cnn);
        //    }
        //    return newID;
        //}
        //#endregion

        //#region SetDetalle
        //public string SetDetalle(ResguardodetalleViewModel resguardodetalleViewModel)
        //{
        //    IDbConnection cnn = null;
        //    string mensaje = string.Empty;

        //    try
        //    {
        //        cnn = new DBConnection().Open();
        //        cnn.Execute("sp_OBM_AgregaResguardoDetalle", resguardodetalleViewModel, commandType: CommandType.StoredProcedure);
        //        mensaje = "Ok";
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        CloseConnection(cnn);
        //    }
        //    return mensaje;
        //}
        //#endregion

        #region Reportes
        public IEnumerable<T> GetReportes<T>(string sp, int opcion)
        {
            IDbConnection cnn = null;
            var result = default(List<T>);
            try
            {
                cnn = new DBConnection().Open();
                result = cnn.Query<T>(sp, new { @opcion = opcion }, null, true, null, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CloseConnection(cnn);
            }
            return result;
        }
        #endregion

        protected bool CloseConnection(IDbConnection cnn)
        {
            if (cnn != null)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                    return true;
                }
            }
            return false;
        }
    }
}
