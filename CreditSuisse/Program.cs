using System.Globalization;
using Domain;

DateTime sReferenceDate;
int iNumberTradesInPortfolio = 0;
string sThreeElements = "";
List<Negocio> lstNegocios = new List<Negocio>();

//Cabeçalho
Console.WriteLine("#####################");
Console.WriteLine("### CREDIT SUISSE ###");
Console.WriteLine("#####################");

//Data para próximo pagamento
Console.WriteLine("");
Console.WriteLine("Informe a data de referência (formato mm/dd/aaaa):");


//while (!DateTime.TryParse(Console.ReadLine(), out sReferenceDate))
while (!DateTime.TryParseExact(Console.ReadLine(), "MM'/'dd'/'yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out sReferenceDate))
{
    Console.WriteLine("");
    Console.WriteLine("Você deve informar uma data no formato mm/dd/aaaa.");
    Console.WriteLine("Informe a data de referência (formato mm/dd/aaaa):");
}

//Número de negócios no portfólio
Console.WriteLine("");
Console.WriteLine("Informe o número de negócios nesse portfólio:");
while (!int.TryParse(Console.ReadLine(), out iNumberTradesInPortfolio))
{
    Console.WriteLine("");
    Console.WriteLine("Você deve informar apenas números inteiros.");
    Console.WriteLine("Informe o número de negócios nesse portfólio:");
}

if (iNumberTradesInPortfolio > 0)
{
    //Se o número de negócios for maior que zero
    for (int i = 1; i <= iNumberTradesInPortfolio; i++)
    {
        bool bValidaLinha = false;
        while (!bValidaLinha)
        {
            Console.WriteLine("");
            Console.WriteLine("Negócio Número " + i + ") Informe Valor Negociação, Setor Cliente e Data Próximo Pagamento Pendente separando as 3 informações com espaço em branco:");
            sThreeElements = Console.ReadLine();
            
            string[] arrAuxThreeElements;
            if (!string.IsNullOrEmpty(sThreeElements))
            {
                arrAuxThreeElements = sThreeElements.Split(" ");

                //Verifica se existem 3 itens
                if (arrAuxThreeElements.Count() == 3)
                {
                    double dValorNegociacaoLinha = 0;
                    string sSetorClienteLinha = arrAuxThreeElements[1];
                    DateTime sDataProximaParcelaLinha;
                                        
                    if (!double.TryParse(arrAuxThreeElements[0], out dValorNegociacaoLinha))
                    {
                        //Valida a primeira informação (valor negociação)
                        Console.WriteLine("");
                        Console.WriteLine("Valor do negócio de número " + i + " inválido. Tente novamente.");
                        bValidaLinha = false;
                    }
                    else if (sSetorClienteLinha.ToUpper().Trim() != "PÚBLICO" && sSetorClienteLinha.ToUpper().Trim() != "PRIVADO")
                    {
                        //Valida setor cliente
                        Console.WriteLine("");
                        Console.WriteLine("Setor do cliente inválido no negócio de número " + i + ". Informe Público ou Privado. Tente novamente.");
                        bValidaLinha = false;
                    }
                    else if(!DateTime.TryParseExact(arrAuxThreeElements[2], "MM'/'dd'/'yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out sDataProximaParcelaLinha))
                    {
                        //Valida a primeira informação (data próxima parcela)
                        Console.WriteLine("");
                        Console.WriteLine("Data próxima parcela do negócio de número " + i + " inválida (formato correto mm/dd/aaaa). Tente novamente.");
                        bValidaLinha = false;
                    }                    
                    else
                    {
                        bValidaLinha = true;
                    }

                    //Validações ok, adiciona um objeto Negocio ao portfolio
                    Negocio objNegocio = new Negocio();
                    objNegocio.Value = dValorNegociacaoLinha;
                    objNegocio.ClientSector = sSetorClienteLinha;
                    objNegocio.NextPaymentDate = Convert.ToDateTime(arrAuxThreeElements[2]);

                    //Classifica os negócios
                    Categoria objCategoria = new Categoria();
                    lstNegocios.Add(objCategoria.ClassificaNegocio(objNegocio, sReferenceDate));
                }
                else
                {
                    bValidaLinha = false;
                    Console.WriteLine("");
                    Console.WriteLine("Você não informou os três dados para o negócio número " + i + ". Tente novamente.");
                }
            }

        }
  
    }

    //Após validações, se existir registros no objportfolio, lista com a classificação
    if (lstNegocios.Count() > 0)
    {
        Console.Clear();
        Console.WriteLine("#################");
        Console.WriteLine("### RESULTADO ###");
        Console.WriteLine("#################");

        Console.WriteLine("Data de Referência: " + sReferenceDate.ToString("MM/dd/yyyy"));
        Console.WriteLine("Número de Trades no Portfólio: " + iNumberTradesInPortfolio);
        Console.WriteLine("");
        Console.WriteLine("");

        //Lista os dados que foram informados
        foreach (Negocio objNegocioPortfolio in lstNegocios)
        {
            Console.WriteLine(objNegocioPortfolio.Value + " " + objNegocioPortfolio.ClientSector + " " + objNegocioPortfolio.NextPaymentDate.ToString("MM/dd/yyyy"));
        }

        //Lista as categorias
        Console.WriteLine("");
        Console.WriteLine("");
        foreach (Negocio objNegocioPortfolio in lstNegocios)
        {
            Console.WriteLine(objNegocioPortfolio.DescricaoCategoria);
        }
    }

    Console.ReadLine();
}
else
{
    Console.Clear();
    Console.WriteLine("Aplicativo encerrado. Número de negócios informado menor ou igual a zero.");
}

