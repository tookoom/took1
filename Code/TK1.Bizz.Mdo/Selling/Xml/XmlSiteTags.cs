using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK1.Bizz.Mdo.Selling.Xml
{
    public class XmlSiteTags
    {
        public class Header
        {
            public static string FileInfo = "info_arquivo";

            public static string Version = "versao";
            public static string CustomerCode = "cod_cliente";
            public static string CustomerName = "nome_cliente";
            public static string Timestamp = "data_arquivo";
            public static string Key = "chave";
            public static string ItemCount = "quant_registros";
        }


        public static string Address = "endereco";
        public static string AddressNumber = "numero";
        public static string AreaDescription = "infra_estrutura_bairro";
        public static string Category = "tipoimovel";
        public static string District = "bairro";
        public static string City = "cidade";
        public static string CondDescription = "infra_estrutura_condominio";
        public static string Description = "descricao";
        public static string DescriptionCollection = "fichaimovel";
        public static string DescriptionValue = "conteudo";
        public static string ExternalArea = "area_terreno";
        public static string GarageNumber = "garagens";
        public static string HasBanner = "Banner";
        public static string InternalArea = "area_uso_privativo";
        public static string IsFeatured = "destaque";
        public static string Pictures = "imagens";
        public static string Picture = "imagem";
        public static string PictureDescription = "descricao_foto";
        public static string PictureFileName = "arquivo_foto";
        public static string RoomNumber = "dormitorios";
        public static string Sales = "vendas";
        public static string Sites = "imoveis";
        public static string Site = "imovel";
        public static string SiteCode = "codigo";
        public static string SiteType = "tipo";
        public static string ShortDescription = "descricaoresumida";
        public static string SuiteNumber = "suites";
        public static string TotalArea = "area_total_imovel";
        public static string UF = "uf";
        public static string Value = "valor";
        public static string XmlSiteFileRoot = "dadosimovel";
        public static string ZipCode = "cep";

        //public static string AdType = "tipo";
        //public static string BuildingName = "edificio";
        //public static string IsExclusive = "ImovelExclusivo";
        //public static string ExcludeSite = "excluir";
        //public static string Description = "caracteristica";
        //public static string DescriptionName = "descricao";

    }
}
