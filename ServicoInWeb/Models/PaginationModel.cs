using System.Text;

namespace ServicoInWeb.Models
{
    public sealed class PaginationModel
    {
        public PaginationModel(int number, bool active, string link)
        {
            Number = number;
            Active = active;
            Link = link;
        }

        public int Number {  get; private set; }
        public bool Active { get; private set; }
        public string Link { get; private set; }

        public static string SetLink(string uri, Dictionary<string, string> parametersUri, int itemsPerPage, int currentPage)
        {
            var builder = new StringBuilder();

            builder.Append(uri);
            builder.AppendFormat("Page={0}", currentPage);
            builder.AppendFormat("&itensPerPage={0}", itemsPerPage);
            foreach (var parameter in parametersUri) 
            {
                builder.AppendFormat("&{0}=", parameter.Key);
                builder.AppendFormat("{0}", parameter.Value);
            }
            return builder.ToString();
        }

        public void SetActive()
        {
            Active = true;
        }
    }
}
