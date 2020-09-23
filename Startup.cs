using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.DataAccess;
using OdeToFood.Data_Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Management.Smo;
using System.Text;
using System.IO;
using Microsoft.Extensions.Logging;
using OdeToFood.ExtensionsMeth;

namespace OdeToFood
{
    public class Startup
    {
      

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<OdeToFoodDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb")));

           services.AddScoped<IResturantData, SqlResturantData>();
            services.AddScoped<ILogger<Startup>, Logger<Startup>>();
            //  services.AddSingleton<IResturantData, InMemoryResturantData>();

        

            services.AddRazorPages();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            }
            );

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env,OdeToFoodDBContext context,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.Use(SayHelloMiddleWare);
            var DropRecreate = false;
            if (DropRecreate)
            {
              string FileName = $"{ Directory.GetCurrentDirectory()}\\DataAccess\\OdeToFoodDbBackUp.sql";

                 Backup(FileName,logger);
            
                
               context.Database.EnsureDeleted();
               context.Database.EnsureCreated();
                ExecBackup(FileName, logger);
            }
         


            app.UseHttpsRedirection();

            app.Use(Next => {

                return async ctx => {
                    if (ctx.Request.Path.StartsWithSegments("/hello"))
                        ctx.Response.Redirect("/Resturants/List");
                    else
                        await Next.Invoke(ctx);

                };
            });
            app.UseStaticFiles();
            app.UseNodeModules();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                
            });

        }

        private RequestDelegate SayHelloMiddleWare(RequestDelegate next)
        {
            return async ctx =>
            {
                await ctx.Request.HttpContext.Response.WriteAsync("hello from middle ware!");


            };
        }
        public  static void  Backup(string FileName,ILogger logger)
        {
            try
            {
                File.WriteAllText(FileName, "");

                Server srv = new Server(new Microsoft.SqlServer.Management.Common.ServerConnection()
                {
                    ServerInstance = "DESKTOP-LMTOH28",
                    Authentication = Microsoft.SqlServer.Management.Common.SqlConnectionInfo.AuthenticationMethod.NotSpecified
                });
                logger.LogInformation("connection to server established....");
                Database dbs = srv.Databases["OdeToFood"];

                #region scripting option object

                ScriptingOptions options = new ScriptingOptions() { ScriptData = true,
                     ScriptDrops = false,
                     FileName = FileName,
                    EnforceScriptingOptions = true,
                    ScriptSchema = false,
                    IncludeHeaders = false,
                    AppendToFile = true,
                    Indexes = false,
                    WithDependencies = true
                };
               
                logger.LogInformation("get database success, building Scripting options obj succeed....");
                

                #endregion
                #region 
                //foreach (var tbl in Tables)
                //var dt = dbs.EnumObjects(DatabaseObjectTypes.Table);
                //var urns = new Microsoft.SqlServer.Management.Sdk.Sfc.Urn[dt.Rows.Count];
                //for (int rowIndex = 0; rowIndex < dt.Rows.Count; ++rowIndex)
                //{
                //    urns[rowIndex] = dt.Rows[rowIndex]["urn"].ToString();
                //}
                //Scripter scripter = new Scripter(srv);
                //var scrip=  scripter.Script(urns);


               
                #endregion
                #region looping tables and script them

                foreach (Table tbl in dbs.Tables)
                {
                    tbl.EnumScript(options);
                    
                    

                    //dbs.Tables[tbl].EnumScript(options);
                }
                #endregion
                logger.LogInformation($"script generated at {FileName} ....");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
   }
        public static void ExecBackup(string FileName,ILogger logger)
        {
            try
            {
                Server srv = new Server(new Microsoft.SqlServer.Management.Common.ServerConnection()
                {
                    ServerInstance = "DESKTOP-LMTOH28",
                    Authentication = Microsoft.SqlServer.Management.Common.SqlConnectionInfo.AuthenticationMethod.NotSpecified
                });
                logger.LogInformation("connection to server established....");
                string script = File.ReadAllText(FileName);
                Database dbs = srv.Databases["OdeToFood"];
                dbs.ExecuteNonQuery(script);
                logger.LogInformation("backup script executed successfully....");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }

            //foreach (var tbl in Tables)

        }



    }
}
