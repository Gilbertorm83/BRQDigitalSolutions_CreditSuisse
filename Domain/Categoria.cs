using System;
namespace Domain
{
    interface ICategoria
    {
        Negocio ClassificaNegocio(Negocio objNegocio, DateTime sReferenceDate);
    }

    public class Categoria : ICategoria
    {
        public Negocio ClassificaNegocio(Negocio objNegocio, DateTime sReferenceDate)
        {
            //EXPIRADO (negócios cuja próxima data de pagamento esteja atrasada por mais de 30 dias com base em uma data de referência que será informada)
            if ((sReferenceDate - objNegocio.NextPaymentDate).TotalDays > 30)
            {
                objNegocio.DescricaoCategoria = "EXPIRADO";
                return objNegocio;
            }

            //ALTO RISCO (negócios com valor superior a 1.000.000 e cliente do setor privado)
            if (objNegocio.Value > 1000000 && objNegocio.ClientSector.ToUpper() == "PRIVADO")
            {
                objNegocio.DescricaoCategoria = "ALTO RISCO";
                return objNegocio;
            }


            //MÉDIO RISCO (operações com valor superior a 1.000.000 e cliente do setor público)
            if (objNegocio.Value > 1000000 && objNegocio.ClientSector.ToUpper() == "PÚBLICO")
            {
                objNegocio.DescricaoCategoria = "MÉDIO RISCO";
                return objNegocio;
            }

            objNegocio.DescricaoCategoria = "INDEFINIDO";
            return objNegocio;
        }
    }
}

