using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC_Diploma.Models;

namespace WebApplicationMVC_Diploma.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/")]
    //[RoutePrefix("api/input")]
    public class InputController : ControllerBase
    {
        private readonly IRequestResponseModel requestResponse;
        private readonly ISqlModel sqlModel;

        public InputController(ISqlModel sqlModel, IRequestResponseModel requestResponse){
            this.sqlModel = sqlModel;
            this.requestResponse = requestResponse;
        }

        [Route("")]
        [HttpGet]        
        public ActionResult<string[]> Get()
        {
            if (!requestResponse.GetRequest(out string[] result)) return NotFound();
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public ActionResult<string[]> Get(int id)
        //{
        //    return Ok(id); // for example
        //}

        [Route("")]
        [HttpPost]
        public ActionResult Post([FromBody]KeyValuePair<string[], string[]> message)
        {            
            requestResponse.AddResult(message);
            requestResponse.Commit();
            // Проверка добавления строки в БД
            sqlModel.AddResult(new Entities.Dictionary { Key = "треуг", Value = "деталь 1, деталь 2" });
            sqlModel.Commit();
            return Ok();
        }

        [Route("data")]
        [HttpGet]
        public ActionResult<KeyValuePair<string[], string[]>> GetData()
        {
            if (!requestResponse.GetResult(out KeyValuePair<string[], string[]> result)) return NotFound();
            return Ok(result);
        }

        [Route("data")]
        [HttpPost]
        public ActionResult PostData([FromBody]string[] data)
        {
            requestResponse.AddRequest(data);                     
            return Ok();
        }        

        //public async Task<ActionResult> Index() //для примера данный view
        //{
        //    string message = "zalupa";
        //    var data1 = await GetData(message);
        //    if (data1 == null)
        //    {
        //        var data2 = await AddData(new Data { mes = message });
        //    }
        //    //return View(data);
        //}
        //private async Task<Data> AddData(Data data)
        //{
        //    using (var context = new Models.AppContext())
        //    {
        //        context.Data.Add(data);
        //        await context.SaveChangesAsync();
        //    }
        //    return data;
        //}
        //private async Task<Data> GetData(string message)
        //{
        //    Data data = null;
        //    using (var context = new Models.AppContext())
        //    {
        //        data = await context.Data.FirstOrDefaultAsync((e) => e.mes.ToLower() == message.ToLower());
        //    }
        //    return data;
        //}
        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return repository.GetAll();
        //}

        //public Product GetProduct(int id)
        //{
        //    Product item = repository.Get(id);
        //    if (item == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //    return item;
        //}

        //public IEnumerable<Product> GetProductsByCategory(string category)
        //{
        //    return repository.GetAll().Where(
        //        p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        //}

        //public HttpResponseMessage PostProduct(Product item)
        //{
        //    item = repository.Add(item);
        //    var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

        //    string uri = Url.Link("DefaultApi", new { id = item.Id });
        //    response.Headers.Location = new Uri(uri);
        //    return response;
        //}

        //public void PutProduct(int id, Product product)
        //{
        //    product.Id = id;
        //    if (!repository.Update(product))
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //}

        //public void DeleteProduct(int id)
        //{
        //    repository.Remove(id);
        //}
    }
}