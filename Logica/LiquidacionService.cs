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
    }
}
