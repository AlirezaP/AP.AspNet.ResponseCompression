# AP.AspNet.ResponseCompression
AP.AspNet.ResponseCompression is for page Compression.<br/>
a middleware for Compression response page.<br/><br/>

          public void Configure(IApplicationBuilder app)
           {

            app.UseResponseCompress();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"<h1>        Hello       World!      </h1>
                  <p>   <h2>tag</h2>    <h2>tag2  tag3   </h2></p>");
            });
        }
        
  ResponseCompression remove "Enter" Char and replace each two "Space" char to one.
  <br/>
  <br/>
  sample output (before ResponseCompression):<br/>
  
                   <h1>        Hello       World!      </h1>
                   <p>   <h2>tag</h2>    <h2>tag2  tag3   </h2></p>
  <br/>
  <br/>
  sample output (after ResponseCompression):<br/>

                   <h1> Hello World! </h1> <p> <h2>tag</h2> <h2>tag2 tag3 </h2></p>
  
