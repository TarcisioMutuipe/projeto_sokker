using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Sokker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sokker.Repository
{
    public class JogadoresRepository : IJogadoresRepository
    {
        private readonly string _connectionString;

        public JogadoresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public int insert(Jogadores jogadores)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                var jogador = ListOne(jogadores.id);
                string query = string.Empty;
                if (jogador == null)
                {
                   query = @"INSERT INTO `sokker`.`jogadores`(`id`,`nome`,`idade`,`valor`,`salario`,`gols`,`assitencia`,`jogos`,`forma`,`experiencia`,`trabalhotime`,
                                    `disciplina`,`resistencia`,`agilidade`,`tecnica`,`passe`,`goleiro`,`desarme`,`armacao`,`finalizacao`)
                                    VALUES(@id,@nome,@idade,@valor,@salario,@gols,@assitencia,@jogos,@forma,@experiencia,@trabalhotime,
                                    @disciplina,@resistencia,@agilidade,@tecnica,@passe,@goleiro,@desarme,@armacao,@finalizacao)";


                }
                else
                {
                    query = @" update `sokker`.`jogadores` set `nome` = @nome, `idade` =@idade,`valor` =@valor,`salario` =@salario,`gols` =@gols,`assitencia` =@assitencia,
                                         `jogos` =@jogos,`forma` =@forma,`experiencia` =@experiencia,`trabalhotime` =@trabalhotime, `disciplina` =@disciplina,
                                         `resistencia` =@resistencia,`agilidade` =@agilidade,`tecnica` =@tecnica,`passe` =@passe,`goleiro` =@goleiro,`desarme`=@desarme
                                        ,`armacao` =@armacao,`finalizacao`=@finalizacao where id = @id ";
                                   
                }
                    int retono = conexao.Execute(query, new
                    {
                        id = jogadores.id,
                        nome = jogadores.nome,
                        idade = jogadores.idade,
                        valor = jogadores.valor,
                        salario = jogadores.salario,
                        gols = jogadores.gols,
                        assitencia = jogadores.assistencia,
                        jogos = jogadores.jogos,
                        forma = jogadores.forma,
                        experiencia = jogadores.experiencia,
                        trabalhotime = jogadores.trabalhotime,
                        disciplina = jogadores.disciplina,
                        resistencia = jogadores.resistencia,
                        agilidade = jogadores.agilidade,
                        tecnica = jogadores.tecnica,
                        passe = jogadores.passe,
                        goleiro = jogadores.goleiro,
                        desarme = jogadores.desarme,
                        armacao = jogadores.armacao,
                        finalizacao = jogadores.finalizacao
                    });

                return retono;

            }
        }
        public int delete(long idjogador)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
               
                string query = string.Empty;
               
                query = @" delete from `sokker`.`jogadores` where id = @id ";
                
                int retono = conexao.Execute(query, new
                {
                    id = idjogador                   
                });

                return retono;

            }
        }

        public IEnumerable<Jogadores> Listall()
        {
             using var connection = new MySqlConnection(_connectionString);

            var jogadoresData = connection.Query<Jogadores>("select * from jogadores");

            return jogadoresData;
        }

        public Jogadores ListOne(long id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var jogadoresData = connection.Query<Jogadores>("select * from jogadores where id = @id", new { 
            id = id
            }).FirstOrDefault();

            return jogadoresData;
        }
    }
}
