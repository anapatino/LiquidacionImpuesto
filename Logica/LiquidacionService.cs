using System;
using Datos;
using Entidad;

namespace Logica
{
    public class LiquidacionService
    {
        readonly LiquidacionRepository liquidacionRepository;
        public LiquidacionService()
        {
            liquidacionRepository = new LiquidacionRepository();
        }
        public string Guarda(LiquidacionImpuesto persona)
        {
            try
            {
                liquidacionRepository.Guardar(persona);
                return "Se guardo el registro Satisfactoriamente";
            }
            catch (Exception e)
            {
                return $"Error inesperado al Guardar: {e.Message}";
            }
        }
        public LiquidacionConsultaResponse Consultar()
        {
            try
            {
                return new LiquidacionConsultaResponse(liquidacionRepository.Consultar());
            }
            catch (Exception e)
            {

                return new LiquidacionConsultaResponse($"Error inesperado al Consultar: {e.Message}");
            }
        }
        public string Eliminar(long identificacion)
        {
            try
            {
                if (liquidacionRepository.Buscar(identificacion) != null)
                {
                    liquidacionRepository.Eliminar(identificacion);
                    return $"Se Eliminó el registro de la persona con Nro.Liquidacion ({identificacion})";
                }
                return $"No fue posible eliminar el registro, porque no existe la persona con Nro.Liquidacion ({identificacion})";
            }
            catch (Exception e)
            {
                return $"Error inesperado al Eliminar: {e.Message}";
            }

        }
    }
}
