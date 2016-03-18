# AP.AspNet.ResponseCompression
AP.AspNet.ResponseCompression is for <b>HTML minification</b> and <b>Gzip</b> compression.<br/>
a middleware for HTML minification.<br/><br/>

          public void Configure(IApplicationBuilder app)
           {

            app.UseGzipResponseCompress(
            new AP.AspNet.ResponseCompression.CompressionOption { 
            EnabledMinification=true});

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"<h1>        Hello       World!      </h1>
                  <p>   <h2>tag</h2>    <h2>tag2  tag3   </h2></p>");
            });
        }
        
  If "EnabledMinification" was true then ResponseCompression remove "Enter" Char and replace each two "Space" char to one.
  <br/>
  <br/>
  sample output (EnabledMinification=false):<br/>
  
                   <h1>        Hello       World!      </h1>
                   <p>   <h2>tag</h2>    <h2>tag2  tag3   </h2></p>
  <br/>
  <br/>
  sample output (EnabledMinification=true):<br/>

                   <h1> Hello World! </h1> <p> <h2>tag</h2> <h2>tag2 tag3 </h2></p>
  
