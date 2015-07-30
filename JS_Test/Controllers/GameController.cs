using JS_Test.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace JS_Test.Controllers
{
    public class Game
    {
       
        public int Id { get; set; }
        public int WhitePlayerId { get; set; }
        public int BlackPlayerId { get; set; }
        public string MoveList { get; set; }
   
    }

    public class GameController : ApiController
    {
        string lista;

        // GET: api/Game
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Game/5
        public Joc Get(int id)
        {
            var context = new SahEntities();
            var currentGame = context.Jocs.Find(id); 
            return currentGame;
        }

        // POST: api/Game
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string newGameStr)
        {
            var newGame = JsonConvert.DeserializeObject<Game>(newGameStr);
            // salvarea jocului in BD 

            //la salvarea in db jocul va primi un ID
            newGame.Id = 4;//simulez salvarea in db

            var context = new SahEntities();
            var gameInfo = JsonConvert.DeserializeObject<Game>(newGameStr);
            gameInfo.Id = newGame.Id;

            var jocNou = new Joc();
            jocNou.id = 4;
            jocNou.blackPlayer = gameInfo.BlackPlayerId;
            jocNou.whitePlayer = gameInfo.WhitePlayerId;
            context.Jocs.Add(jocNou);
            context.SaveChanges();

            var response = new HttpResponseMessage(HttpStatusCode.Created);
            string responseContentStr = "api/Game/" + newGame.Id;
            response.Content = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(responseContentStr))); ;
            return response;
        }

        // PUT: api/Game/5
        [HttpPut]
        public void Put(int id, [FromBody]string gameMoves)//example value for gameMoves: p4e4,p4e5,b2c4,b2c5,N2c3,N2f6
        {
            var context = new SahEntities();
            var newGame = JsonConvert.DeserializeObject<Game>(gameMoves);
           // lista = lista + newGame.MoveList;
            var currentGame = context.Jocs.Find(4);      
            currentGame.moveList = newGame.MoveList;
            context.SaveChanges();            

            //var currentGame = context.Games.Where(x => x.id == 1).First(); 
            //select top 1 * from Game where Id == 1
            
            //x => x.id == 1 este sintaxa pentru lambda expressions
            //Python: lambda x: x.id == 1
            //ruby: |x| x.id == 1

            //(x,y) => x.id == 1 && y == 2

            //(x,y) => {
            //    return x.id == 1 && y == 2
            //}


            //var currentGame = context.Games.Find(id); 
            //currentGame.Moves = gameMoves;
            //context.SaveChanges;


            // salvez in BD lista actuala de mutari

        }


        // DELETE: api/Game/5
        public void Delete(int id)
        {
        }
    }
}
