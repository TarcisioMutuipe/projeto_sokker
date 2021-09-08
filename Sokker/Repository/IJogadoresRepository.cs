using Sokker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokker.Repository
{
    public interface IJogadoresRepository
    {
        public IEnumerable<Jogadores> Listall();

        public int insert(Jogadores jogadores);

        int delete(long idjogador);
    }
}
