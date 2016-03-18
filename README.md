# AP.AspNet.ResponseCompression
AP.AspNet.ResponseCompression is for <b>HTML minification</b> and <b>Gzip</b> compression.<br/>
a middleware for HTML minification.<br/><br/>
for gzip:

          public void Configure(IApplicationBuilder app)
           {

            app.UseGzipResponseCompress();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"<h1>        Hello       World!      </h1>
                  <p>   <h2>tag</h2>    <h2>tag2  tag3   </h2></p>");
            });
        }
        
  for minification:
  
          public void Configure(IApplicationBuilder app)
           {

            app.UseMinifyResponseCompress();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"<h1>        Hello       World!      </h1>
                  <p>   <h2>tag</h2>    <h2>tag2  tag3   </h2></p>");
            });
        }
        <br/>
  ResponseCompression remove "Enter" Char and replace each two "Space" char to one.
  <br/>
  <br/>
  sample output (Before Minify):<br/>
  
                   <h1>        Hello       World!      </h1>
                   <p>   <h2>tag</h2>    <h2>tag2  tag3   </h2></p>
  <br/>
  <br/>
  sample output (after Minify):<br/>

                   <h1> Hello World! </h1> <p> <h2>tag</h2> <h2>tag2 tag3 </h2></p>
  
