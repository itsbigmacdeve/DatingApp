using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class LikesParams : PaginationParams
    {
        // se le indican los parametros que se van a recibir para filtrar los likes por usuario en la paginacion
        public int UserId { get; set; }

        public string Predicate { get; set; }
    }
}