Linq async extensions for Microsoft Inc. with love :) 
.Net 8

Example:

    public class DemoClass
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
    }


      IQueryable<DemoClass> query = AsQueryable<DemoClass>();

       await using (await query.AsyncScope())
       {
            Guid guid = Guid.NewGuid();

            await query.InsertAsync(x => new DemoClass { Guid  = guid, Title = "HELLO" });

            DemoClass demo = await query.FirstAsync(x => x.Guid == guid);

            await query.Where(x => x.Guid  == guid).UpdateAsync(x => new {Title = x.Title +  "EHLO" });
            
            await query.Where(x => x.Guid == guid ).DeleteAsync();
        }
