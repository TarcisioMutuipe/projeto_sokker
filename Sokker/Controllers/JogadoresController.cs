using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Sokker.Domain;
using Sokker.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Sokker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogadoresController : Controller
    {     
        private readonly ILogger<JogadoresController> _logger;
        private readonly IJogadoresRepository _jogadoresRepository;

      
        public JogadoresController(ILogger<JogadoresController> logger, IJogadoresRepository jogadoresRepository)
        {
            _logger = logger;
            _jogadoresRepository = jogadoresRepository;
        }

        [HttpGet]
        public ActionResult GetAllJogadores()
        {
            try
            {
                var data = _jogadoresRepository.Listall();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tetar obter dados");
                return new StatusCodeResult(500);
            }
        }

        // POST: JogadoresController/Create
        [HttpPost]        
        public ActionResult InserindojogadoresNovos(string user, string senha )
        {
            try
            {
                using (var client = new WebClientEx())
                {

                    var data = new NameValueCollection
                    {
                        { "ilogin", "tauu" },
                        { "ipassword", "Mutuipe01" },
                    };
                    var response1 = client.UploadValues("https://sokker.org/start.php?session=xml", data);
                    var response2 = client.DownloadString("https://sokker.org/xml/players-90578.xml");
                    StreamWriter sw = new StreamWriter("C:\\Projetos\\ArquivosSokker\\WriteText.xml");
                    //Write a line of text
                    sw.WriteLine(response2);
                    //Write a second line of text
                    //Close the file
                    sw.Close();

                    XmlTextReader xmlReader = new XmlTextReader("C:\\Projetos\\ArquivosSokker\\WriteText.xml");
                    IList<Jogadores> listJogadores = new List<Jogadores>();
                    Jogadores jogador = new Jogadores();
                    bool inicio = false;
                    int contador = 1;
                    List<string> lista = new List<string>();
                    while (xmlReader.Read())
                    {                        
                        switch (xmlReader.NodeType)
                        {                            
                            case XmlNodeType.Element:
                                if (xmlReader.Name.Equals("player"))
                                {                                   
                                    if(jogador.id > 0) { 

                                    listJogadores.Add(jogador);
                                    jogador = new Jogadores();
                                    contador = 1;
                                    }
                                }

                                break;

                            case XmlNodeType.Text:
                                 
                                    if(contador == 1) jogador.id = Convert.ToInt64( xmlReader.Value); 
                                    if (contador == 2 || contador == 3) jogador.nome += string.IsNullOrEmpty(jogador.nome) ? xmlReader.Value : ' ' + xmlReader.Value ;                                    
                                    if (contador == 5)  jogador.idade = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 11) jogador.valor = Convert.ToDecimal(xmlReader.Value);
                                    if (contador == 12) jogador.salario = Convert.ToDecimal(xmlReader.Value);
                                    if (contador == 14) jogador.gols = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 15) jogador.assistencia = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 16) jogador.jogos = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 23) jogador.forma = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 24) jogador.experiencia = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 25) jogador.trabalhotime = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 26) jogador.disciplina = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 28) jogador.resistencia = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 29) jogador.agilidade = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 30) jogador.tecnica = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 31) jogador.passe = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 32) jogador.goleiro = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 33) jogador.desarme = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 34) jogador.armacao = Convert.ToInt32(xmlReader.Value);
                                    if (contador == 35) jogador.finalizacao = Convert.ToInt32(xmlReader.Value);

                                    contador = contador + 1;
                                                              
                                break;
                        } 
                    }
                   
                    foreach (var item in listJogadores)
                    {
                        _jogadoresRepository.insert(item);
                    }
                    var listaBanco = _jogadoresRepository.Listall();
                    foreach (var itemDeleta in listaBanco)
                    {
                        if (listJogadores.Where(x => x.id == itemDeleta.id).ToList().Count == 0)
                            _jogadoresRepository.delete(itemDeleta.id);
                    }
                   
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tetar inserir os dados");
                return new StatusCodeResult(500);
            }
        }


        [HttpGet]
        [Route("Transferencia")]
        public ActionResult Transferencia()
        {          
            try
            {
                using (var client = new WebClientEx())
                {
                    var data = new NameValueCollection
                    {
                        { "ilogin", "tauu" },
                        { "ipassword", "Mutuipe01" },
                    };
                    var response1 = client.UploadValues("https://sokker.org/start.php?session=xml", data);
                    var response2 = client.DownloadString("https://sokker.org/xml/transfers.xml");
                    StreamWriter sw = new StreamWriter("C:\\Projetos\\ArquivosSokker\\transferencia.xml");
                    //Write a line of text
                    sw.WriteLine(response2);
                    //Write a second line of text
                    //Close the file
                    sw.Close();

                    XmlTextReader xmlReader = new XmlTextReader("C:\\Projetos\\ArquivosSokker\\transferencia.xml");
                    IList<Jogadores> listJogadores = new List<Jogadores>();
                    Jogadores jogador = new Jogadores();                   
                    int contador = 1;
                    List<string> lista = new List<string>();
                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (xmlReader.Name.Equals("transfer"))
                                {
                                    if (jogador.id > 0)
                                    {
                                        listJogadores.Add(jogador);
                                        jogador = new Jogadores();
                                        contador = 1;
                                    }
                                }

                                break;

                            case XmlNodeType.Text:

                                if (contador == 1) jogador.id = Convert.ToInt64(xmlReader.Value);
                                if (contador == 2 || contador == 3) jogador.nome += string.IsNullOrEmpty(jogador.nome) ? xmlReader.Value : ' ' + xmlReader.Value;
                                if (contador == 5) jogador.idade = Convert.ToInt32(xmlReader.Value);
                                if (contador == 11) jogador.valor = Convert.ToDecimal(xmlReader.Value);
                                if (contador == 12) jogador.salario = Convert.ToDecimal(xmlReader.Value);
                                if (contador == 14) jogador.gols = Convert.ToInt32(xmlReader.Value);
                                if (contador == 15) jogador.assistencia = Convert.ToInt32(xmlReader.Value);
                                if (contador == 16) jogador.jogos = Convert.ToInt32(xmlReader.Value);
                                if (contador == 23) jogador.forma = Convert.ToInt32(xmlReader.Value);
                                if (contador == 24) jogador.experiencia = Convert.ToInt32(xmlReader.Value);
                                if (contador == 25) jogador.trabalhotime = Convert.ToInt32(xmlReader.Value);
                                if (contador == 26) jogador.disciplina = Convert.ToInt32(xmlReader.Value);
                                if (contador == 28) jogador.resistencia = Convert.ToInt32(xmlReader.Value);
                                if (contador == 29) jogador.agilidade = Convert.ToInt32(xmlReader.Value);
                                if (contador == 30) jogador.tecnica = Convert.ToInt32(xmlReader.Value);
                                if (contador == 31) jogador.passe = Convert.ToInt32(xmlReader.Value);
                                if (contador == 32) jogador.goleiro = Convert.ToInt32(xmlReader.Value);
                                if (contador == 33) jogador.desarme = Convert.ToInt32(xmlReader.Value);
                                if (contador == 34) jogador.armacao = Convert.ToInt32(xmlReader.Value);
                                if (contador == 35) jogador.finalizacao = Convert.ToInt32(xmlReader.Value);

                                contador = contador + 1;

                                break;
                        }
                    }
                  return Ok();
                }               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tetar obter dados");
                return new StatusCodeResult(500);
            }
        }
    }
}
