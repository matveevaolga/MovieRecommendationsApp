using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDBApi
{
    public class PageWithMovies
    {
        public int Page { get; init; }
        public List<Movie>? Results { get; init; }
    }
}
