# AP.AspNet.ResponseCompression
AP.AspNet.ResponseCompression is for HTML minification and Gzip compression.<br/>
a middleware for HTML minification.<br/><br/>
For Html minification:<br/>

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
  
<br/>
For Gzip:

          public void Configure(IApplicationBuilder app)
           {

            app.UseGzipResponseCompress();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"<h1>        Hello       World!      </h1>
                  <p>   <h2>tag</h2>    <h2>tag2  tag3   </h2></p>");
            });
        }
