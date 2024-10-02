using ServicoInWeb.Models;

namespace ServicoInWeb.Service
{
    public sealed class Pagination
    {
        public static List<PaginationModel> GetPaginationsLinks(int totalItens, int itensPerPage, int page, string uri, Dictionary<string, string> queryParametersUri)
        {
            List<PaginationModel> links = [];

            int totalPages = (int)Math.Ceiling((double)totalItens / itensPerPage);

            string firstLink = PaginationModel.SetLink(uri, queryParametersUri, itensPerPage, 1);
            links.Add(new PaginationModel(1, false, firstLink));

            if (page == 1)
            {
                for (int i = page; i <= page + 4; i++)
                {
                    if (i <= totalPages)
                    {
                        links.Add(new PaginationModel(i, i == page, PaginationModel.SetLink(uri, queryParametersUri, itensPerPage, i)));
                    }
                }
            }
            else if (page == totalPages)
            {
                for (int i = page; i > page - 5; i--)
                {
                    if (i > 0)
                    {
                        links.Add(new PaginationModel(i, i == page, PaginationModel.SetLink(uri, queryParametersUri, itensPerPage, i)));
                    }
                }
            }
            else
            {
                if (page == totalPages - 1)
                {
                    for (int i = page; i >= page - 3; i--)
                    {
                        if (i > 0 && links.Count < 5)
                        {
                            links.Add(new PaginationModel(i, i == page, PaginationModel.SetLink(uri, queryParametersUri, itensPerPage, i)));
                        }
                    }
                }
                else
                {
                    for (int i = page; i >= page - 3; i--)
                    {
                        if (i > 0 && links.Count < 4)
                        {
                            links.Add(new PaginationModel(i, i == page, PaginationModel.SetLink(uri, queryParametersUri, itensPerPage, i)));
                        }
                    }
                }

                for (int i = page; i < page + 4; i++)
                {
                    if (links.Count < 6 && i < totalPages + 1)
                    {
                        links.Add(new PaginationModel(i, i == page, PaginationModel.SetLink(uri, queryParametersUri, itensPerPage, i)));
                    }
                }
            }

            string lastLink = PaginationModel.SetLink(uri, queryParametersUri, itensPerPage, totalPages);
            links.Add(new PaginationModel(totalItens, false, lastLink));

            return links.OrderBy(x => x.Number).ToList();
        }
    }
}
